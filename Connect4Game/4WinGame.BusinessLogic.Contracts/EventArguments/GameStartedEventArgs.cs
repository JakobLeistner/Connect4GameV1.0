using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.EventArguments
{
    public class GameStartedEventArgs : EventArgs
    {
        public string GameID;
        public GameStartedEventArgs(string id)
        {
            GameID = id;
        }

    }
}
