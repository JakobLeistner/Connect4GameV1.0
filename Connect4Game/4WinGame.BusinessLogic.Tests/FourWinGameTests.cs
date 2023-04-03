using _4WinGame.BusinessLogic.Contracts.EventArguments;
using _4WinGame.BusinessLogic.Contracts.Exceptions;
using _4WinGame.BusinessLogic.Contracts.Interfaces;
using _4WinGame.BusinessLogic.Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _4WinGame.BusinessLogic.Tests
{
    [TestClass]
    public class FourWinGameTests
    {
        FourWinGamePlayer p1;
        FourWinGamePlayer p2;
        FourWinGame game;

        [TestInitialize]
        public void Init()
        {
            p1 = new FourWinGamePlayer("Alexander Marcus", "1");
            p2 = new FourWinGamePlayer("Player2", "2");
            game = new FourWinGame(p1, p2);
        }

        // GetName(): string
        [TestMethod]
        public void GetName_WhenNamesAreSame_NamesMatch()
        {
            string name = "Alexander Marcus";
            Assert.AreEqual(name, game.Player1.Name);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(-3)]
        public void MakeMove_WhenColumnIsOutOfRange_ShouldThrowArgumentOutOfRange(int col)
        {
            FourWinGamePlayer player = game.Player1;
            Assert.ThrowsException<BoardOutOfRangeException>(() => game.DoMove(col, player));
        }

        [TestMethod]
        public void MakeMove_WhenAColumnIsFull_ShouldThrowInvalidOperations()
        {
            FourWinGamePlayer player = game.Player1;
            int col = 2; // depends on Array Column's Number

            game.Board = new int[][]
            {
              new int[]{0,2,0,0,0,0,0},
              new int[]{0,1,0,0,0,0,0},
              new int[]{0,2,0,0,0,0,0},
              new int[]{0,1,0,0,0,0,0},
              new int[]{0,1,0,0,0,0,0},
              new int[]{0,2,0,0,0,0,0},
            };

            Assert.ThrowsException<BoardColumnIsFullException>(() => game.DoMove(col, player));
        }

        [TestMethod]
        public void DoMove_BoardOutOfRange()
        {
            Assert.ThrowsException<BoardOutOfRangeException>(() => game.DoMove(8, p1));
            game.CurrentPlayer = 2;
            Assert.ThrowsException<BoardOutOfRangeException>(() => game.DoMove(0, p2));


        }
        [TestMethod]
        public void DoMove_MoveNotPossibleBecauseItsNotPlayerMove()
        {
            game.CurrentPlayer = 1;
            Assert.ThrowsException<NotYourTurnException>(() => game.DoMove(1, p2));
        }
        [TestMethod]
        public void DoMove_PlayerNotInGame()
        {
            FourWinGamePlayer testplayer = new FourWinGamePlayer("Player2", "ID3");
            Assert.ThrowsException<PlayerNotInGameException>(() => game.DoMove(1, testplayer));
        }
        [TestMethod]
        [DataRow(1, 0, 3)]
        [DataRow(2, 1, 0)]
        [DataRow(3, 2, 1)]
        [DataRow(4, 3, 4)]
        [DataRow(5, 4, 2)]
        [DataRow(6, 5, 1)]
        [DataRow(7, 6, 5)]
        public void DoMove_CheckIfInsertIsInTheRightPlace(int insertcolumm, int expectedx, int expectedy)
        {
            game.Board = new int[][]
            {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,1,0,0,0,0,0},
                new int[]{0,2,2,0,0,2,0},
                new int[]{0,1,2,0,2,1,0},
                new int[]{1,1,1,0,1,2,0},
                new int[]{1,2,2,1,2,1,0},
            };
            game.DoMove(insertcolumm, p1);
            Assert.AreEqual(1, game.Board[expectedy][expectedx]);
        }
        [TestMethod]
        public void DoMove_OnGameStageChange()
        {
            bool raised = false;
            game.OnGameStateChange += (Sender, e) =>
            {   
                raised = true;
            };

            game.DoMove(3, p1);
            Assert.IsTrue(raised);

        }
        [TestMethod]
        public void DoMove_SetFullBoard_GameFinishedWithNullWinner()
        {
            game.Board = new int[][]
           {
                new int[]{0,1,2,1,2,1,1},
                new int[]{2,1,2,1,2,1,1},
                new int[]{1,2,2,2,1,2,2},
                new int[]{1,2,1,2,2,1,1},
                new int[]{2,1,1,1,2,1,2},
                new int[]{1,2,2,1,2,1,1},
           };
            bool raised = false;
            game.OnGameFinish += (Sender, e) =>
            {
                raised = true;
                GameWinnerEventArgs eventArgs = (GameWinnerEventArgs)e;
                Assert.AreSame(null, eventArgs.Player);
            };

            game.DoMove(1, p1);
            Assert.IsTrue(raised);
        }
        [TestMethod]
        public void DoMove__SetWinner_GameFinishedInvoked()
        {
            game.Board = new int[][]
           {
                new int[]{0,0,0,0,0,0,0},
                new int[]{1,1,0,0,0,0,0},
                new int[]{1,2,2,0,0,2,0},
                new int[]{1,1,2,0,2,1,0},
                new int[]{1,1,1,0,1,2,0},
                new int[]{2,2,0,0,0,1,0},
           };
            
            bool raised = false;
            game.OnGameFinish += (Sender, e) =>
            {
                raised = true;
                GameWinnerEventArgs eventArgs = (GameWinnerEventArgs)e;
                Assert.AreSame(p1, eventArgs.Player);
            };

            game.DoMove(1, p1);
            Assert.IsTrue(raised);
        }
        [TestMethod]
        public void DoMove__SetNotFinsished_GameFinishedInvoked()
        {
            game.Board = new int[][]
           {
                new int[]{0,0,0,0,0,0,0},
                new int[]{1,1,0,0,0,0,2},
                new int[]{1,2,2,0,0,2,2},
                new int[]{1,1,2,0,2,1,2},
                new int[]{1,1,1,0,1,2,2},
                new int[]{2,2,0,0,0,1,1},
           };

            bool raised = false;
            game.OnGameFinish += (Sender, e) =>
            {
                raised = true;
                GameWinnerEventArgs eventArgs = (GameWinnerEventArgs)e;
                Assert.AreNotSame(p1, null);
                Assert.AreNotSame(p2, null);
            };

            game.DoMove(1, p1);
            Assert.IsTrue(raised);
        }
        [TestMethod]
        public void ReSign_PlayerNotInGame()
        {

        }
        [TestMethod]
        public void GetCurrentPlayer_SetCurrentPlayerOne_ReturnsPlayerOne()
        {
            game.CurrentPlayer = 1;
            var player = new Contracts.Models.FourWinGamePlayer("Test", "ID");
            game.Player1 = player;
            Assert.AreEqual(player, game.GetCurrentPlayer());
        }

        [TestMethod]
        public void GetCurrentPlayer_SetCurrentPlayerTwo_ReturnsPlayerTwo()
        {
            game.CurrentPlayer = 2;
            var player = new Contracts.Models.FourWinGamePlayer("Test", "ID");
            game.Player2 = player;
            Assert.AreEqual(player, game.GetCurrentPlayer());
        }

        [TestMethod]
        public void GetWinner_HorizontalLine_WinnerPlayer1()
        {
            game.Board = new int[][]
          {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{1,1,1,1,0,0,0},
          };
            FourWinGamePlayer winner = game.GetWinner();
            Assert.AreEqual(winner, p1);

            game.Board = new int[][]
            {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,2,2,2,0,0,0},
                new int[]{2,1,2,2,1,0,0},
                new int[]{1,1,2,1,2,0,0},
             };
            FourWinGamePlayer winner2 = game.GetWinner();
            Assert.AreEqual(winner2, p2);
        }
        [TestMethod]
        public void GetWinner_Vertical_WinnerPlayer2()
        {
            game.Board = new int[][]
         {
                new int[]{0,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,1,0,1,0},
                new int[]{2,0,0,1,0,1,0},
                new int[]{1,0,0,1,0,1,0},
         };
            FourWinGamePlayer winner = game.GetWinner();
            Assert.AreEqual(winner, p2);

            game.Board = new int[][]
        {
                new int[]{0,0,0,0,0,2,0},
                new int[]{0,0,0,1,0,2,0},
                new int[]{0,0,0,1,0,2,0},
                new int[]{1,0,0,1,0,2,0},
                new int[]{2,0,0,1,1,1,0},
                new int[]{1,2,0,1,2,1,0},
        };
            FourWinGamePlayer winner4 = game.GetWinner();
            Assert.AreEqual(winner4, p2);

        }
        [TestMethod]
        public void GetWinner_DiagonalTopLeftToBottomRight_WinnerPlayer1()
        {
            game.Board = new int[][]
         {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,1,0,0,0,0},
                new int[]{1,2,2,1,0,0,0},
                new int[]{2,1,1,2,1,0,0},
                new int[]{2,2,2,1,2,1,0},
         };
            FourWinGamePlayer winner = game.GetWinner();
            Assert.AreEqual(winner, p1);

            game.Board = new int[][]
         {
                new int[]{2,0,0,0,0,0,0},
                new int[]{1,2,0,0,0,0,0},
                new int[]{2,1,2,0,0,0,0},
                new int[]{1,1,2,2,0,0,0},
                new int[]{2,2,1,2,0,0,0},
                new int[]{2,2,2,1,2,0,2},
         };
            FourWinGamePlayer winner5 = game.GetWinner();
            Assert.AreEqual(winner5, p2);
        }
        [TestMethod]
        public void GetWinner_DiagonalBottomLeftToTopRight_WinnerPlayer2()
        {
            game.Board = new int[][]
         {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,2,0,0},
                new int[]{0,0,0,2,1,0,0},
                new int[]{0,0,2,1,1,0,1},
                new int[]{0,2,2,2,2,0,1},
                new int[]{0,1,1,1,2,0,1},
         };
            FourWinGamePlayer winner = game.GetWinner();
            Assert.AreEqual(winner, p2);

            game.Board = new int[][]
         {
                new int[]{0,0,0,0,0,1,0},
                new int[]{0,0,0,0,1,2,0},
                new int[]{0,0,0,1,1,1,0},
                new int[]{0,0,1,1,2,2,1},
                new int[]{0,0,1,2,2,1,1},
                new int[]{1,1,1,2,1,1,1},
         };
            FourWinGamePlayer winner6 = game.GetWinner();
            Assert.AreEqual(winner6, p1);
        }

    }



}
