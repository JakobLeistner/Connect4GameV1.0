import { EventEmitter, Injectable, Output } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    public hubProxy: any;
    public notifyGameStart: EventEmitter<any>; 
    public notifyGameUpdated: EventEmitter<any>; 
    public notifyGameFinished: EventEmitter<any>; 
    public notifyWaitingListUpdated: EventEmitter<any>; 
    public connection: any;
    public connectionId:any;
    // changed from https to http to fix ssl certificate error on server
    // and now changed it back to https
    hubUrl: string = "http://192.168.0.1:93";
    constructor()
    {
        this.hubProxy = new signalR.HubConnectionBuilder()
        .withUrl(this.hubUrl + "/fourwingamehub")
        .withAutomaticReconnect()
        .build();
        this.notifyGameStart = new EventEmitter();
        this.notifyGameUpdated = new EventEmitter(); 
        this.notifyGameFinished = new EventEmitter(); 
        this.notifyWaitingListUpdated = new EventEmitter();
    }
    
    public ConnectGame(){

        this.hubProxy.on("GameStart", (gameid: string) => { 
            this.notifyGameStart.emit(gameid);
         });

         this.hubProxy.on("GameUpdated", (gameid: string) => { 
            this.notifyGameUpdated.emit(gameid);
         });
    
        
         this.hubProxy.on("GameFinished", (winner: any) => { 

            this.notifyGameFinished.emit(winner);
         });

         this.hubProxy.on("WaitingListUpdated", () => { 
            this.notifyWaitingListUpdated.emit();
         });



         try{
            this.hubProxy
            .start()
            .then(() => { 
                this.connection = true;
                this.connectionId = this.hubProxy.connection.connectionId;
            });
         } catch (err){
            console.error("Couldn't connect");
         }
        
    }
    
}