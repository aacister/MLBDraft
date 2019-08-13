using System;
using System.Collections.Generic;
using MLBDraft.API.Entities;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;
using MLBDraft.API.Security;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace MLBDraft.API.Entities
{
    public static class MLBDraftContextExtensions
    {
  
        public static void EnsureSeedDataForContext(this MLBDraftContext context){

            context.DraftSelections.RemoveRange(context.DraftSelections);
            context.SaveChanges();

            context.Drafts.RemoveRange(context.Drafts);
            context.SaveChanges();
            
            context.Players.RemoveRange(context.Players);
            context.SaveChanges();

            context.StatCategories.RemoveRange(context.StatCategories);
            context.SaveChanges();

            context.MlbTeams.RemoveRange(context.MlbTeams);
            context.SaveChanges();

            context.Positions.RemoveRange(context.Positions);
            context.SaveChanges();
            
            context.Teams.RemoveRange(context.Teams);
            context.SaveChanges();

            context.Leagues.RemoveRange(context.Leagues);
            context.SaveChanges();

            context.Users.RemoveRange(context.Users);
            context.SaveChanges();

            var user = new User();
            user.Username = "username";
            byte[] salt = new byte[24];
            var keyGenerator = RandomNumberGenerator.Create();
            keyGenerator.GetBytes(salt);
            user.Salt = salt;
            IPasswordHasher _passwordHasher = new PasswordHasher();
            user.Hash = _passwordHasher.Hash("password", user.Salt);

            context.Users.Add(user);
            context.SaveChanges();
            

            var mlbTeams = new List<MlbTeam>()
            {
                new MlbTeam(){
                    Id = new Guid("611d6452-7a69-4d81-9cdc-a07d171afac1"),
                    Description = "Anaheim Angels",
                    Abbreviation = "ANA",
                    LogoPath = "la_angels.gif"
                },
                new MlbTeam()
                {
                    Id= new Guid("5c52309d-c5eb-4bdd-a944-479184435096"),
                    Description = "Arizona Diamondbacks",
                    Abbreviation = "ARI",
                    LogoPath= "arizona_diamondbacks.gif"
                },
                new MlbTeam(){
                    Id = new Guid("9340a994-fae9-4b16-b091-1a22df63f02b"),
                    Description = "Atlanta Braves",
                    Abbreviation = "ATL",
                    LogoPath = "atlanta_braves.gif"
                },
                new MlbTeam(){
                    Id = new Guid("9af12d76-8dd8-4d77-83b9-50fae8c4e1a0"),
                    Description = "Baltimore Orioles",
                    Abbreviation = "BAL",
                    LogoPath = "baltimore_orioles.gif"
                },
                new MlbTeam(){
                    Id = new Guid("7614b3ae-b661-4dbb-929a-c5f4c3804248"),
                    Description = "Boston Red Sox",
                    Abbreviation = "BOS",
                    LogoPath = "boston_redsox.gif"
                },
                new MlbTeam(){
                    Id = new Guid("a4cddcc7-adc3-471b-acb6-d5dd9e440460"),
                    Description = "Chicago Cubs",
                    Abbreviation = "CHC",
                    LogoPath = "chicago_cubs.gif"
                },
                new MlbTeam(){
                    Id = new Guid("b923f780-80e5-4e65-8161-eafe8046b980"),
                    Description = "Chicago White Sox",
                    Abbreviation = "CWS",
                    LogoPath = "chicago_whitesox.gif"
                },
                new MlbTeam(){
                    Id = new Guid("60909f72-1fc5-4fb7-8a55-40390db127d9"),
                    Description = "Cincinnati Reds",
                    Abbreviation = "CIN",
                    LogoPath = "cincinnati_reds.gif"
                },
                new MlbTeam() {
                    Id = new Guid("5877ed68-be79-4871-898a-edfa4fdefcad"),
                    Description = "Cleveland Indians",
                    Abbreviation = "CLE",
                    LogoPath = "cleveland_indians.gif"
                },
                new MlbTeam(){
                    Id = new Guid("8683bd02-8b00-4203-8ccb-d38698ae02c4"),
                    Description = "Colorado Rockies",
                    Abbreviation = "COL",
                    LogoPath = "colorado_rockies.gif"
                },
                new MlbTeam(){
                    Id  = new Guid("a6b60598-3749-4b4a-a6fd-e91bb2cdcd35"),
                    Description = "Detroit Tigers",
                    Abbreviation = "DET",
                    LogoPath = "detroit_tigers.gif"
                },
                new MlbTeam(){
                    Id = new Guid("88d0421c-913c-4dbe-a86f-bfa1ac392e38"),
                    Description = "Houston Astros",
                    Abbreviation = "HOU",
                    LogoPath = "houston_astros.gif"
                },
                new MlbTeam(){
                    Id = new Guid("23aa79c5-2ae1-4b0b-8dc9-8d14f6eaf82c"),
                    Description = "Kansas City Royals",
                    Abbreviation = "KC",
                    LogoPath = "kansascity_royals.gif"
                },
                
                new MlbTeam(){
                    Id = new Guid("03b71a1d-61ca-4c06-b3ab-618a42e43f8d"),
                    Description = "Los Angeles Dodgers",
                    Abbreviation = "LAD",
                    LogoPath = "la_dodgers.gif"
                },
                new MlbTeam(){
                    Id = new Guid("555dcea5-341d-4ec3-93ef-5d74682e8e01"),
                    Description = "Miami Marlins",
                    Abbreviation = "MIA",
                    LogoPath = "miami_marlins.gif"
                },
                new MlbTeam(){
                    Id = new Guid("09c2bddd-e7bf-406f-ab1d-5135d22d133d"),
                    Description = "Milwaukee Brewers",
                    Abbreviation = "MIL",
                    LogoPath = "milwaukee_brewers.gif"
                },
                new MlbTeam(){
                    Id = new Guid("3f560a78-a371-4cb9-820d-c34aff25db24"),
                    Description = "Minnesota Twins",
                    Abbreviation = "MIN",
                    LogoPath = "minn_twins.gif"
                },
                new MlbTeam() {
                    Id = new Guid("ff51458d-d1ea-4f3c-84ed-20e05f1e02e2"),
                    Description = "New York Mets",
                    Abbreviation = "NYM",
                    LogoPath = "ny_mets.gif"
                },
                new MlbTeam(){
                    Id = new Guid("c4aaf363-6756-4faf-a6ea-3acf578fa3d4"),
                    Description = "New York Yankees",
                    Abbreviation = "NYY",
                    LogoPath = "ny_yankees.gif"
                },
                new MlbTeam(){
                    Id = new Guid("9513f8ce-7199-4d0d-a15c-6eaafb92040d"),
                    Description = "Oakland Athletics",
                    Abbreviation = "OAK",
                    LogoPath = "oakland_athletics.gif"
                },
                new MlbTeam(){
                    Id = new Guid("b34669f4-72b0-46e2-ad34-a7d911e99eec"),
                    Description = "Philadelphia Phillies",
                    Abbreviation = "PHI",
                    LogoPath = "philadelphia_phillies.gif"
                },
                new MlbTeam(){
                    Id = new Guid("3f0cc811-927c-419d-aa50-e58ab6dc0ab0"),
                    Description = "San Diego Padres",
                    Abbreviation = "SD",
                    LogoPath = "sd_padres.gif"
                },
                new MlbTeam(){
                    Id = new Guid("da177ce8-eda0-4ba1-ab8a-823c8affafb3"),
                    Description = "Seattle Mariners",
                    Abbreviation = "SEA",
                    LogoPath = "seattle_mariners.gif"
                },
                new MlbTeam(){
                    Id = new Guid("9609e6df-2759-4d45-81dd-b0919c100c3a"),
                    Description = "San Francisco Giants",
                    Abbreviation = "SF",
                    LogoPath = "sf_giants.gif"
                },
                 new MlbTeam(){
                    Id = new Guid("cf360a44-2a91-423e-bd8a-ca163626de9b"),
                    Description = "St Louis Cardinals",
                    Abbreviation = "STL",
                    LogoPath = "stlouis_cardinals.gif"
                },
                new MlbTeam(){
                    Id = new Guid("3ac900ab-011b-4677-9380-0b4d453ad24d"),
                    Description = "Tampa Bay Rays",
                    Abbreviation = "TB",
                    LogoPath = "tb_rays.gif"
                },
                new MlbTeam(){
                    Id = new Guid("d1950c34-b856-483f-b6cd-4712db85dd7f"),
                    Description = "Texas Rangers",
                    Abbreviation = "TEX",
                    LogoPath = "texas_rangers.gif"
                },
                new MlbTeam(){
                    Id = new Guid("72304b8a-1473-4586-931c-36c837bb7515"),
                    Description = "Toronto Blue Jays",
                    Abbreviation = "TOR",
                    LogoPath = "toronto_bluejays.gif"
                },
                new MlbTeam(){
                    Id = new Guid("7e567e06-6394-4c6b-be5a-3cc490b8ff5a"),
                    Description = "Washington Nationals",
                    Abbreviation = "WSH",
                    LogoPath = "washington_nationals.gif"
                }

            };

            context.MlbTeams.AddRange(mlbTeams);
            context.SaveChanges();

            var positions = new List<Position>()
            {
                new Position()
                {
                    Id = new Guid("b4c79dba-7cef-428c-8c3b-902b14422119"),
                    Description = "Catcher",
                    Abbreviation = "C"
                },
                new Position(){
                    Id = new Guid("8e600cfe-a4db-4e2c-baac-a0c3415a5eef"),
                    Description = "First Base",
                    Abbreviation = "1B"
                },
                new Position()
                {
                    Id = new Guid("3a012591-c93e-4ae2-9335-b5761efe988f"),
                    Description = "Second Base",
                    Abbreviation = "2B"
                },
                new Position(){
                    Id = new Guid("59ca324f-36a7-4eef-8e69-39426ac0557f"),
                    Description = "Short Stop",
                    Abbreviation="SS"
                },
                new Position(){
                    Id = new Guid("f8accbb3-9e59-4790-b433-3ef38a2f6a38"),
                    Description = "Third Base",
                    Abbreviation = "3B"
                },
                new Position(){
                    Id = new Guid("d70591c4-71f8-4a5a-9cd0-0b33b8d62d04"),
                    Description = "Outfieler",
                    Abbreviation = "OF"
                },
                new Position(){
                    Id = new Guid("5b1e3df7-bbfc-4d21-a346-7731fb4a8aa8"),
                    Description = "Starting Pitcher",
                    Abbreviation = "SP"
                }
            };

            context.Positions.AddRange(positions);
            context.SaveChanges();

            var statCategories = new List<StatCategory>(){
                 new StatCategory(){
                     Id = new Guid("1db8e859-103a-4606-82c4-f2a3396cbd57"),
                     Description = "Batting Average",
                     Abbreviation = "BA"                   
                 },
                 new StatCategory(){
                     Id = new Guid("4426c890-b095-4c45-bff3-f424d7289ee0"),
                     Description = "Home Runs",
                     Abbreviation = "HR"                   
                 },
                 new StatCategory(){
                     Id = new Guid("bff1f6ec-14b0-4605-ad4a-ecf124a6a9a5"),
                     Description = "Runs",
                     Abbreviation = "R"
                 },
                 new StatCategory(){
                     Id = new Guid("180359b2-8af7-4b48-a05c-ced702883c23"),
                     Description = "Runs Batted In",
                     Abbreviation = "RBI"
                 },
                  new StatCategory(){
                     Id = new Guid("967b3e17-4059-4fe1-8dcd-cd83efcd5584"),
                     Description = "Stolen Bases",
                     Abbreviation = "SB"
                 },
                  new StatCategory(){
                     Id = new Guid("9f82e159-7984-4b17-8be3-5a90519ee1f2"),
                     Description = "Innings Pitched",
                     Abbreviation = "IP"
                 }, 
                  new StatCategory(){
                     Id = new Guid("7a8bafb4-c36b-4cf2-a6a1-811f250c5a32"),
                     Description = "Wins",
                     Abbreviation = "W"
                 },
                  new StatCategory(){
                     Id = new Guid("b80ac00d-ee7e-4b64-99ab-a7dc3abc71a0"),
                     Description = "Strikeouts",
                     Abbreviation = "K"
                 },
                 new StatCategory(){
                     Id = new Guid("4ee78456-6b15-4532-890d-10f436a56af9"),
                     Description = "Earned Run Average",
                     Abbreviation = "ERA"
                 },
                 new StatCategory(){
                     Id = new Guid("1292425d-f25d-4c70-83e9-e88d54672778"),
                     Description = "Walks Hits Per Innings Pitched",
                     Abbreviation = "WHIP"
                 }
            };

            context.StatCategories.AddRange(statCategories);
            context.SaveChanges();

            var players = new List<Player>()
            {
                new Player()
                {
                    Id = new Guid("7d9bafc1-2274-407a-a7cd-60e2f6ba496e"),
                    FirstName = "J.T.",
                    LastName = "Realmuto",
                    ImagePath = "JT_Realmuto.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "PHI").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "C").FirstOrDefault(),
                    StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".277"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "22"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "74"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "74"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "3"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }

                },
                new Player()
                {
                    Id = new Guid("693b55c4-60ea-4b06-b512-e6dd4a2a0bbb"),
                    FirstName = "Salvador",
                    LastName = "Perez",
                    ImagePath = "Salvador_Perez.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "KC").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "C").FirstOrDefault(),
                    StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".235"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "27"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "52"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "80"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "1"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("448e7ae2-cf85-4058-aa7a-3fe5cbd9ee75"),
                    FirstName = "Wilson",
                    LastName = "Contreras",
                    ImagePath = "Wilson_Contreras.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "CHC").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "C").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".249"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "54"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "50"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "54"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "4"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("f2d2aefe-ff6e-4d40-9656-c5dd01a735ab"),
                    FirstName = "Buster",
                    LastName = "Posey",
                    ImagePath = "Buster_Posey.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "SF").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "C").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".284"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "5"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "47"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "41"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "3"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("ecaf9cd8-0fb7-4804-8029-8b717d0bb493"),
                    FirstName = "Yadier",
                    LastName = "Molina",
                    ImagePath = "Yadier_Molina.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "STL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "C").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".261"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "20"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "55"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "74"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "4"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("d5553712-08d3-416d-8477-9d04afccfb08"),
                    FirstName = "Freddie",
                    LastName = "Freeman",
                    ImagePath = "Freddie_Freeman.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "ATL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "1B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".309"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "23"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "94"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "98"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "10"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("20675dfb-89c1-4d0e-a3c0-6eccdffb23e0"),
                    FirstName = "Paul",
                    LastName = "Goldschmidt",
                    ImagePath = "Paul_Goldschmidt.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "STL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "1B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".290"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "33"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "95"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "83"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "7"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("25081e0d-350e-4c60-a09f-df9687199151"),
                    FirstName = "Anthony",
                    LastName = "Rizzo",
                    ImagePath = "Anthony_Rizzo.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "CHC").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "1B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".283"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "25"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "74"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "101"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "6"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("f200b117-41fc-416f-acd3-c5cdc7cf3cb0"),
                    FirstName = "Joey",
                    LastName = "Votto",
                    ImagePath = "Joey_Votto.jpg",
                   MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "CIN").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "1B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".284"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "12"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "67"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "67"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("5e109be4-45d6-4b09-9d64-ab2cef47e0de"),
                    FirstName = "Albert",
                    LastName = "Pujols",
                    ImagePath = "Albert_Pujols.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "ANA").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "1B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".245"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "19"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "50"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "64"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "1"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("71e64d75-1097-41a3-a887-0e2b12c5c6e6"),
                    FirstName = "Jose",
                    LastName = "Altuve",
                    ImagePath = "Jose_Altuve.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "HOU").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "2B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".316"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "13"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "84"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "61"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "17"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("a3640c6c-0e1d-4e47-814e-62f89c2c0d8a"),
                    FirstName = "Ozzie",
                    LastName = "Albies",
                    ImagePath = "Ozzie_Albies.jpg",
                     MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "ATL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "2B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".261"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "24"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "105"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "72"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "14"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("2f98ae62-34c0-4dcc-b1c3-4cf41bc64ebf"),
                    FirstName = "Whit",
                    LastName = "Merrifield",
                    ImagePath = "Whit_Merrifield.jpg",
                     MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "KC").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "2B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".304"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "12"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "88"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "60"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "45"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("ae72100e-7f50-4c5d-bee4-2d6db4ad0ad5"),
                    FirstName = "DJ",
                    LastName = "LeMahieu",
                    ImagePath = "DJ_LeMahieu.jpg",
                     MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "NYY").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "2B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".276"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "15"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "90"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "62"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "6"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }

                },
                new Player()
                {
                    Id = new Guid("7ff5d447-3f5f-44c7-b2ee-07fd6454b6c7"),
                    FirstName = "Starlin",
                    LastName = "Castro",
                    ImagePath = "Starlin_Castro.jpg",
                     MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "MIA").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "2B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".278"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "12"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "76"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "54"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "6"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("fb1787a0-f08b-4a4a-bf51-918cacc4c8c8"),
                    FirstName = "Trea",
                    LastName = "Turner",
                    ImagePath = "Trea_Turner.jpg",
                     MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "WSH").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SS").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".271"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "19"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "103"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "73"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "43"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("5742c08a-e076-4e9e-93be-1361b60d02f0"),
                    FirstName = "Francisco",
                    LastName = "Lindor",
                    ImagePath = "Francisco_Lindor.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "CLE").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SS").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".277"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "38"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "129"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "92"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "25"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("6079e089-3405-4454-b666-7e3d6f0abfd6"),
                    FirstName = "Alex",
                    LastName = "Bregman",
                    ImagePath = "Alex_Bregman.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "HOU").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SS").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".286"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "31"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "105"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "103"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "10"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("59d90331-693c-468b-961e-75e5b1cd2a94"),
                    FirstName = "Didi",
                    LastName = "Gregorius",
                    ImagePath = "Didi_Gregorius.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "NYY").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SS").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".268"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "27"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "89"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "86"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "10"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("4af0341c-a285-4f79-931d-3bb653a177e2"),
                    FirstName = "Xander",
                    LastName = "Bogaerts",
                    ImagePath = "Xander_Bogaerts.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "BOS").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SS").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".288"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "23"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "72"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "103"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "8"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("7982dda0-6d06-48b6-8d98-475d01693ed6"),
                    FirstName = "Nolan",
                    LastName = "Arenado",
                    ImagePath = "Nolan_Arenado.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "COL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "3B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".297"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "38"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "104"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "110"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("1457f9b7-ee19-412f-bc75-70a0592edd93"),
                    FirstName = "Jose",
                    LastName = "Ramirez",
                    ImagePath = "Jose_Ramirez.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "CLE").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "3B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".270"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "38"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "110"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "105"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "34"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("f72cf4ea-cde1-4d3f-9c2d-c37750002e2f"),
                    FirstName = "Miguel",
                    LastName = "Andujar",
                    ImagePath = "Miguel_Andujar.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "NYY").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "3B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".297"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "27"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "92"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "83"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("9f5efc60-0cf4-4436-82d2-f262842521f4"),
                    FirstName = "Evan",
                    LastName = "Longoria",
                    ImagePath = "Evan_Longoria.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "SF").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "3B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".244"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "16"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "51"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "54"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "3"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("85ad97a5-8de9-4203-9c5f-3b4e02d9bf4d"),
                    FirstName = "Anthony",
                    LastName = "Rendon",
                    ImagePath = "Anthony_Rendon.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "WSH").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "3B").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".308"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "24"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "88"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "92"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("0afd77bb-9b31-4434-bef4-bb3c23592b6d"),
                    FirstName = "Nicholas",
                    LastName = "Castellanos",
                    ImagePath = "Nicholas_Castellanos.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "DET").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".298"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "23"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "88"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "89"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("4adcf3cd-366e-47ee-ab6b-eb3368d78bf8"),
                    FirstName = "Giancarlo",
                    LastName = "Stanton",
                    ImagePath = "Giancarlo_Stanton.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "NYY").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".266"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "38"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "102"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "100"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "5"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("e961b169-ab87-4e46-ac75-476f92369607"),
                    FirstName = "Ender",
                    LastName = "Inciarte",
                    ImagePath = "Ender_Inciarte.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "ATL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".265"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "10"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "83"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "61"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "28"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("121982f2-526c-4d0d-afb0-b5827cc654af"),
                    FirstName = "Mitch",
                    LastName = "Haniger",
                    ImagePath = "Mitch_Haniger.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "SEA").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".285"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "26"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "90"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "93"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "8"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("211b7298-6b25-406c-96b1-f37b54368481"),
                    FirstName = "Marcell",
                    LastName = "Ozuna",
                    ImagePath = "Marcell_Ozuna.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "STL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".280"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "23"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "69"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "88"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "3"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("1b8f8b37-42d7-4fc9-b821-20045e6c84c2"),
                    FirstName = "Adam",
                    LastName = "Jones",
                    ImagePath = "Adam_Jones.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "BAL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".281"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "15"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "54"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "63"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "7"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("69073dc0-f976-4c66-97c1-e198f395c1ac"),
                    FirstName = "Andrew",
                    LastName = "Benintendi",
                    ImagePath = "Andrew_Benintendi.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "BOS").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".290"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "16"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "103"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "87"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "21"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("b72b6336-4e8a-4593-84d8-c739cfaadc00"),
                    FirstName = "Christian",
                    LastName = "Yelich",
                    ImagePath = "Christian_Yelich.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "MIL").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".326"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "36"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "118"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "110"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "22"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("70aa8911-ed75-4dbf-9948-c0ed6ee545db"),
                    FirstName = "Michael",
                    LastName = "Brantley",
                    ImagePath = "Michael_Brantley.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "CLE").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".309"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "17"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "89"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "76"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "12"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("8322b09e-9715-409b-93d3-e4eb3ea5adc1"),
                    FirstName = "J.D.",
                    LastName = "Martinez",
                    ImagePath = "JD_Martinez.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "BOS").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".330"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "43"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "111"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "130"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "6"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("efa4a840-8d5e-40f0-baf6-6c52b9bfe762"),
                    FirstName = "David",
                    LastName = "Peralta",
                    ImagePath = "David_Peralta.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "ARI").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".293"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "30"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "75"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "87"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "4"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("3510d06e-f826-449f-8229-44f5998da8de"),
                    FirstName = "Mookie",
                    LastName = "Betts",
                    ImagePath = "Mookie_Betts.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "BOS").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".346"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "32"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "129"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "80"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "30"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("d3afecc7-e1c9-4c8f-b8d5-088015319453"),
                    FirstName = "Max",
                    LastName = "Kepler",
                    ImagePath = "Max_Kepler.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "TEX").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".224"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "20"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "80"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "58"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "4"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("a9b09d66-80f7-461f-b1f0-46accd6f489f"),
                    FirstName = "Aaron",
                    LastName = "Hicks",
                    ImagePath = "Aaron_Hicks.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "NYY").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".248"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "27"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "90"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "79"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "11"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("c4f23289-71f7-41e8-b79f-d38f2c89a895"),
                    FirstName = "Andrew",
                    LastName = "McCutchen",
                    ImagePath = "Andrew_McCutchen.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "PHI").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "OF").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ".255"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = "15"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = "65"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = "55"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = "13"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = ""
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("e6ffbf0d-ebee-424d-ad7e-8f858e403ed6"),
                    FirstName = "Max",
                    LastName = "Scherzer",
                    ImagePath = "Max_Scherzer.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "WSH").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SP").FirstOrDefault(),
                     StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = "220.2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = "18"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = "300"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = "2.53"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = "0.91"
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("e1ef1d82-2c0d-4721-b4f0-aa7f958c3b67"),
                    FirstName = "Jacob",
                    LastName = "deGrom",
                    ImagePath = "Jacob_deGrom.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "NYM").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SP").FirstOrDefault(),
                    StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = "217.0"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = "10"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = "269"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = "1.70"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = "0.91"
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("1762a454-6514-4dc3-82f8-85c2f4abe51c"),
                    FirstName = "Blake",
                    LastName = "Snell",
                    ImagePath = "Blake_Snell.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "TB").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SP").FirstOrDefault(),
                    StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = "180.2"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = "21"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = "221"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = "1.89"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = "0.97"
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("a713c338-094a-4cc6-8a3f-cbefcc05fea8"),
                    FirstName = "Justin",
                    LastName = "Verlander",
                    ImagePath = "Justin_Verlander.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "HOU").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SP").FirstOrDefault(),
                    StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = "214.0"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = "16"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = "290"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = "2.52"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = "0.90"
                        }
                    }
                },
                new Player()
                {
                    Id = new Guid("283b66a5-f6e4-47e6-b41c-68d49406156f"),
                    FirstName = "Aaron",
                    LastName = "Nola",
                    ImagePath = "Aaron_Nola.jpg",
                    MlbTeam = context.MlbTeams.Where(t => t.Abbreviation == "PHI").FirstOrDefault(),
                    Position = context.Positions.Where(p => p.Abbreviation == "SP").FirstOrDefault(),
                    StatCategories = new List<PlayerStatCategory>(){
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "BA").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "HR").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "R").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "RBI").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "SB").FirstOrDefault(),
                            Value = ""
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "IP").FirstOrDefault(),
                            Value = "212.1"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "W").FirstOrDefault(),
                            Value = "17"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "K").FirstOrDefault(),
                            Value = "224"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "ERA").FirstOrDefault(),
                            Value = "2.37"
                        },
                        new PlayerStatCategory(){
                            StatCategory = context.StatCategories.Where(sc => sc.Abbreviation == "WHIP").FirstOrDefault(),
                            Value = "0.97"
                        }
                    }
                }
            };

            context.Players.AddRange(players);
            context.SaveChanges();


             var leagues = new List<League>()
            {
                new League(){
                Id = new Guid("d803d697-7f22-412a-b24b-f9ee91eca40f"),
                    Name = "League #1",
                    MinTeams = 2,
                    MaxTeams = 10
                }
            };

            context.Leagues.AddRange(leagues);
            context.SaveChanges();

            var teams = new List<Team>()
            {
                new Team(){
                    Id = new Guid("903753bf-0b3a-4fc7-993f-1a56b096ab7b"),
                    Name = "Team #1",
                    Owner = context.Users.Where(t => t.Username == "username").FirstOrDefault(),
                    League = context.Leagues.Where(l => l.Name == "League #1").FirstOrDefault(),
                },
                 new Team(){
                    Id = new Guid("c5c8f500-8170-4628-b92a-b6db10c51e5f"),
                    Name = "Team #2",
                    Owner = context.Users.Where(t => t.Username == "username").FirstOrDefault(),
                    League = context.Leagues.Where(l => l.Name == "League #1").FirstOrDefault(),
                },
                 new Team(){
                    Id = new Guid("e22e4c52-85f6-4841-9950-ec855571ff74"),
                    Name = "Team #3",
                    Owner = context.Users.Where(t => t.Username == "username").FirstOrDefault(),
                    League = context.Leagues.Where(l => l.Name == "League #1").FirstOrDefault(),
                }
            };

            context.Teams.AddRange(teams);
            context.SaveChanges();

           



        }
    }
}