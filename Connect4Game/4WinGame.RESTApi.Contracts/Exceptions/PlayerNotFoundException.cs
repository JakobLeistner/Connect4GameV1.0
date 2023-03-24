using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class PlayerNotFoundException : Exception
    {

        public PlayerNotFoundException() : base("The chosen player was not found")
        {

        }

        public PlayerNotFoundException(string message) : base(message)
        {

        }

    }
}
