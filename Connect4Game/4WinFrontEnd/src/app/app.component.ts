import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { PlayerHolder } from '../RestAPIClient/Services/PlayerHolder';
import { SignalRService } from '../RestAPIClient/Services/SignalRService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit
{
  title = '4WinFrontEnd';

  constructor(public signalRService: SignalRService, public router: Router, public playerHolder: PlayerHolder)
  { }

  ngOnInit(): void 
  {
    this.signalRService.ConnectGame();
    if (this.playerHolder.myPlayer == undefined) this.router.navigate(['/register']);
  }
}
