import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { APIInterface } from '../../RestAPIClient/APIInterface';
import { GameInfo, MyPlayer, Player } from '../../RestAPIClient/Contracts/Contracts';
import { PlayerHolder } from '../../RestAPIClient/Services/PlayerHolder';
import { SignalRService } from '../../RestAPIClient/Services/SignalRService';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})

export class GameComponent 
{


  // myPlayer: MyPlayer | undefined;


  public board: number[][] = 
      [
      [0,0,0,0,0,0,0], 
      [0,0,0,0,0,0,0], 
      [0,0,0,0,0,0,0], 
      [0,2,1,1,0,0,0], 
      [0,2,2,1,1,0,0], 
      [0,1,2,2,2,0,0] 
      ];
  
  public gameID: string = "";
  public opponent: Player|undefined;
  public yourMove: boolean = false;
  public playerNumber: number = 0; 

  myPlayer = this.playerHolder.myPlayer as MyPlayer;

  public player1name: string=this.myPlayer.playerName;
  public opponentName: string="";
  public winnerName: string="";

  public GameState: string="running";





//(private signalRService: SignalRService, private apiInterface: APIInterface)
    
    constructor(private playerHolder: PlayerHolder, private signalRService: SignalRService, public router: Router, public apiInterface: APIInterface, private route: ActivatedRoute)
  {

  }

  ngAfterViewInit(): void 
  { 
  this.route.queryParams.subscribe(params => {this.gameID = params["gameid"];});


   if (this.myPlayer == undefined){this.router.navigate(['/register']);};

  
   this.apiInterface.GetGameInfo(this.gameID, this.myPlayer.playerID).subscribe
   ({
     next: (resgameinfo: any) => 
     { 
      

       resgameinfo.gameInfo.board

       this.board=resgameinfo.gameInfo.board;
       this.opponent=resgameinfo.gameInfo.opponent;
       this.opponentName=resgameinfo.gameInfo.opponent.playerName;
       this.yourMove=resgameinfo.gameInfo.yourMove;
       this.playerNumber=resgameinfo.gameInfo.playerNumber;
     },
     error: (error: any) => {console.error(error);},
     complete: () => {}

   })



   
   this.signalRService.notifyGameUpdated.subscribe
   ({
     next: (res: any) => 
     { 

      this.apiInterface.GetGameInfo(res, this.myPlayer.playerID).subscribe
      ({

        next: (resgameinfo: any) => 
        { 
          resgameinfo.gameInfo.board

          this.board= resgameinfo.gameInfo.board;
          this.opponent=resgameinfo.gameInfo.opponent;
          this.opponentName=resgameinfo.gameInfo.opponent.playerName;
          this.yourMove=resgameinfo.gameInfo.yourMove;
          this.playerNumber=resgameinfo.gameInfo.playerNumber;
        },
        error: (error: any) => {console.error(error);},
        complete: () => {}

      })
      
     },
 
     error: (error: any) => {console.error(error);},
     complete: () => {}
 
   });




   
   this.signalRService.notifyGameFinished.subscribe
   ({
     next: (res: Player) => 
     { 
      this.winnerName=res.playerName;
        if(this.winnerName==this.player1name){this.GameState = "won";}
        if(this.winnerName==this.opponentName){this.GameState = "lost";}
        if(this.winnerName==undefined){this.GameState = "draw";}
        console.log(this.winnerName);
     },
 
     error: (error: any) => {console.error(error);},
     complete: () => {}
 
   });

  }

public ClickBackToLobby() :void
{
  this.router.navigate(['/queue']);
}




  //müsste man ausführen, wenn das event "Gamestartet" invoked wurde
  // public FixShite()
  // {
  //   if (this.playerholder.myPlayer != undefined)
  //   {
  //     this.playerName = this.playerholder.myPlayer.PlayerName;
  //   } 
  // }



    
  // signalRService.notifyGameFinished.subscribe({ /* Code */ });



 public ClickLeaveGameNow() :void
 {

  this.apiInterface.LeaveGame(this.myPlayer, this.gameID).subscribe()

 };

 public ClickMoveWasMade(col:number) :void
 {
  // this.apiInterface.DoMove(col,).subscribe

  
  if(this.yourMove==true)
  {
    this.apiInterface.DoMove(col, this.gameID, this.myPlayer).subscribe();
  }

 };






}
