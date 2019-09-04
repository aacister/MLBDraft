import {Injectable } from '@angular/core';
import { Observable, BehaviorSubject} from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

import { ApiService } from './api.service';
import { Player} from '../models';

@Injectable()
export class PlayerService {

    players: Observable<Player[]>;
    private _players: BehaviorSubject<Player[]>;
    private dataStore: { players: Player[] };

    constructor(private apiService: ApiService){
        this.dataStore = { players: [] };
        this._players = new BehaviorSubject([]);
        this.players = this._players.asObservable();
    }

    getPlayers()  {
       return this.apiService.get('/players')
        .pipe(
            map((players: any[]) => {
                return players.map((player: any) => {
                    let playerModel: Player;
                    Object.assign(playerModel, player );
                    return playerModel;
                });
            }),
            tap(players => {
                this.dataStore.players = players;
                this._players.next(Object.assign({}, this.dataStore).players);
            })
        );
    }

    createPlayer(player: Player) {
        this.apiService.post('/players', { player: player })
          .subscribe(_ => {
          this.dataStore.players.push(player);
          this._players.next(Object.assign({}, this.dataStore).players);
        });
      }
    

    deletePlayer(id: Guid) {
        this.apiService.delete('/players/' + id).subscribe(_ => {
          this.dataStore.players.forEach((p, i) => {
            if (p.id === id) {
              this.dataStore.players.splice(i, 1);
            }
          });
          this._players.next(Object.assign({}, this.dataStore).players);
        });
      }
}