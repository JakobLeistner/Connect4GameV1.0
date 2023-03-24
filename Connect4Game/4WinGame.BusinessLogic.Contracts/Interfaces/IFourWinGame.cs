using _4WinGame.BusinessLogic.Contracts.Models;
using System;

namespace _4WinGame.BusinessLogic.Contracts.Interfaces
{
    public interface IFourWinGame
    {
        int[][] Board { get; }
        EventHandler OnGameStateChange { get; set; }
        EventHandler OnGameFinish { get; set; }
        FourWinGamePlayer Player1 { get; }
        FourWinGamePlayer Player2 { get; }
        string ID { get; set; }


        void DoMove(int column, FourWinGamePlayer player);
        FourWinGamePlayer GetWinner();
        void Resign(FourWinGamePlayer p);
        FourWinGamePlayer GetCurrentPlayer();
        bool IsPlayerInGame(string playerID);
        FourWinGamePlayer GetOpponent(FourWinGamePlayer player);
    }
}
