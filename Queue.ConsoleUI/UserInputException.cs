using System;

namespace Queue.ConsoleUI
{
    internal class UserInputException : Exception
    {
        public UserInputException(string message) : base(message)
        {
        }
    }
}