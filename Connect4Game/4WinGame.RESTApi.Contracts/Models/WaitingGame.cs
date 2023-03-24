using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class WaitingGame
    {

        public string PlayerName { get; set; }

        public WaitingGame(string playername)
        {
            PlayerName = playername;
        }
        public WaitingGame()
        {

        }
    }
}
