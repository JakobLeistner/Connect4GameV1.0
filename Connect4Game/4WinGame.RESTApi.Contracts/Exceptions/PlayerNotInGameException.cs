using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class PlayerNotInGameException : Exception
    {

        public PlayerNotInGameException() : base("The chosen player is currently not in a game!")
        {

        }

        public PlayerNotInGameException(string message) : base(message)
        {

        }

    }
}
