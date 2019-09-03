import {Guid } from 'guid-typescript';

export class TeamRoster {
    public id: Guid;
    public draftId: Guid;
    public teamId: Guid;
    public catcherId: Guid;
    public firstBaseId: Guid;
    public secondBaseId: Guid;
    public thirdBaseId: Guid;
    public shortStopId: Guid;
    public outfield1Id: Guid;
    public outfield2Id: Guid;
    public outfield3Id: Guid;
    public startingPitcherId: Guid;

    constructor(id: Guid, draftId: Guid, teamId: Guid, catcherId: Guid, firstBaseId: Guid, secondBaseId: Guid, thirdBaseId: Guid, shortStopId: Guid,
            outfield1Id: Guid, outfield2Id: Guid, outfield3Id: Guid, startingPitcherId: Guid) {
        this.id = id;
        this.draftId = draftId;
        this.teamId = teamId;
        this.catcherId = catcherId;
        this.firstBaseId = firstBaseId;
        this.secondBaseId = secondBaseId;
        this.shortStopId = shortStopId;
        this.thirdBaseId = thirdBaseId;
        this.outfield1Id = outfield1Id;
        this.outfield2Id = outfield2Id;
        this.outfield3Id = outfield3Id;
        this.startingPitcherId = startingPitcherId;
    
      }
}