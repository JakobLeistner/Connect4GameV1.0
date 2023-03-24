using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException() : base("This game does not exist") { }
    }
}
