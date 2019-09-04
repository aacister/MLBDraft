import {Injectable } from '@angular/core';
import { Observable, BehaviorSubject} from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

import { ApiService } from './api.service';
import { Draft } from '../models';

@Injectable()
export class DraftService {

    drafts: Observable<Draft[]>;
    private _drafts: BehaviorSubject<Draft[]>;
    private dataStore: { drafts: Draft[] };

    constructor(private apiService: ApiService){
        this.dataStore = { drafts: [] };
        this._drafts = new BehaviorSubject([]);
        this.drafts = this._drafts.asObservable();
    }

    getDrafts(leagueId : Guid)  {
       return this.apiService.get('/leagues/' + leagueId + '/drafts')
        .pipe(
            map((drafts: any[]) => {
                return drafts.map((draft: any) => {
                    let draftModel: Draft;
                    Object.assign(draftModel, draft );
                    return draftModel;
                });
            }),
            tap(drafts => {
                this.dataStore.drafts = drafts;
                this._drafts.next(Object.assign({}, this.dataStore).drafts);
            })
        );
    }

    createDraft(leagueId : Guid, draft: Draft) {
        this.apiService.post('/leagues/' + leagueId + '/drafts', { draft: draft })
          .subscribe(_ => {
          this.dataStore.drafts.push(draft);
          this._drafts.next(Object.assign({}, this.dataStore).drafts);
        });
      }
    
}