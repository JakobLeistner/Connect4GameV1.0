export class Player
{
    playerName: string;

    constructor(playerName: string)
    {
        this.playerName = playerName;
    }
}

export class GameInfo
{
    board: number[][];
    opponent: Player;
    yourMove: boolean;
    playerNumber: number;
    
    constructor(board: number[][], opponent: Player, yourMove: boolean, playerNumber: number)
    {
        this.board = board;
        this.opponent = opponent;
        this.yourMove = yourMove;
        this.playerNumber = playerNumber;
    }
}

export class GameInfoResponse
{
    gameInfo: GameInfo;

    constructor(gameInfo: GameInfo)
    {
        this.gameInfo = gameInfo;
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
    playerID: string;
    rtpConnectionID: string;

    constructor(playerName: string, rtpConnectionID: string, playerID: string)
    {
        super(playerName); // playerName?   [resolved: super(playerName)]
        this.playerID = playerID;
        this.rtpConnectionID = rtpConnectionID;
    }
}

export class RegisterPlayerResponse
{
    registeredPlayer: MyPlayer;

    constructor(registeredPlayer: MyPlayer) // base(playerName) ?   [resolved: nope brauchen wir nicht]
    {
        this.registeredPlayer = registeredPlayer;
    }
}

export class WaitingGame
{
    playerName: string;

    constructor(playerName: string)
    {
        this.playerName = playerName;
    }
}

export class WaitingGamesResponse
{
    waitingGames: WaitingGame[] // Array<WaitingGame>; // or WaitingGame[]  [resolved: yes WaitingGame[] needed]

    constructor(waitingGames: Array<WaitingGame>)
    {
        this.waitingGames = waitingGames;
    }
}