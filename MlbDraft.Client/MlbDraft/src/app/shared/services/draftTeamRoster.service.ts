import {Injectable } from '@angular/core';
import { Observable, BehaviorSubject} from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

import { ApiService } from './api.service';
import { DraftTeamRoster } from '../models';

@Injectable()
export class DraftTeamRosterService {

    draftTeamRosters: Observable<DraftTeamRoster[]>;
    private _draftTeamRosters: BehaviorSubject<DraftTeamRoster[]>;
    private dataStore: { draftTeamRosters: DraftTeamRoster[] };

    constructor(private apiService: ApiService){
        this.dataStore = { draftTeamRosters: [] };
        this._draftTeamRosters = new BehaviorSubject([]);
        this.draftTeamRosters = this._draftTeamRosters.asObservable();
    }

    getDraftRosters(leagueId : Guid, draftId: Guid)  {
       return this.apiService.get('/leagues/' + leagueId + '/drafts/' + draftId + '/rosters')
        .pipe(
            map((draftTeamRosters: any[]) => {
                return draftTeamRosters.map((roster: any) => {
                    let teamRosterModel: DraftTeamRoster;
                    Object.assign(teamRosterModel, roster );
                    return teamRosterModel;
                });
            }),
            tap(draftTeamRosters => {
                this.dataStore.draftTeamRosters = draftTeamRosters;
                this._draftTeamRosters.next(Object.assign({}, this.dataStore).draftTeamRosters);
            })
        );
    }
}