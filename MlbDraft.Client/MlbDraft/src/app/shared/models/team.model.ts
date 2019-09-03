import {Guid } from 'guid-typescript';

export class Team {
    public id: Guid;
    public name: string;
    public owner: string;
    public leaugueId: Guid;

    constructor(id: Guid, name: string, owner: string, leagueId: Guid) {
        this.id = id;
        this.name = name;
        this.owner = owner;
        this.leaugueId = leagueId;
    
      }
}