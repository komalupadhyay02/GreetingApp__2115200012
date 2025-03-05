using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using ModelLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly GreetingDbContext _context;

        public GreetingRL(GreetingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new greeting asynchronously.
        /// </summary>
        /// <param name="greeting">Greeting object to add.</param>
        /// <returns>Added Greeting object.</returns>

        public async Task<IEnumerable<Greeting>> GetAllGreetingsAsync()
        {
            return await _context.Greetings.ToListAsync();
        }
        public async Task<Greeting> GetGreetingByIdAsync(int id)
        {
            return await _context.Greetings.FindAsync(id);
        }
        public async Task<Greeting> UpdateGreetingAsync(Greeting greeting)
        {
            _context.Greetings.Update(greeting);
            await _context.SaveChangesAsync();
            return greeting;
        }


        /// <summary>
        /// Save a new greeting synchronously.
        /// </summary>
        /// <param name="greeting">Greeting object to save.</param>
        /// <returns>Saved Greeting object.</returns>
        public Greeting SaveGreeting(Greeting greeting)
        {
            try
            {
                _context.Greetings.Add(greeting);
                _context.SaveChanges();
                return greeting;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving greeting: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
        }

    }
}
