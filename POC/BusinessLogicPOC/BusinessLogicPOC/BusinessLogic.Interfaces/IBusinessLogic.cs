using BusinessLogic.Interfaces.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBusinessLogic
    {
        public void StartGame();

        public Action<GameState> GameStateChanged { get; set; }

        public bool GameStartedState { get; set; }
    }
}
