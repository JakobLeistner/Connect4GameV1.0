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

export class MyPlayer extends Player// :Player ?    [resolved: extends Player]
{
    PlayerID: string;
    RTPConnectionID: string;

    constructor(playerName: string, rtpConnectionID: string, playerID: string)
    {
        super(playerName); // playerName?   [resolved: super(playerName)]
        this.PlayerID = playerID;
        this.RTPConnectionID = rtpConnectionID;
    }
}

export class RegisterPlayerResponse
{
    RegisteredPlayer: MyPlayer;

    constructor(registeredPlayer: MyPlayer) // base(playerName) ?   [resolved: nope brauchen wir nicht]
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
    WaitingGames: WaitingGame[] // Array<WaitingGame>; // or WaitingGame[]  [resolved: yes WaitingGame[] needed]

    constructor(waitingGames: Array<WaitingGame>)
    {
        this.WaitingGames = waitingGames;
    }
}