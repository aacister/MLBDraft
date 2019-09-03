import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

var appRoutes = [
    { path: '', component: HomeComponent },
    { path: 'players', loadChildren: './players/players.module#PlayersModule' }
];

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
