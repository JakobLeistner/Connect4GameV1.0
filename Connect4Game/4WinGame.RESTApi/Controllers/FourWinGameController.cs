using _4WinGame.BusinessLogic.Contracts.EventArguments;
using _4WinGame.BusinessLogic.Contracts.Exceptions;
using _4WinGame.BusinessLogic.Contracts.Interfaces;
using _4WinGame.BusinessLogic.Contracts.Models;
using _4WinGame.RESTApi.Contracts.Exceptions;
using _4WinGame.RESTApi.Contracts.Models;
using _4WinGame.RESTApi.Hubs;
using _4WinGame.RESTApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FourWinGameController : ControllerBase
    {

        private ConnectionService connectionService;
        private IFourWinGamesService fourWinGameService;
        private FourWinGameEventHandler fourWinGameEventHandler;

        public FourWinGameController(ConnectionService connectionService, IFourWinGamesService fourWinGames, FourWinGameEventHandler fourWinGameEventHandler)
        {
            this.connectionService = connectionService;
            this.fourWinGameService = fourWinGames;
            this.fourWinGameEventHandler = fourWinGameEventHandler;
        }

        [HttpPost("RegisterPlayer")]
        public IActionResult RegisterPlayer([FromQuery] string name, [FromQuery] string RTPconnectionID)
        {
            if(connectionService.PlayerIDToConnectionIDlist.Where(c => c.Value == RTPconnectionID).Count()>0)
            {
                throw new ConnectionIDNotUniqueException("You are already logged in with an other account!");
            }

            MyPlayer player = new MyPlayer(name, RTPconnectionID, Guid.NewGuid().ToString());
            if(!connectionService.ConnectedIDs.Contains(RTPconnectionID))
            {
                throw new ConnectionIDNotFoundException();
            }
            connectionService.AddPlayer(player, player.PlayerID, RTPconnectionID);
            FourWinGamePlayer fourWinGamePlayer = new FourWinGamePlayer(player.PlayerName, player.PlayerID);
            fourWinGameService.AllPlayers.Add(fourWinGamePlayer);
            return Ok( new RegisterPlayerResponse(player));
        }

        [HttpPost("CreateGame")]
        public IActionResult CreateGame([FromBody] MyPlayer player)
        {
            FourWinGamePlayer found = fourWinGameService.AllPlayers.Where(p => p.ID == player.PlayerID).FirstOrDefault();
            if (found==null)
            {
                throw new PlayerNotFoundException();
            }
            if (fourWinGameService.WaitingGames.Where(p => p.ID == player.PlayerID).FirstOrDefault() != null)
            {
                throw new PlayerAlreadyCreatedWaitingGame();
            }
            fourWinGameService.WaitingGames.Add(found);
            fourWinGameService.OnWaitingListUpdated.Invoke(found, EventArgs.Empty);
            return Ok();
        }

        [HttpPost("JoinGame")]
        public IActionResult JoinGame([FromBody] MyPlayer p1, [FromQuery] int waitingGameListIndex)
        {
            FourWinGamePlayer found = fourWinGameService.AllPlayers.Where(player => player.ID == p1.PlayerID).FirstOrDefault();
            FourWinGamePlayer gameCreatedPlayer = fourWinGameService.WaitingGames.ElementAt(waitingGameListIndex);
            if (found==null) {
                throw new PlayerNotFoundException();
            }
            if(waitingGameListIndex-1 > fourWinGameService.WaitingGames.Count)
            {
                throw new WaitingListEntryNotFoundException();
            }
            if(found.ID==gameCreatedPlayer.ID)
            {
                throw new PlayerAgainsItselfException();
            }
            /*if (fourWinGame.)
            {
                throw new PlayerAgainsItselfException();
            }*/
            IFourWinGame fourWinGame = fourWinGameService.JoinWaitingGame(gameCreatedPlayer, found);
            fourWinGame.OnGameStateChange += fourWinGameEventHandler.OnGameStateChange;
            fourWinGame.OnGameFinish += fourWinGameEventHandler.OnGameFinish;
            return Ok(new JoinGameResponse(fourWinGame.ID));
        }


        [HttpPost("DoMove")]
        public IActionResult DoMove([FromQuery] int column, [FromQuery] string gameID, [FromBody] MyPlayer player)
        {
            IFourWinGame fourWinGame;
            try
            {
                fourWinGame = fourWinGameService.GetGameByID(gameID);
            }
            catch (_4WinGame.BusinessLogic.Contracts.Exceptions.GameNotFoundException)
            {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.GameNotFoundException();
            }
                      
            FourWinGamePlayer found = fourWinGameService.AllPlayers.Where(p => p.ID == player.PlayerID).FirstOrDefault();
            if (found==null) {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.PlayerNotFoundException();
            } 
            
            try
            {
                fourWinGame.DoMove(column, found);
            }
            catch (BoardColumnIsFullException e)
            {

                throw new DoMoveException("The column is already full", e);
            }
            catch (BoardOutOfRangeException e)
            {
                throw new DoMoveException("The column is out of range!", e);
            }
            catch (NotYourTurnException e)
            {
                throw new DoMoveException("It is not your move!", e);
            }
            catch (_4WinGame.BusinessLogic.Contracts.Exceptions.PlayerNotInGameException e)
            {
                throw new DoMoveException("The searched player is not in the game", e);
            }
            return Ok();
        }

        [HttpPost("LeaveGame")]
        public IActionResult LeaveGame([FromBody] MyPlayer player, [FromQuery] string gameID)
        {
            IFourWinGame fourWinGame;
            try
            {
                fourWinGame = fourWinGameService.GetGameByID(gameID);
            }
            catch (_4WinGame.BusinessLogic.Contracts.Exceptions.GameNotFoundException)
            {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.GameNotFoundException();
            }
            try
            {
                FourWinGamePlayer found = fourWinGameService.AllPlayers.Where(p => p.ID==player.PlayerID).FirstOrDefault();
                if (found==null)
                {
                    throw new PlayerNotFoundException();
                }
                fourWinGameService.LeaveActiveGame(found, gameID);
            }
            catch (BusinessLogic.Contracts.Exceptions.PlayerNotInGameException)
            {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.PlayerNotInGameException();
            }
            return Ok();
        }

        [HttpGet("GetWaitingGames")]
        public IActionResult GetWaitingGames()
        {
            return Ok(new WaitingGamesResponse(fourWinGameService.WaitingGames.Select(player => new WaitingGame(player.Name)).ToList()));
        }

        [HttpGet("GetGameInfo")]
        public IActionResult GetGameInfo([FromQuery] string gameID, [FromQuery] string playerID)
        {
            IFourWinGame fourWinGame;
            try
            {
                fourWinGame = fourWinGameService.GetGameByID(gameID);
            }
            catch (_4WinGame.BusinessLogic.Contracts.Exceptions.GameNotFoundException)
            {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.GameNotFoundException();
            }
            FourWinGamePlayer found = fourWinGameService.AllPlayers.Where(p => p.ID == playerID).FirstOrDefault();
            if(found==null)
            {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.PlayerNotFoundException();
            }
            if(fourWinGame.Player1.ID != playerID &&
                fourWinGame.Player2.ID != playerID)
            {
                throw new _4WinGame.RESTApi.Contracts.Exceptions.PlayerNotInGameException();
            }
            int playerNumber = 0;
            if(fourWinGame.Player1.ID == playerID)
            {
                playerNumber = 1;
            } else
            {
                playerNumber = 2;
            }
            bool yourmove = (playerID==fourWinGame.GetCurrentPlayer().ID) ? true : false;
            FourWinGamePlayer fourWinGamePlayerOpponent = fourWinGame.GetOpponent(found);
            MyPlayer opponent = connectionService.AllPlayers.Where(p => p.PlayerID == fourWinGamePlayerOpponent.ID).FirstOrDefault();
            return Ok(new GameInfoResponse(new GameInfo(fourWinGame.Board, (Player)opponent, yourmove, playerNumber)));
        }    
        
        [HttpGet("PlayerHasAlreadyWaitingGame")]
        public bool PlayerHasAlreadyWaitingGame([FromQuery]string playerID)
        {
            return true;
        }

    }
}
