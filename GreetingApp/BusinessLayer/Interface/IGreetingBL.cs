using System;
using ModelLayer.Entity;
namespace BusinessLayer.Interface
{
	public interface IGreetingBL
	{
        string GetGreeting();
        string GetGreeting(string firstName, string lastName);
        Greeting SaveGreeting(Greeting greeting);
        Task<IEnumerable<Greeting>> GetAllGreetingsAsync();
        Task<Greeting> GetGreetingByIdAsync(int id);
    }
  

}

