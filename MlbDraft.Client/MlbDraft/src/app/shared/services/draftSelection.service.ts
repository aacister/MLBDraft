import {Injectable } from '@angular/core';
import { Observable, BehaviorSubject} from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

import { ApiService } from './api.service';
import { DraftSelection } from '../models';

@Injectable()
export class DraftSelectionService {

    draftSelections: Observable<DraftSelection[]>;
    private _draftSelections: BehaviorSubject<DraftSelection[]>;
    private dataStore: { draftSelections: DraftSelection[] };

    constructor(private apiService: ApiService){
        this.dataStore = { draftSelections: [] };
        this._draftSelections = new BehaviorSubject([]);
        this.draftSelections = this._draftSelections.asObservable();
    }

    getDraftSelections(leagueId : Guid, draftId: Guid)  {
       return this.apiService.get('/leagues/' + leagueId + '/drafts/' + draftId + '/draftSelections')
        .pipe(
            map((draftSelections: any[]) => {
                return draftSelections.map((draftSelection: any) => {
                    let draftSelectionModel: DraftSelection;
                    Object.assign(draftSelectionModel, draftSelection );
                    return draftSelectionModel;
                });
            }),
            tap(draftSelections => {
                this.dataStore.draftSelections = draftSelections;
                this._draftSelections.next(Object.assign({}, this.dataStore).draftSelections);
            })
        );
    }

    updateDraftSelection(leagueId : Guid, draftId: Guid,  draftSelection: DraftSelection) {
        this.apiService.put('/leagues/' + leagueId + '/drafts/' + draftId + '/draftSelections', { draftSelection: draftSelection })
          .subscribe( data => {
          this.dataStore.draftSelections.forEach((ds, i) => {
              if(ds.id === data.id){
                  this.dataStore.draftSelections[i] = data;
              }
          });
          this._draftSelections.next(Object.assign({}, this.dataStore).draftSelections);
        });
      }
    
}