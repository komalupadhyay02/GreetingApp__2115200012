 using System;
using ModelLayer.Entity;
namespace RepositoryLayer.Interface
{
	public interface IGreetingRL
	{
    
        
            Task<IEnumerable<Greeting>> GetAllGreetingsAsync();
            // Other methods
     
        Greeting SaveGreeting(Greeting greeting); // Add this method
        Task<Greeting> GetGreetingByIdAsync(int id);
        Task<Greeting> UpdateGreetingAsync(Greeting greeting);
        Task DeleteGreetingAsync(int id);




    }
}

