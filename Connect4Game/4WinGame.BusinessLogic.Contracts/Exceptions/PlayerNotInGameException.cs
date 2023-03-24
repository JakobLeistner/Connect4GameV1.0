using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.Exceptions
{
    public class PlayerNotInGameException : Exception
    {
        public PlayerNotInGameException() : base("This player is not in this game") { }
    }
}
