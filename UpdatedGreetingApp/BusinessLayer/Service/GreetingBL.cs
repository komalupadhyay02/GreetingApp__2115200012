using System;
using BusinessLayer.Interface;
namespace BusinessLayer.Service
{
	public class GreetingBL:IGreetingBL
	{
        public string GetGreeting()
        {
            return "Hello, World";
        }
        public string GetGreeting(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                return $"Hello {firstName} {lastName}!";
            else if (!string.IsNullOrWhiteSpace(firstName))
                return $"Hello {firstName}!";
            else if (!string.IsNullOrWhiteSpace(lastName))
                return $"Hello {lastName}!";
            else
                return "Hello World!";
        }
    }
}

