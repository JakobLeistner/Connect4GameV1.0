using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class PlayerAgainsItselfException : Exception
    {

        public PlayerAgainsItselfException(string message) : base(message)
        {

        }

        public PlayerAgainsItselfException() : base("It is not possible to play a game against yourself!")
        {

        }

    }
}
