using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.Exceptions
{
    public class BoardColumnIsFullException : Exception
    {
        public BoardColumnIsFullException() : base("This board column is full. Please select another one") { }
    }
}
