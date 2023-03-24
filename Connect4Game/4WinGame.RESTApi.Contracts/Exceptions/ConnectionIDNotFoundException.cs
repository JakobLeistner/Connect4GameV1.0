using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class ConnectionIDNotFoundException : Exception
    {

        public ConnectionIDNotFoundException(string message) : base(message)
        {

        }

        public ConnectionIDNotFoundException() : base("The connection id could not found")
        {

        }

    }
}
