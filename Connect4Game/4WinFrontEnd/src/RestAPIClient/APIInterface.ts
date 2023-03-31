import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import { APIUriBuilder } from "./Services/APIUriBuilder";
import { APIHttpRequest } from "./Services/HttpRequest";
import { MyPlayer, RegisterPlayerResponse, JoinGameResponse, WaitingGamesResponse, GameInfoResponse} from "./Contracts/Contracts"

@Injectable({
    providedIn: 'root'
})
export class APIInterface{
    private apiUriBuilder: APIUriBuilder;
    constructor(private apiHttpRequest:APIHttpRequest){
        this.apiUriBuilder = new APIUriBuilder("FourWinGame");
    }
    public RegisterPlayer(name: string, rtpConnectionID: string): Observable<RegisterPlayerResponse>{
        return this.apiHttpRequest.Post("",this.apiUriBuilder.GetAPIUri("RegisterPlayer",
        new Map()
        .set("name", name)
        .set("rtpconnectionid", rtpConnectionID)
        ));
    }
    public CreateGame(player: MyPlayer): Observable<any>{
        return this.apiHttpRequest.Post(player, this.apiUriBuilder.GetAPIUri("CreateGame",
        new Map()
        ));
    }
    public JoinGame(p1: MyPlayer, waitingGameListIndex: number): Observable<JoinGameResponse>{
        return this.apiHttpRequest.Post(p1, this.apiUriBuilder.GetAPIUri("JoinGame",
        new Map()
        .set("waitingGameListIndex", waitingGameListIndex)
        ));
    }
    public DoMove(column: number, gameID: string, player: MyPlayer): Observable<any>{
        return this.apiHttpRequest.Post(player, this.apiUriBuilder.GetAPIUri("DoMove",
        new Map()
        .set("column", column)
        .set("gameID", gameID)
        ));
    }
    public LeaveGame(player: MyPlayer, gameID: string): Observable<any>{
        return this.apiHttpRequest.Post(player, this.apiUriBuilder.GetAPIUri("LeaveGame",
        new Map()
        .set("gameID", gameID)
        ))
    }
    public GetWaitingGames(): Observable<WaitingGamesResponse>
    {
        let result = this.apiHttpRequest.Get(this.apiUriBuilder.GetAPIUri("GetWaitingGames", new Map()));
        return result;
    }

    public GetGameInfo(gameID: string, playerID: string): Observable<GameInfoResponse>{
        return this.apiHttpRequest.Get(this.apiUriBuilder.GetAPIUri("GetGameInfo",
        new Map()
        .set("gameID", gameID)
        .set("playerID", playerID)
        ))
    }
    public PlayerHasAlreadyWaitingGame(playerID: string): Observable<any>{
        return this.apiHttpRequest.Get(this.apiUriBuilder.GetAPIUri("PlayerHasAlreadyWaitingGame",
        new Map()
        .get(playerID)
        ))
    }
}