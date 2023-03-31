import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { APIInterface } from '../../RestAPIClient/APIInterface';
import { MyPlayer, WaitingGame, WaitingGamesResponse } from '../../RestAPIClient/Contracts/Contracts';
import { PlayerHolder } from '../../RestAPIClient/Services/PlayerHolder';
import { SignalRService } from '../../RestAPIClient/Services/SignalRService';

@Component({
  selector: 'app-queue',
  templateUrl: './queue.component.html',
  styleUrls: ['./queue.component.scss']
})

export class QueueComponent 
{

  myPlayer = this.playerHolder.myPlayer as MyPlayer;
  queue: WaitingGame[] = [];
  nameQueue: string[] = [];

  
  constructor(public apiInterface: APIInterface, public playerHolder: PlayerHolder, public router: Router, public signalRService: SignalRService)
  { }

  public CreateGame()
  {
    this.apiInterface.CreateGame(this.playerHolder.myPlayer as MyPlayer).subscribe();
  }

  public JoinGame(waitingGameListIndex: number)
  {
    this.apiInterface.JoinGame(this.myPlayer, waitingGameListIndex).subscribe();
  }

  public SaveAndUpdateQueue() 
  { 
    this.apiInterface.GetWaitingGames().subscribe
    ({
      next: (response: any) => 
      { 
        this.queue = response.waitingGames;
        this.nameQueue = [];
        for(let i=0; i<this.queue.length; i++ )
        {
          this.nameQueue[i] = this.queue[i].playerName;
        }
      },
      error: (error: any) => {console.error(error);},
      complete: () => {}
    });
  }


  ngAfterViewInit()
  {
    this.SaveAndUpdateQueue();
    if (this.myPlayer == undefined) this.router.navigate(['/register']);

    this.signalRService.notifyWaitingListUpdated.subscribe(() => this.SaveAndUpdateQueue());
    this.signalRService.notifyGameStart.subscribe
    ({
      next: (resgameId: any) => 
      { 
        this.router.navigate(['/game'], { queryParams: { gameid: resgameId } });
      },
      error: (error: any) => {console.error(error);},
      complete: () => {}
    });
  }

}


