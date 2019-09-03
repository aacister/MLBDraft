import {Guid } from 'guid-typescript';

export class DraftSelection {
    public id: Guid
    public draftId: Guid;
    public teamId: Guid;
    public round: number;
    public playerId: Guid;


    constructor(id: Guid, draftId: Guid, teamId: Guid, round: number, playerId: Guid) {
        this.id = id;
        this.draftId = draftId;
        this.teamId = teamId;
        this.round = round;
        this.playerId = playerId;
      }
}