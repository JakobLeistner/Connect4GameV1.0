using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class JoinGameResponse
    {

        public string GameID { get; set; }

        public JoinGameResponse(string gameid)
        {
            GameID = gameid;
        }
        public JoinGameResponse()
        {

        }
    }
}
