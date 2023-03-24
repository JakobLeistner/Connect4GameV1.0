using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Contracts;

namespace BusinessLogicPOC
{
    public class BusinessLogic : IBusinessLogic
    {
        public BusinessLogic()
        {

        }
        public Action<GameState> GameStateChanged { get; set; }
        public bool GameStartedState { get; set; } = false;

        public void StartGame()
        {
            GameStartedState = true;
            GameStateChanged?.Invoke(new GameState() { State = true });
        }
    }
}