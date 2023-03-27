import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import { APIUriBuilder } from "./Services/APIUriBuilder";
import { APIHttpRequest } from "./Services/HttpRequest";

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
        .set("rtpconnentionid", rtpConnectionID)
        ));
    }
}