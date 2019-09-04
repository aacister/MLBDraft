import {Injectable } from '@angular/core';
import { Observable, BehaviorSubject} from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

import { ApiService } from './api.service';
import { Team } from '../models';

@Injectable()
export class TeamService {

    teams: Observable<Team[]>;
    private _teams: BehaviorSubject<Team[]>;
    private dataStore: { teams: Team[] };

    constructor(private apiService: ApiService){
        this.dataStore = { teams: [] };
        this._teams = new BehaviorSubject([]);
        this.teams = this._teams.asObservable();
    }

    getTeams(leagueId : Guid)  {
       return this.apiService.get('/leagues/' + leagueId + '/teams')
        .pipe(
            map((teams: any[]) => {
                return teams.map((team: any) => {
                    let teamModel: Team;
                    Object.assign(teamModel, team );
                    return teamModel;
                });
            }),
            tap(teams => {
                this.dataStore.teams = teams;
                this._teams.next(Object.assign({}, this.dataStore).teams);
            })
        );
    }

    createTeam(leagueId : Guid, team: Team) {
        this.apiService.post('/leagues/' + leagueId + '/teams', { team: team })
          .subscribe(_ => {
          this.dataStore.teams.push(team);
          this._teams.next(Object.assign({}, this.dataStore).teams);
        });
      }
    

    deleteTeam(leagueId: Guid, teamId: Guid) {
        this.apiService.delete('/leagues/' + leagueId + '/teams/' + teamId).subscribe(_ => {
          this.dataStore.teams.forEach((t, i) => {
            if (t.id === teamId) {
              this.dataStore.teams.splice(i, 1);
            }
          });
          this._teams.next(Object.assign({}, this.dataStore).teams);
        });
      }
}