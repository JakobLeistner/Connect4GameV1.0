import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Routing
import { LoginComponent } from './login/login.component';
import { GameComponent } from './game/game.component';
import { QueueComponent } from './queue/queue.component';

const routes: Routes = [
  {path: '', redirectTo: 'register', pathMatch: 'full'},
  {path: 'register', component: LoginComponent},
  {path: 'queue', component: QueueComponent},
  {path: 'game', component: GameComponent}
]

@NgModule({
  declarations: [
    LoginComponent,
    GameComponent,
    QueueComponent
  ],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
