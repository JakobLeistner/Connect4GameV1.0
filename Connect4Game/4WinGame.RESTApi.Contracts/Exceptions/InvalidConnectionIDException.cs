using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class InvalidConnectionIDException : Exception
    {

        public InvalidConnectionIDException(string message) : base(message)
        {

        }

        public InvalidConnectionIDException() : base("Your connection-id is invalid!")
        {

        }

    }
}
