import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../RestAPIClient/Services/SignalRService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit
{
  title = '4WinFrontEnd';



  constructor(public signalRService: SignalRService)
  {
    
  }

  ngOnInit(): void 
  {
  this.signalRService.ConnectGame();
  
  }
}
