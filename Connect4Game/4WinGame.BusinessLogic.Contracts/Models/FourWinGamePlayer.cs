using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.Models
{
    public class FourWinGamePlayer
    {
        public string Name { get; }
        public string ID { get; }
        public FourWinGamePlayer(string name, string id)
        {
            Name = name;
            ID = id;
        }
    }
}
