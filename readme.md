
# MLBDraft (In progress) #

MLBDraft is used to simulate online fantasy baseball snake drafts. Create your team, join a league, and simulate a draft, selecting from a pool of players.

Includes 2 projects:
 1. MlbDraft.Api
 2. MlbDraft.Client

## MlbDraft.Api ##
 * Developed utilizing Asp.net Core Web Api 2
 * SqLite data storage
 * JWT auth
 * Entity Framework
 * AutoMapper
 * NLog
 * Player paging, filtering, and search
 * Custom Validation
 
 ### API Test Calls 
 Based on initial setup (draft entity records not included in initial setup.)

AUTHCONTROLLER.CS
POST http://localhost:5050/api/auth/register
{
    "userName" : "username",
    "password" : "password"
}

POST http://localhost:5050/api/auth/login
{
    "userName" : "username",
    "password" : "password"
}
-----------------------------------------------------------

PLAYERSCONTROLLER.CS
GET http://localhost:5050/api/players

GET http://localhost:5050/api/players/a9b09d66-80f7-461f-b1f0-46accd6f489f

POST http://localhost:5050/api/players
{
"firstName":"Aaron",
"lastName": "Judge",
"mlbTeamAbbreviation" : "NYY",
"positionAbbreviation":"OF",
"battingAverage":".278",
"homeRuns":"27",
"runs":"77",
"runsBattedIn":"67",
"stolenBases":"6",
"inningsPitched":"",
"wins":"",
"strikeouts":"",
"earnedRunAverage":"",
"whip": ""
}

DELETE http://localhost:5050/api/players/a9b09d66-80f7-461f-b1f0-46accd6f489f

------------------------------------------------------------

LEAGUESCONTROLLER.CS
GET http://localhost:5050/api/leagues

POST http://localhost:5050/api/leagues
{
"name": "League #2",
"maxTeams": 10
}

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f

DELETE http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f

------------------------------------------------------------

TEAMSCONTROLLER.CS
GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/teams

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/teams/903753bf-0b3a-4fc7-993f-1a56b096ab7b

POST http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/teams

{
"name": "Team #5",
"owner": "username"
}


Delete http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/teams/903753bf-0b3a-4fc7-993f-1a56b096ab7b

------------------------------------------------
USERSCONTROLLER.CS (Must use auth controller to receive token)
GET http://localhost:5050/api/users

GET http://localhost:5050/api/users/username

DELETE http://localhost:5050/api/users/username

-----------------------------------------------------
DRAFTCONTROLLER.CS
GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/80834091-58f9-4f47-9295-bf2ec8b7728e returns a draft's selections and team rosters as well

POST http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts  --- creates draft order with empty team draft selections and team rosters


-------------------------------------------------
DRAFTSELECTIONCONTROLLER.CS
GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/c0ca9dd3-6ad1-4f0d-981b-53eb9ddfa4cc/draftSelections

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/c0ca9dd3-6ad1-4f0d-981b-53eb9ddfa4cc/draftSelections/teams/903753bf-0b3a-4fc7-993f-1a56b096ab7b  =>draft selections for team

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/c0ca9dd3-6ad1-4f0d-981b-53eb9ddfa4cc/draftSelections/teams/903753bf-0b3a-4fc7-993f-1a56b096ab7b/rounds/2    =>Get team seletion for round


PUT http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/c0ca9dd3-6ad1-4f0d-981b-53eb9ddfa4cc/draftSelections -- updates draft selection with player (used to draft a player)

{
"draftId": "ba6395e9-d4e3-4c65-848f-cee1b5f32055",
"teamId": "903753bf-0b3a-4fc7-993f-1a56b096ab7b",
"round": 1,
"playerId": "6079e089-3405-4454-b666-7e3d6f0abfd6"

}
--------------------------------------------------------
DRAFTTEAMROSTERCONTROLLER.CS

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/ba6395e9-d4e3-4c65-848f-cee1b5f32055/rosters    -- retrieves rosters for a draft

GET http://localhost:5050/api/leagues/d803d697-7f22-412a-b24b-f9ee91eca40f/drafts/ba6395e9-d4e3-4c65-848f-cee1b5f32055/rosters/teams/903753bf-0b3a-4fc7-993f-1a56b096ab7b   -- retrieve a team's rosters for a draft




