using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException() : base("Your name is invalid!") {

        }

        public InvalidNameException(string message) : base(message)
        {

        }
    }
}
