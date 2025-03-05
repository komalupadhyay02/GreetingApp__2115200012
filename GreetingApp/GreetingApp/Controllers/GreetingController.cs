using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.Entity;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Controller providing APIs for the Greeting Application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        // Dictionary for storing greetings (used for UC1 - Testing different HTTP methods)
        private static Dictionary<string, string> greetings = new Dictionary<string, string>();

        // Service Layer Dependency Injection (used for UC2, UC3, UC4)
        private readonly IGreetingBL _greetingService;

        public HelloGreetingController(IGreetingBL greetingService)
        {
            _greetingService = greetingService;
        }

        //UC5 code
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGreetingById(int id)
        {
            var greeting = await _greetingService.GetGreetingByIdAsync(id);
            if (greeting == null)
            {
                return NotFound($"Greeting with ID {id} not found.");
            }
            return Ok(greeting);
        }
        //uc4 code
        [HttpGet("GetAllGreetings")]
        public async Task<IActionResult> GetAllGreetings()
        {
            var greetings = await _greetingService.GetAllGreetingsAsync();
            if (greetings == null || !greetings.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "No greetings found.",
                    Data = null
                });
            }

            return Ok(new ResponseModel<IEnumerable<Greeting>>
            {
                Success = true,
                Message = "Greetings retrieved successfully.",
                Data = greetings
            });
        }
        /// <summary>
        /// UC1 - HTTP GET: Return "Hello World" Message
        /// </summary>
        [HttpGet("Message")]
        public IActionResult GetHelloMessage()
        {
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Hello to Greeting App API.",
                Data = "Hello World"
            });
        }

        /// <summary>
        /// UC2 - HTTP GET: Fetch Greeting from Service Layer
        /// </summary>
        [HttpGet("Greeting")]
        public IActionResult GetGreeting()
        {
            var message = _greetingService.GetGreeting();
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting retrieved successfully.",
                Data = message
            });
        }

        /// <summary>
        /// UC3 - HTTP GET: Personalized Greeting with First Name and/or Last Name
        /// </summary>
        [HttpGet("PersonalizedGreeting")]
        public IActionResult GetPersonalizedGreeting([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            var message = _greetingService.GetGreeting(firstName, lastName);

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting retrieved successfully.",
                Data = message
            });
        }

        /// <summary>
        /// UC4 - HTTP POST: Save Greeting in Repository
        /// </summary>
        [HttpPost("SaveGreeting")]
        public IActionResult SaveGreeting([FromBody] Greeting greeting)
        {
            if (string.IsNullOrWhiteSpace(greeting.Message))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting message cannot be empty.",
                    Data = null
                });
            }

            var result = _greetingService.SaveGreeting(greeting);

            return Created("", new ResponseModel<Greeting>
            {
                Success = true,
                Message = "Greeting saved successfully.",
                Data = result
            });
        }

        /// <summary>
        /// UC1 - HTTP POST: Add Greeting
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] RequestModel requestModel)
        {
            if (string.IsNullOrWhiteSpace(requestModel.key) || string.IsNullOrWhiteSpace(requestModel.value))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Key and Value are required.",
                    Data = null
                });
            }

            if (greetings.ContainsKey(requestModel.key))
            {
                return Conflict(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Key already exists. Use a different key.",
                    Data = null
                });
            }

            greetings[requestModel.key] = requestModel.value;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting added successfully.",
                Data = $"Key: {requestModel.key}, Value: {requestModel.value}"
            });
        }

        /// <summary>
        /// UC1 - HTTP PUT: Update Greeting
        /// </summary>
        [HttpPut("{key}")]
        public IActionResult Put(string key, [FromBody] RequestModel requestModel)
        {
            if (!greetings.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found.",
                    Data = null
                });
            }

            greetings[key] = requestModel.value;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting updated successfully.",
                Data = $"Key: {key}, Value: {requestModel.value}"
            });
        }
        [HttpPatch("{key}")]
        public IActionResult Patch(string key, [FromBody] string partialUpdate)
        {
            if (!greetings.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found.",
                    Data = null
                });
            }

            // Append the partial update to the existing value
            greetings[key] += " " + partialUpdate;

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting updated successfully.",
                Data = $"Key: {key}, Value: {greetings[key]}"
            });
        }


        /// <summary>
        /// UC1 - HTTP DELETE: Remove Greeting
        /// </summary>
        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            if (!greetings.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found.",
                    Data = null
                });
            }

            string removedGreeting = greetings[key];
            greetings.Remove(key);
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting deleted successfully.",
                Data = $"Key: {key}, Value: {removedGreeting}"
            });
        }
    }
}
