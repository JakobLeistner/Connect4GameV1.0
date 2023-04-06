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
        public void DoMove(int col, FourWinGamePlayer p)
        {
            string playerID = p.ID;
            if (Player1.ID != playerID && Player2.ID != playerID)
            {
                throw new PlayerNotInGameException();
            }
            if (Player1.ID == playerID && CurrentPlayer != 1 || Player2.ID == playerID && CurrentPlayer != 2)
            {
                throw new NotYourTurnException();
            }
            if (col < 1 || col > 7)
            {
                throw new BoardOutOfRangeException();
            }
            if (Board[0][col-1] != 0)
            {
                throw new BoardColumnIsFullException();
            }

            col--;
            int row = -1;
            for (int r = BoardHeight - 1; r >= 0; r--)
            {
                if (Board[r][col] == 0)
                {
                    row = r;
                    break;
                }
            }

            if (row == -1)
            {
                return;
            }
            Board[row][col] = CurrentPlayer;

            OnGameStateChange?.Invoke(this, new EventArgs());
            TogglePlayer();

            if (isBoardFull() || GetWinner() != null)
            {
                InvokeGameFinished();
            }
        }

        public void TogglePlayer()
        {
            if (CurrentPlayer == 1)
            {
                CurrentPlayer = 2;
            }
            else if (CurrentPlayer == 2)
            {
                CurrentPlayer = 1;
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
            bool win;

            // Überprüfen horizontal
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c <= 3; c++)
                {
                    int player = Board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (Board[r][c + i] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            // Überprüfen vertikal
            for (int c = 0; c < 7; c++)
            {
                for (int r = 0; r <= 2; r++)
                {
                    int player = Board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (Board[r + i][c] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            // Überprüfen diagonal "\"
            for (int c = 0; c <= 3; c++)
            {
                for (int r = 0; r <= 2; r++)
                {
                    int player = Board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (Board[r + i][c + i] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
                    }
                }
            }
            // Überprüfen diagonal "/"
            for (int c = 7 - 1; c >= 4 - 1; c--)
            {
                for (int r = 0; r <= 2; r++)
                {
                    int player = Board[r][c];
                    if (player == 0)
                    {
                        continue;
                    }

                    win = true;
                    for (int i = 1; i < 4; i++)
                    {
                        if (Board[r + i][c - i] != player)
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        return GetPlayerFromPlayerIndex(player);
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
            if (GetWinner() == null) 
            {
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
    }
}
