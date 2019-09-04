import {Injectable } from '@angular/core';
import { Observable, BehaviorSubject} from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

import { ApiService } from './api.service';
import { League} from '../models';

@Injectable()
export class LeagueService {

    leagues: Observable<League[]>;
    private _leagues: BehaviorSubject<League[]>;
    private dataStore: { leagues: League[] };

    constructor(private apiService: ApiService){
        this.dataStore = { leagues: [] };
        this._leagues = new BehaviorSubject([]);
        this.leagues = this._leagues.asObservable();
    }

    getLeagues()  {
       return this.apiService.get('/leagues')
        .pipe(
            map((leagues: any[]) => {
                return leagues.map((league: any) => {
                    let leagueModel: League;
                    Object.assign(leagueModel, league );
                    return leagueModel;
                });
            }),
            tap(leagues => {
                this.dataStore.leagues = leagues;
                this._leagues.next(Object.assign({}, this.dataStore).leagues);
            })
        );
    }


    createLeague(league: League) {
        this.apiService.post('/leagues', { league: league })
          .subscribe(_ => {
          this.dataStore.leagues.push(league);
          this._leagues.next(Object.assign({}, this.dataStore).leagues);
        });
      }
    

    deleteLeague(id: Guid) {
        this.apiService.delete('/leagues/' + id).subscribe(_ => {
          this.dataStore.leagues.forEach((p, i) => {
            if (p.id === id) {
              this.dataStore.leagues.splice(i, 1);
            }
          });
          this._leagues.next(Object.assign({}, this.dataStore).leagues);
        });
      }
}