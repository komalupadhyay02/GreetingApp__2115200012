using System;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using ModelLayer.Entity;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        // Constructor to initialize the repository layer
        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }
       
        
        // Basic greeting method
        public string GetGreeting()
        {
            return "Hello, World!";
        }

        // Overloaded greeting method with first and last name
        public string GetGreeting(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                return $"Hello {firstName} {lastName}!";
            else if (!string.IsNullOrWhiteSpace(firstName))
                return $"Hello {firstName}!";
            else if (!string.IsNullOrWhiteSpace(lastName))
                return $"Hello {lastName}!";
            else
                return "Hello, World!";
        }

        public async Task<IEnumerable<Greeting>> GetAllGreetingsAsync()
        {
            return await _greetingRL.GetAllGreetingsAsync();
        }
        public Greeting SaveGreeting(Greeting greeting)
        {
            return _greetingRL.SaveGreeting(greeting);
        }
        public async Task<Greeting> GetGreetingByIdAsync(int id)
        {
            return await _greetingRL.GetGreetingByIdAsync(id);
        }

    }
}
