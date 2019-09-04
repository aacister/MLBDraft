import { DraftSelection } from './draftSelection.model';
import { DraftTeamRoster } from './draftTeamRoster.model';

import { Guid } from 'guid-typescript';

export class Draft {
    public id: Guid;
    public leagueId: Guid;
    public startDate: Date;
    public endDate: Date;
    public draftSelections: DraftSelection [];
    public draftTeamRosters : DraftTeamRoster [];


    constructor(id: Guid, leagueId: Guid, start: Date, end: Date, draftSelections: DraftSelection [], draftTeamRosters: DraftTeamRoster[]) {
        this.id = id;
        this.leagueId = leagueId;
        this.startDate = start;
        this.endDate = end;
        this.draftSelections = draftSelections;
        this.draftTeamRosters = draftTeamRosters;
    
      }
}