using _4WinGame.BusinessLogic.Contracts.EventArguments;
using _4WinGame.BusinessLogic.Contracts.Exceptions;
using _4WinGame.BusinessLogic.Contracts.Interfaces;
using _4WinGame.BusinessLogic.Contracts.Models;
using System;

namespace _4WinGame.BusinessLogic
{
    public class FourWinGame : IFourWinGame
    {
        public const int BoardWidth = 7;
        public const int BoardHeight = 6;
        public int[][] Board { get; set; }
        public FourWinGamePlayer Player1 { get; set; }
        public FourWinGamePlayer Player2 { get; set; }
        public int CurrentPlayer { get; set; }
        public EventHandler OnGameStateChange { get; set; }
        public EventHandler OnGameFinish { get; set; }
        public string ID { get; set; }

        public FourWinGame(FourWinGamePlayer player1, FourWinGamePlayer player2)
        {
            // Set empty board
            Board = new int [BoardHeight][];
            for (int row = 0; row < BoardHeight; row++)
            {
                Board[row] = new int[BoardWidth];
                for (int column = 0; column < BoardWidth; column++)
                {
                    Board[row][column] = 0;
                }
            }

            Player1 = player1;
            Player2 = player2;
            CurrentPlayer = 1;
            Guid uuid = Guid.NewGuid();
            ID = uuid.ToString();
        }
        public void DoMove(int column, FourWinGamePlayer player)
        {
            string playerID = player.ID;
            if (Player1.ID != playerID && Player2.ID != playerID)
            {
                throw new PlayerNotInGameException();
            }
            if (Player1.ID == playerID && CurrentPlayer != 1 || Player2.ID == playerID && CurrentPlayer != 2)
            {
                throw new NotYourTurnException();
            }
            if (column < 1 || column > 7)
            {
                throw new BoardOutOfRangeException();
            }
            if (Board[0][column-1] != 0)
            {
                throw new BoardColumnIsFullException();
            }

            for (int row = BoardHeight - 1; row > -1; row--)
            {
                if (Board[row][column-1] == 0)
                {
                    Board[row][column-1] = CurrentPlayer;
                    break;
                }
            }

            OnGameStateChange?.Invoke(this, new EventArgs());
            CurrentPlayer = (CurrentPlayer - 1) * -1 + 2; // Toggle CurrentPlayer

            if (isBoardFull() || GetWinner() != null)
            {
                InvokeGameFinished();
            }
        }

        private bool isBoardFull()
        {
            for (int column = 0; column < BoardWidth; column++)
            {
                if (Board[0][column] == 0)
                    return false;
            }
            return true;
        }

        public FourWinGamePlayer GetWinner()
        {
 
            // Horizontal
            for (int row = 0; row < BoardHeight; row++)
            {
                for (int column = 0; column < BoardWidth - 3; column++)
                {
                    int winner = (Board[row][column] + Board[row][column + 1] + Board[row][column + 2] + Board[row][column + 3]) / 4;
                    if ((Board[row][column] + Board[row][column + 1] + Board[row][column + 2] + Board[row][column + 3]) % 4 == 0 && Board[row][column] == Board[row][column + 1] && Board[row][column] == Board[row][column + 2] && winner != 0)
                    {
                        return GetPlayerFromPlayerIndex((int)winner);
                    }
                }
            }

            // Vertical
            for (int row = 0; row < BoardHeight - 3; row++)
            {
                for (int column = 0; column < BoardWidth; column++)
                {
                    int winner = (Board[row][column] + Board[row + 1][column] + Board[row + 2][column] + Board[row + 3][column]) / 4;
                    if ((Board[row][column] + Board[row + 1][column] + Board[row + 2][column] + Board[row + 3][column]) % 4 == 0 && Board[row][column] == Board[row + 1][column] && Board[row][column] == Board[row + 2][column] && winner != 0)
                    {
                        return GetPlayerFromPlayerIndex((int)winner);
                    }
                }
            }

            // Diagonal Top Left To Bottom Right
            for (int row = 0; row < BoardHeight - 3; row++)
            {
                for (int column = 0; column < BoardWidth - 3; column++)
                {
                    int winner = (Board[row][column] + Board[row + 1][column + 1] + Board[row + 2][column + 2] + Board[row + 3][column + 3]) / 4;
                    if ((Board[row][column] + Board[row + 1][column + 1] + Board[row + 2][column + 2] + Board[row + 3][column + 3]) % 4 == 0 && Board[row][column] == Board[row + 1][column + 1] && Board[row][column] == Board[row + 2][column + 2] && winner != 0)
                    {
                        return GetPlayerFromPlayerIndex((int)winner);
                    }
                }
            }

            // Diagonal Bottom Left To Top Right
            for (int row = BoardHeight-1; row > 2; row--)
            {
                for (int column = 0; column < BoardWidth - 3; column++)
                {
                    int winner = (Board[row][column] + Board[row][column] + Board[row - 1][column + 1] + Board[row - 2][column + 2] + Board[row - 3][column + 3]) / 4;
                    if ((Board[row][column] + Board[row - 1][column + 1] + Board[row - 2][column + 2] + Board[row - 3][column + 3]) % 4 == 0 && Board[row][column] == Board[row - 1][column + 1] && Board[row][column] == Board[row - 2][column + 2] && winner != 0)
                    {
                        return GetPlayerFromPlayerIndex((int)winner);
                    }
                }
            }
            return null;
        }

        private FourWinGamePlayer GetPlayerFromPlayerIndex(int winner)
        {
            if (winner == 1)
            {
                return Player1;
            }
            if (winner == 2)
            {
                return Player2;
            }
            throw new IndexOutOfRangeException();
        }

        public void Resign(FourWinGamePlayer p)
        {
            int playerNum = 0;
            if (Player1.ID == p.ID)
                playerNum = 1;
            if (Player2.ID == p.ID)
                playerNum = 2;
            if (playerNum == 0)
                throw new PlayerNotInGameException();

            // Fill the board to avoid a draw if a player leaves
            for (int row = 0; row < BoardHeight; row++)
            {
                for (int column = 0; column < BoardWidth; column++)
                {
                    if (Board[row][column] == 0)
                    {
                        Board[row][column] = (playerNum - 1) * -1 + 2;
                    }
                }
            }
            InvokeGameFinished();
        }

        private void InvokeGameFinished()
        {
            FourWinGamePlayer winner = GetWinner();
            OnGameFinish?.Invoke(this, new GameWinnerEventArgs(winner));
        }

        public FourWinGamePlayer GetCurrentPlayer()
        {
            if (CurrentPlayer == 1)
            {
                return Player1;
            }
            else if (CurrentPlayer == 2)
            {
                return Player2;
            }
            throw new IndexOutOfRangeException();
        }

        public bool IsPlayerInGame(string playerID)
        {
            if (Player1.ID == playerID || Player2.ID == playerID)
            {
                return true;
            }
            return false;
        }

        public FourWinGamePlayer GetOpponent(FourWinGamePlayer player)
        {
            if (Player1.ID == player.ID)
            {
                return Player2;
            }
            if (Player2. ID == player.ID)
            {
                return Player1;
            }
            throw new PlayerNotInGameException();
        }

    }
}
