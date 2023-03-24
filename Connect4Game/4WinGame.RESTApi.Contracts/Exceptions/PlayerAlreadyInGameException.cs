using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class PlayerAlreadyInGameException : Exception
    {

        public PlayerAlreadyInGameException(string message) : base(message)
        {

        }

        public PlayerAlreadyInGameException() : base("The player is already in a game!")
        {

        }

    }
}
