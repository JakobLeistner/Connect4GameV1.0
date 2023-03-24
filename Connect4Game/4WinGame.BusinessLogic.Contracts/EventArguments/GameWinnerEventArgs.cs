using _4WinGame.BusinessLogic.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.EventArguments
{
    public class GameWinnerEventArgs : EventArgs
    {
        public FourWinGamePlayer Player { get; set; }
        public GameWinnerEventArgs(FourWinGamePlayer player)
        {
            Player = player;
        }
    }
}
