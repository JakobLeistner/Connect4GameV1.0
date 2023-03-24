import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})

//npm install @microsoft/signalr

export class SignalRService {
  public connected: boolean = false
  public hubProxy: any;
  public hubUrl: string;


  constructor() {
    this.hubUrl = window.location.origin;
    console.log(['hub url: ', this.hubUrl]);
    this.hubProxy = new signalR.HubConnectionBuilder()
    .withUrl(this.hubUrl + "/signalr")
    .withAutomaticReconnect()
    .build();

  }

  public startConnection = () => {
    console.log("Hier ist startConnection.")
    this.hubProxy.on("ReceiveMessage", (message: String) => {
      console.log("Message: "+ message)

    });

    try {
      this.hubProxy
        .start()
        .then(() => {
          console.log('Now connected, connection ID=' + this.hubProxy.id);
          this.connected = true;
        })
    } catch (err) {
      console.error('Could not connect');
    }
  }

  ngOnDestroy() {
    if (this.hubProxy) {
      this.hubProxy.stop();
    }
  }

  SendMessage() {
    try{
      this.hubProxy.invoke("SendMessage");
    } catch (err) {
      console.warn(`$SendMessage failed to invoke.`);
    }
  }
}

