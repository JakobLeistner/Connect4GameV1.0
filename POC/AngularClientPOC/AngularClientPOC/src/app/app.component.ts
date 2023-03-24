import { Component } from '@angular/core';
import { LoginAPIInterface } from './RestAPIClient/LoginAPIInterface';
import { SignalRService } from './SignalRClient/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularClientPOC';
  private signalRService: SignalRService ;
  private loginAPIInterface: LoginAPIInterface;
  
  public constructor(signalRService: SignalRService, loginAPIInterface: LoginAPIInterface){
    this.signalRService = signalRService;
    this.loginAPIInterface = loginAPIInterface;
  }
}
