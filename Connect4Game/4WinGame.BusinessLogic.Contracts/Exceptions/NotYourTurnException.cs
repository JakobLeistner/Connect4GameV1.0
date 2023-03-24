using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.Exceptions
{
    public class NotYourTurnException : Exception
    {
        public NotYourTurnException() : base("It is not your turn") { }
    }
}
