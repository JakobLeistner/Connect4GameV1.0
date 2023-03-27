export class Player
{
    PlayerName: string;

    constructor(playerName: string)
    {
        this.PlayerName = playerName;
    }
}

export class GameInfo
{
    Board: number[][];
    Opponent: Player;
    YourMove: boolean;
    PlayerNumber: number;
    
    constructor(board: number[][], opponent: Player, yourMove: boolean, playerNumber: number)
    {
        this.Board = board;
        this.Opponent = opponent;
        this.YourMove = yourMove;
        this.PlayerNumber = playerNumber;
    }
}

export class GameInfoResponse
{
    GameInfo: GameInfo;

    constructor(gameInfo: GameInfo)
    {
        this.GameInfo = gameInfo;
    }
}

export class JoinGameResponse
{
    GameID: string;

    constructor(gameID: string)
    {
        this.GameID = gameID;
    }
}

export class MyPlayer // :Player ?
{
    PlayerID: string;
    RTPConnectionID: string;

    constructor(playerName: string, rtpConnectionID: string, playerID: string)
    {
        // playerName ?
        this.PlayerID = playerID;
        this.RTPConnectionID = rtpConnectionID;
    }
}

export class RegisterPlayerResponse
{
    RegisteredPlayer: MyPlayer;

    constructor(registeredPlayer: MyPlayer) // base(playerName) ?
    {
        this.RegisteredPlayer = registeredPlayer;
    }
}

export class WaitingGame
{
    PlayerName: string;

    constructor(playerName: string)
    {
        this.PlayerName = playerName;
    }
}

export class WaitingGameResponse
{
    WaitingGames: Array<WaitingGame>; // or WaitingGame[]

    constructor(waitingGames: Array<WaitingGame>)
    {
        this.WaitingGames = waitingGames;
    }
}