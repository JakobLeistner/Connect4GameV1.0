using _4WinGame.BusinessLogic.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4WinGame.BusinessLogic.Contracts.Interfaces
{
    public interface IFourWinGamesService
    {
        List<IFourWinGame> Games { get; }
        List<FourWinGamePlayer> WaitingGames { get; set; }
        EventHandler OnGameStarted { get; set; }
        List<FourWinGamePlayer> AllPlayers { get; set; }
        EventHandler OnWaitingListUpdated { get; set; }

        IFourWinGame JoinWaitingGame(FourWinGamePlayer p1, FourWinGamePlayer p2);
        void LeaveActiveGame(FourWinGamePlayer p, string gameID);
        IFourWinGame GetGameByID(string gameID);
    }
}
