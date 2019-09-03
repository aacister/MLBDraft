import { MlbTeam } from './mlbTeam.model';
import { Position } from './position.model';
import { PlayerStat } from './playerStat.model';

import {Guid } from 'guid-typescript';

export class Player {
  public id: Guid;
  public firstName: string;
  public lastName: string;
  public fullName: string;
  public mlbTeam: MlbTeam;
  public position: Position;
  public homeRuns: string;
  public runs: string;
  public runsBattedIn: string;
  public stolenBases: string;
  public inningsPitched: string;
  public wins: string;
  public strikeouts: string;
  public earnedRunsAvg: string;
  public whip: string;
  public statistics: PlayerStat[];

  constructor(id: Guid, first: string, last: string, full: string, team: MlbTeam, position: Position, stats: PlayerStat[],
        hrs: string, runs: string, rbis: string, sbs: string, ips: string, wins: string, ks: string, era: string, whip: string) {
    this.id = id;
    this.firstName = first;
    this.lastName = last;
    this.fullName = full;
    this.mlbTeam = team;
    this.position = position;
    this.statistics = stats;
    this.homeRuns = hrs;
    this.runs = runs;
    this.runsBattedIn = rbis;
    this.stolenBases = sbs;
    this.inningsPitched = ips;
    this.wins = wins;
    this.strikeouts = ks;
    this.earnedRunsAvg = era;
    this.whip = whip;

  }
}
