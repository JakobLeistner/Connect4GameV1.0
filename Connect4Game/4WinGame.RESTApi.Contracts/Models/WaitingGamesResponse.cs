using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class WaitingGamesResponse
    {
        public List<WaitingGame> WaitingGames { get; set; }
        
        public WaitingGamesResponse(List<WaitingGame> waitingGames) 
        {
            WaitingGames = waitingGames;
        }
        public WaitingGamesResponse()
        {

        }
    }

}
