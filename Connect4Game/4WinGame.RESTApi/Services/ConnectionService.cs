using _4WinGame.RESTApi.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Services
{
    public class ConnectionService
    {

        public Dictionary<string, string> PlayerIDToConnectionIDlist;
        public List<string> ConnectedIDs;
        public List<MyPlayer> AllPlayers; 

        public ConnectionService()
        {
            PlayerIDToConnectionIDlist = new Dictionary<string, string>();
            ConnectedIDs = new List<string>();
            AllPlayers = new List<MyPlayer>(); 
        }

        public void AddPlayer(MyPlayer player, string playerID, string connectionID)
        {
            PlayerIDToConnectionIDlist.Add(playerID, connectionID);
            AllPlayers.Add(player);
        }

        public void LeavePlayer(string playerID)
        {
            PlayerIDToConnectionIDlist.Remove(playerID);
        }

    }
}
