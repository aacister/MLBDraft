import { NgModule } from "@angular/core";

import {PlayersComponent} from './players.component';
import {PlayersRoutingModule} from './players-routing.module';
import { CommonModule } from "@angular/common";

@NgModule({
    declarations: [PlayersComponent],
    imports: [CommonModule, PlayersRoutingModule]
})

export class PlayersModule{
    
}