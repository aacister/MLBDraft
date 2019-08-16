using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;
using MLBDraft.API.Entities;
using Microsoft.Extensions.Logging;


namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Authorize("MlbDraftUsers")]
    [Route("api/leagues/{leagueId}/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private ITeamRepository _teamRepository;
        private ILeagueRepository _leagueRepository;

        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<TeamsController> _logger;
    
        public TeamsController(ITeamRepository teamRepository,
        ILeagueRepository leagueRepository,
        IUserRepository userRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<TeamsController> logger)
        {
            _teamRepository = teamRepository;
            _leagueRepository = leagueRepository;
            _userRepository = userRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetTeams")]
        public IActionResult Get(Guid leagueId)
        {
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

                var teams = _teamRepository.GetTeamsForLeague(leagueId);
                
                if(teams == null){
                    _logger.LogWarning($"No teams were found for league {leagueId}.");
                    return NotFound();
                }
                
                var teamModels = _mapper.Map<IEnumerable<TeamModel>>(teams);
                _logger.LogInformation($"{teamModels.Count()} teams were found for {leagueId}.");
                return Ok(teamModels);

        }

        [HttpGet("{id}", Name="GetTeam")]
        public IActionResult GetTeam(Guid leagueId, Guid id)
        {
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

                if(!_teamRepository.TeamExistsForLeague(leagueId, id))
                {
                    _logger.LogError($"Team {id} does not exist in league {leagueId}.");
                    return NotFound();
                }
                var team = _teamRepository.GetTeamForLeague(leagueId,id);

                var teamModel = _mapper.Map<TeamModel>(team);
                return Ok(teamModel);

        }

        [HttpPost()]
        public IActionResult CreateTeamForLeague(Guid leagueId, [FromBody] TeamCreateModel teamCreateModel){
            if(teamCreateModel == null)
            {
                return BadRequest();
            }

            if (!_leagueRepository.LeagueExists(leagueId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var leagueEntity = _leagueRepository.GetLeague(leagueId);


            var teamEntity = _mapper.Map<Team>(teamCreateModel);
            teamEntity.League = leagueEntity;
            teamEntity.LeagueId = leagueEntity.Id;

            var owner = _userRepository.GetUser(teamCreateModel.Owner).Result;
            if(owner == null){
                return BadRequest();
            }
          
            teamEntity.Owner = owner;
            teamEntity.OwnerId = owner.UserName;
            
            _teamRepository.AddTeam(teamEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a team failed on save.");
            }

            var teamToReturn = _mapper.Map<TeamModel>(teamEntity);

            return CreatedAtRoute("GetTeam",
                    new { leagueId = leagueId, id = teamToReturn.Id},
                    teamToReturn);
        }
/* 
         [HttpPut("{id}")]
        public IActionResult UpdateTeamForLeague(Guid leagueId, Guid id,
            [FromBody] TeamUpdateModel team)
        {
            if (team == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }


            if (!_leagueRepository.LeagueExists(leagueId))
            {
                return NotFound();
            }

            var teamForLeagueFromRepo = _teamRepository.GetTeamForLeague(leagueId, id);
            if (teamForLeagueFromRepo == null)
            {

                //add
                var teamToAdd = _mapper.Map<Team>(team);
                teamToAdd.Id = id;

                _teamRepository.AddTeamForLeague(leagueId, teamToAdd);

                if (!_mlbDraftRepository.Save())
                {
                    throw new Exception($"Upserting team {id} for league {leagueId} failed on save.");
                }

                var teamToReturn = _mapper.Map<TeamModel>(teamToAdd);

                return CreatedAtRoute("GetTeam",
                    new { leagueId = leagueId, id = teamToReturn.Id},
                    teamToReturn);
            }

            var teamUpdate = _mapper.Map<Team>(team);
            _teamRepository.UpdateTeamForLeague(leagueId, teamForLeagueFromRepo, teamUpdate);

            if (!_mlbDraftRepository.Save())
            {
                throw new Exception($"Updating team {id} for league {leagueId} failed on save.");
            }

           return NoContent();
        }

*/
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid leagueId, Guid id)
        {

            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            if(!_teamRepository.TeamExistsForLeague(leagueId, id))
            {
                _logger.LogError($"Team does not exist for league {leagueId}.");
                return NotFound();
            }

            var team = _teamRepository.GetTeamForLeague(leagueId, id);

            _teamRepository.DeleteTeam(team);
            if(!_mlbDraftRepository.Save())
            {
                _logger.LogError($"Could not delete team {id}");
                throw new Exception($"Deleting team {id} failed on save.");
            }
             
            return NoContent();

        }


    }
}
