 using System;
using ModelLayer.Entity;
namespace RepositoryLayer.Interface
{
	public interface IGreetingRL
	{
    
        
            Task<IEnumerable<Greeting>> GetAllGreetingsAsync();
            // Other methods
     
        Greeting SaveGreeting(Greeting greeting); // Add this method

    }
}

