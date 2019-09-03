import { Team } from './team.model';
import { Draft } from './draft.model';

import {Guid } from 'guid-typescript';

export class League {
    public id: Guid;
    public name: string;
    public minTeams: number;
    public maxTeams: number;
    public teams : Team[];
    public drafts: Draft[];


    constructor(id: Guid, name: string, minTeams: number, maxTeams: number, teams: Team[], drafts: Draft[]) {
        this.id = id;
        this.name = name;
        this.minTeams = minTeams;
        this.maxTeams = maxTeams;
        this.teams = teams;
        this.drafts = drafts;
    
      }
}