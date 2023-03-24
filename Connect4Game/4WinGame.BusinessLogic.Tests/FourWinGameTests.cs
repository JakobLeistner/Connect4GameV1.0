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
        private FourWinGame fourWinGame;
        private FourWinGamePlayer p1;
        private FourWinGamePlayer p2;
        



        [TestInitialize]
        public void Init()
        {

            p1 = new FourWinGamePlayer("Player1", "ID");
            p2 = new FourWinGamePlayer("Player2", "ID2");
            fourWinGame = new FourWinGame(p1, p2);

        }

        [TestMethod]
        public void DoMove_FieldWhereColummIsFull_ColummISFullException()
        {
            fourWinGame.Board = new int[][]
            {
                new int[]{0,2,0,0,0,0,0},
                new int[]{0,1,0,0,0,0,0},
                new int[]{0,2,0,0,0,0,0},
                new int[]{0,1,0,0,0,0,0},
                new int[]{0,1,0,0,0,0,0},
                new int[]{0,2,0,0,0,0,0},
            };
            Assert.ThrowsException<BoardColumnIsFullException>(() => fourWinGame.DoMove(2, p1));

        }

        [TestMethod]
        public void DoMove_BoardOutOfRange()
        {
            Assert.ThrowsException<BoardOutOfRangeException>(() => fourWinGame.DoMove(8, p1));
            fourWinGame.CurrentPlayer = 2;
            Assert.ThrowsException<BoardOutOfRangeException>(() => fourWinGame.DoMove(0, p2));


        }
        [TestMethod]
        public void DoMove_MoveNotPossibleBecauseItsNotPlayerMove()
        {
            fourWinGame.CurrentPlayer = 1;
            Assert.ThrowsException<NotYourTurnException>(() => fourWinGame.DoMove(1, p2));
        }
        [TestMethod]
        public void DoMove_PlayerNotInGame()
        {
            FourWinGamePlayer testplayer = new FourWinGamePlayer("Player2", "ID3");
            Assert.ThrowsException<PlayerNotInGameException>(() => fourWinGame.DoMove(1, testplayer));
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
            fourWinGame.Board = new int[][]
            {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,1,0,0,0,0,0},
                new int[]{0,2,2,0,0,2,0},
                new int[]{0,1,2,0,2,1,0},
                new int[]{1,1,1,0,1,2,0},
                new int[]{1,2,2,1,2,1,0},
            };
            fourWinGame.DoMove(insertcolumm, p1);
            Assert.AreEqual(1, fourWinGame.Board[expectedy][expectedx]);
        }
        [TestMethod]
        public void DoMove_OnGameStageChange()
        {
            bool raised = false;
            fourWinGame.OnGameStateChange += (Sender, e) =>
            {   
                raised = true;
            };

            fourWinGame.DoMove(3, p1);
            Assert.IsTrue(raised);

        }
        [TestMethod]
        public void DoMove_SetFullBoard_GameFinishedWithNullWinner()
        {
            fourWinGame.Board = new int[][]
           {
                new int[]{0,1,2,1,2,1,1},
                new int[]{2,1,2,1,2,1,1},
                new int[]{1,2,2,2,1,2,2},
                new int[]{1,2,1,2,2,1,1},
                new int[]{2,1,1,1,2,1,2},
                new int[]{1,2,2,1,2,1,1},
           };
            bool raised = false;
            fourWinGame.OnGameFinish += (Sender, e) =>
            {
                raised = true;
                GameWinnerEventArgs eventArgs = (GameWinnerEventArgs)e;
                Assert.AreSame(null, eventArgs.Player);
            };

            fourWinGame.DoMove(1, p1);
            Assert.IsTrue(raised);
        }
        [TestMethod]
        public void DoMove__SetWinner_GameFinishedInvoked()
        {
            fourWinGame.Board = new int[][]
           {
                new int[]{0,0,0,0,0,0,0},
                new int[]{1,1,0,0,0,0,0},
                new int[]{1,2,2,0,0,2,0},
                new int[]{1,1,2,0,2,1,0},
                new int[]{1,1,1,0,1,2,0},
                new int[]{2,2,0,0,0,1,0},
           };
            
            bool raised = false;
            fourWinGame.OnGameFinish += (Sender, e) =>
            {
                raised = true;
                GameWinnerEventArgs eventArgs = (GameWinnerEventArgs)e;
                Assert.AreSame(p1, eventArgs.Player);
            };

            fourWinGame.DoMove(1, p1);
            Assert.IsTrue(raised);
        }
        [TestMethod]
        public void DoMove__SetNotFinsished_GameFinishedInvoked()
        {
            fourWinGame.Board = new int[][]
           {
                new int[]{0,0,0,0,0,0,0},
                new int[]{1,1,0,0,0,0,2},
                new int[]{1,2,2,0,0,2,2},
                new int[]{1,1,2,0,2,1,2},
                new int[]{1,1,1,0,1,2,2},
                new int[]{2,2,0,0,0,1,1},
           };

            bool raised = false;
            fourWinGame.OnGameFinish += (Sender, e) =>
            {
                raised = true;
                GameWinnerEventArgs eventArgs = (GameWinnerEventArgs)e;
                Assert.AreNotSame(p1, null);
                Assert.AreNotSame(p2, null);
            };

            fourWinGame.DoMove(1, p1);
            Assert.IsTrue(raised);
        }
        [TestMethod]
        public void ReSign_PlayerNotInGame()
        {

        }
        [TestMethod]
        public void GetCurrentPlayer_SetCurrentPlayerOne_ReturnsPlayerOne()
        {
            fourWinGame.CurrentPlayer = 1;
            var player = new Contracts.Models.FourWinGamePlayer("Test", "ID");
            fourWinGame.Player1 = player;
            Assert.AreEqual(player, fourWinGame.GetCurrentPlayer());
        }

        [TestMethod]
        public void GetCurrentPlayer_SetCurrentPlayerTwo_ReturnsPlayerTwo()
        {
            fourWinGame.CurrentPlayer = 2;
            var player = new Contracts.Models.FourWinGamePlayer("Test", "ID");
            fourWinGame.Player2 = player;
            Assert.AreEqual(player, fourWinGame.GetCurrentPlayer());
        }
        [TestMethod]
        public void GetOpponent_SetToPlayer1_ReturnPlayer2()
        {
            fourWinGame.GetOpponent(p2);
            var player = new Contracts.Models.FourWinGamePlayer("Test", "ID");
            fourWinGame.Player1 = player;
            fourWinGame.Player2 = p2;
            Assert.AreEqual(player, fourWinGame.GetOpponent(p2));

        }

        [TestMethod]
        public void GetOpponent_SetToPlayer2_ReturnPlayer1()
        {
            fourWinGame.GetOpponent(p1);
            var player = new Contracts.Models.FourWinGamePlayer("Test", "ID");
            fourWinGame.Player2 = player;
            fourWinGame.Player1 = p1;
            Assert.AreEqual(player, fourWinGame.GetOpponent(p1));
        }

        [TestMethod]
        public void GetWinner_HorizontalLine_WinnerPlayer1()
        {
            fourWinGame.Board = new int[][]
          {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{1,1,1,1,0,0,0},
          };
            FourWinGamePlayer winner = fourWinGame.GetWinner();
            Assert.AreEqual(winner, p1);

            fourWinGame.Board = new int[][]
            {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,2,2,2,0,0,0},
                new int[]{2,1,2,2,1,0,0},
                new int[]{1,1,2,1,2,0,0},
             };
            FourWinGamePlayer winner2 = fourWinGame.GetWinner();
            Assert.AreEqual(winner2, p2);
        }
        [TestMethod]
        public void GetWinner_Vertical_WinnerPlayer2()
        {
            fourWinGame.Board = new int[][]
         {
                new int[]{0,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,0,0,0,0},
                new int[]{2,0,0,1,0,1,0},
                new int[]{2,0,0,1,0,1,0},
                new int[]{1,0,0,1,0,1,0},
         };
            FourWinGamePlayer winner = fourWinGame.GetWinner();
            Assert.AreEqual(winner, p2);

            fourWinGame.Board = new int[][]
        {
                new int[]{0,0,0,0,0,2,0},
                new int[]{0,0,0,1,0,2,0},
                new int[]{0,0,0,1,0,2,0},
                new int[]{1,0,0,1,0,2,0},
                new int[]{2,0,0,1,1,1,0},
                new int[]{1,2,0,1,2,1,0},
        };
            FourWinGamePlayer winner4 = fourWinGame.GetWinner();
            Assert.AreEqual(winner4, p2);

        }
        [TestMethod]
        public void GetWinner_DiagonalTopLeftToBottomRight_WinnerPlayer1()
        {
            fourWinGame.Board = new int[][]
         {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,1,0,0,0,0},
                new int[]{1,2,2,1,0,0,0},
                new int[]{2,1,1,2,1,0,0},
                new int[]{2,2,2,1,2,1,0},
         };
            FourWinGamePlayer winner = fourWinGame.GetWinner();
            Assert.AreEqual(winner, p1);

            fourWinGame.Board = new int[][]
         {
                new int[]{2,0,0,0,0,0,0},
                new int[]{1,2,0,0,0,0,0},
                new int[]{2,1,2,0,0,0,0},
                new int[]{1,1,2,2,0,0,0},
                new int[]{2,2,1,2,0,0,0},
                new int[]{2,2,2,1,2,0,2},
         };
            FourWinGamePlayer winner5 = fourWinGame.GetWinner();
            Assert.AreEqual(winner5, p2);
        }
        [TestMethod]
        public void GetWinner_DiagonalBottomLeftToTopRight_WinnerPlayer2()
        {
            fourWinGame.Board = new int[][]
         {
                new int[]{0,0,0,0,0,0,0},
                new int[]{0,0,0,0,2,0,0},
                new int[]{0,0,0,2,1,0,0},
                new int[]{0,0,2,1,1,0,1},
                new int[]{0,2,2,2,2,0,1},
                new int[]{0,1,1,1,2,0,1},
         };
            FourWinGamePlayer winner = fourWinGame.GetWinner();
            Assert.AreEqual(winner, p2);

            fourWinGame.Board = new int[][]
         {
                new int[]{0,0,0,0,0,1,0},
                new int[]{0,0,0,0,1,2,0},
                new int[]{0,0,0,1,1,1,0},
                new int[]{0,0,1,1,2,2,1},
                new int[]{0,0,1,2,2,1,1},
                new int[]{1,1,1,2,1,1,1},
         };
            FourWinGamePlayer winner6 = fourWinGame.GetWinner();
            Assert.AreEqual(winner6, p1);
        }

    }



}
