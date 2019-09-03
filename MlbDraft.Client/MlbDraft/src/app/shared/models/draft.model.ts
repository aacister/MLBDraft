import { DraftSelection } from './draftSelection.model';
import { TeamRoster } from './teamRoster.model';

import { Guid } from 'guid-typescript';

export class Draft {
    public id: Guid;
    public leagueId: Guid;
    public startDate: Date;
    public endDate: Date;
    public draftSelections: DraftSelection [];
    public teamRosters : TeamRoster [];


    constructor(id: Guid, leagueId: Guid, start: Date, end: Date, draftSelections: DraftSelection [], teamRosters: TeamRoster[]) {
        this.id = id;
        this.leagueId = leagueId;
        this.startDate = start;
        this.endDate = end;
        this.draftSelections = draftSelections;
        this.teamRosters = teamRosters;
    
      }
}