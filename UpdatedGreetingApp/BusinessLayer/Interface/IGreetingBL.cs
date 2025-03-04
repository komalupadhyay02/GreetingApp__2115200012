using System;
namespace BusinessLayer.Interface
{
	public interface IGreetingBL
	{
        string GetGreeting();
        string GetGreeting(string firstName, string lastName);
    }
}

