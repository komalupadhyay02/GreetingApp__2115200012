using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]  // Fixed Route
    public class HelloGreetingController : ControllerBase
    {
        //UC3  code
        [HttpGet("PersonalizedGreeting")]
        public IActionResult GetPersonalizedGreeting([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            var message = _greetingService.GetGreeting(firstName, lastName);

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting retrieved successfully",
                Data = message
            });
        }
        private static Dictionary<string, string> greetings = new Dictionary<string, string>();

        // UC2 - Service Layer
        private readonly IGreetingBL _greetingService;

        public HelloGreetingController(IGreetingBL greetingService)
        {
            _greetingService = greetingService;
        }

        /// <summary>
        /// Get greeting from Service Layer
        /// </summary>
        [HttpGet("Greeting")]
        public IActionResult GetGreeting()
        {
            var message = _greetingService.GetGreeting();
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting retrieved successfully",
                Data = message
            });
        }

        /// <summary>
        /// Get method to return "Hello World"
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
        /// Post method to add a greeting
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
        /// Put method to update a greeting
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

        /// <summary>
        /// Delete method to remove a greeting
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

        /// <summary>
        /// Patch method to modify a greeting
        /// </summary>
        [HttpPatch("{key}")]
        public IActionResult Patch(string key, [FromBody] RequestModel requestModel)
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

            greetings[key] += " " + requestModel.value;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting modified successfully.",
                Data = $"Key: {key}, Value: {greetings[key]}"
            });
        }
    }
}