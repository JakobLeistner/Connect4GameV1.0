using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class ConnectionIDNotUniqueException : Exception
    {

        public ConnectionIDNotUniqueException(string message) : base(message)
        {

        }

        public ConnectionIDNotUniqueException() : base("Exception ID not unique!")
        {

        }

    }
}
