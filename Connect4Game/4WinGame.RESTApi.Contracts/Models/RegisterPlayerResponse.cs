using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class RegisterPlayerResponse
    {

        public MyPlayer RegisteredPlayer { get; set; }

        public RegisterPlayerResponse(MyPlayer myplayer)
        {
            RegisteredPlayer = myplayer;
        }
        public RegisterPlayerResponse()
        {

        }
    }
}
