using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class PlayerAlreadyCreatedWaitingGame : Exception
    {

        public PlayerAlreadyCreatedWaitingGame(string message) : base(message)
        {

        }

        public PlayerAlreadyCreatedWaitingGame() : base("The player has already created a waiting game!")
        {

        }

    }
}
