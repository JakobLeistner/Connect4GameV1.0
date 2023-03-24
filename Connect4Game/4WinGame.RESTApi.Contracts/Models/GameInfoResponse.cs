using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class GameInfoResponse
    {
        public GameInfo GameInfo {get; set; }

        public GameInfoResponse(GameInfo gameinfo)
        {
            GameInfo = gameinfo;
        }

        public GameInfoResponse()
        {

        }

    }
}
