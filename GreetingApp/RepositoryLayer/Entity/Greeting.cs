using System.ComponentModel.DataAnnotations;

namespace GreetingApp.Models
{
    public class Greeting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
