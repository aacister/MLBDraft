import {NgModule} from '@angular/core';
import { RouterModule } from '@angular/router';

import {PlayersComponent } from './players.component';

@NgModule({
    imports: [
        RouterModule.forChild([{ path: '', component: PlayersComponent}])
    ],
    exports: [RouterModule]
})

export class PlayersRoutingModule{
    
}