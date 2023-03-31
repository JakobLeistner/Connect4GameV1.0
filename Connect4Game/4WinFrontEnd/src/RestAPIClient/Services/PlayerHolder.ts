import { Injectable } from "@angular/core";
import { MyPlayer } from "../Contracts/Contracts";

@Injectable({
    providedIn: 'root'
})

export class PlayerHolder
{
  constructor(){}

  public myPlayer: MyPlayer|undefined;

  public SetPlayer(myPlayer: MyPlayer) :void
    {
        this.myPlayer = myPlayer;
    }
  
}
