import {Guid } from 'guid-typescript';

export class Position {
    public id: Guid;
    public description: string;
    public abbreviation: string;

    constructor(id: Guid, desc: string, abbrev: string) {
        this.id = id;
        this.description = desc;
        this.abbreviation = abbrev;
    
      }
}