using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class MyPlayer : Player
    {

        public string PlayerID { get; set; }
        public string RTPConnectionID { get; set; }

        public MyPlayer(string playerName, string rtpconnectionid, string id) : base(playerName)
        {
            PlayerID = id;
            RTPConnectionID = rtpconnectionid;
        }

        public MyPlayer() : base()
        {

        }

    }
}
