import { Component, OnInit } from '@angular/core';

@Component({
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{

    public message: string;
    public signature: string;

    constructor(){

    }

    ngOnInit(){
        this.message = "Mlb Draft is currently work in progress.  MLBDraft is built utilizing an ASP.NET Core 2 api, and Angular 7 client. " +
            "MLBDraft is used to simulate online fantasy baseball snake drafts. Create your team, join a league, and simulate a draft, selecting from a pool of players.";
    
        this.signature = "-- Andrew A. Cisternino";
    }
}