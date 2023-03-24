using System;

namespace _4WinGame.RESTApi.Contracts.Models
{
    public class Player
    {
        public string PlayerName { get; set; }

        public Player(string playerName)
        {
            PlayerName = playerName;
        }

        public Player()
        {

        }

    }
}
