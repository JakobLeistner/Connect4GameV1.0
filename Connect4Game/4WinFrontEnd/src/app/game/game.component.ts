import { Component } from '@angular/core';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})




export class GameComponent 
{
  constructor(signalRService)
  {
    this.signalRService.notifyGameStart.subscribe({ //Code });
  }









 public ClickLeaveGameNow()
 {
  console.log("left game");
 };

 public MakeMove()
 {
  console.log("Move was made");
  // ganzes board zurückgeben
  //
 };



}
