using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class PlayerAlreadyInWaitingListException : Exception
    {

        public PlayerAlreadyInWaitingListException(string message) : base(message)
        {

        }

        public PlayerAlreadyInWaitingListException() : base("The player is already a member of the waiting list!")
        {

        }

    }
}
