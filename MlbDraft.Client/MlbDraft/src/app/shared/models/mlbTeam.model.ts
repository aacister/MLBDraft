import {Guid } from 'guid-typescript';

export class MlbTeam {
    public id: Guid;
    public description: string;
    public abbreviation: string;
    public logoPath: string;

    constructor(id: Guid, desc: string, abbrev: string, logoPath: string) {
        this.id = id;
        this.description = desc;
        this.abbreviation = abbrev;
        this.logoPath = logoPath;
    
      }
}