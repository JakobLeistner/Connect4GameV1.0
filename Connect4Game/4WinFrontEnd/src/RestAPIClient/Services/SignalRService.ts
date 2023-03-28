import { EventEmitter, Injectable, Output } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Player } from '../Contracts/Contracts';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    public hubProxy: any;
    public notifyGameFinished: EventEmitter<Player>; 
    public connection: any;
    public connectionID:any;
    constructor(){
        this.hubProxy = new signalR.HubConnectionBuilder();
        this.notifyGameFinished = new EventEmitter();
    }
    
    public ConnectGame(){
        this.hubProxy.on("GameFinished", (winner: any) => { 
            this.notifyGameFinished.emit(winner);
         });
         try{
            this.hubProxy
            .start()
            .then(() => { 
                this.connection = true;
                this.connectionID = this.hubProxy.connection.connectionID;
            });
         } catch (err){
            console.error("Couldn´t connect");
         }
        
    }
    
}