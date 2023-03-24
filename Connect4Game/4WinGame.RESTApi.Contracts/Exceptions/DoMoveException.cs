using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class DoMoveException : Exception
    {

        public DoMoveException(string message, Exception exception) : base(message, exception)
        {

        }

        public DoMoveException(Exception exception) : base("Exception while do Move")
        {

        }

    }
}
