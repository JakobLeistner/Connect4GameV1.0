using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Exceptions
{
    public class WaitingListEntryNotFoundException : Exception
    {

        public WaitingListEntryNotFoundException(string message) : base(message)
        {

        }

        public WaitingListEntryNotFoundException() : base("The searched object is not a member of the waiting list!")
        {

        }

    }
}
