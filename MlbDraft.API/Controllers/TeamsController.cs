using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;
using MLBDraft.API.Entities;
using Microsoft.Extensions.Logging;


namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Route("api/teams")]
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
        public IActionResult Get()
        {
                var teams = _teamRepository.GetTeams();
                
                if(teams == null){
                    _logger.LogWarning("No teams were found.");
                    return NotFound();
                }
                
                var teamModels = _mapper.Map<IEnumerable<TeamModel>>(teams);
                _logger.LogInformation($"{teamModels.Count()} teams were found.");
                return Ok(teamModels);

        }

        [HttpGet("{id}", Name="GetTeam")]
        public IActionResult GetTeam(Guid id)
        {
                if(!_teamRepository.TeamExists(id))
                {
                    _logger.LogError($"Team {id} does not exist.");
                    return NotFound();
                }
                var team = _teamRepository.GetTeam(id);

                var teamModel = _mapper.Map<TeamModel>(team);
                return Ok(teamModel);

        }

        [HttpPost("{leagueId}")]
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

            var owner = _userRepository.GetUser(teamCreateModel.Owner);
            if(owner == null){
                return BadRequest();
            }

            teamEntity.Owner = owner;
            teamEntity.OwnerId = owner.Username;
            
            _teamRepository.AddTeam(teamEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a team failed on save.");
            }

            var teamToReturn = _mapper.Map<TeamModel>(teamEntity);

            return CreatedAtRoute("GetTeam",
                    new Team{Id = teamToReturn.Id},
                    teamToReturn);
        }

         [HttpPut("{leagueId}/{id}")]
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
                var teamToAdd = _mapper.Map<Team>(team);
                teamToAdd.Id = id;

                _teamRepository.AddTeamForLeague(leagueId, teamToAdd);

                if (!_mlbDraftRepository.Save())
                {
                    throw new Exception($"Upserting team {id} for league {leagueId} failed on save.");
                }

                var teamToReturn = _mapper.Map<TeamModel>(teamToAdd);

                return CreatedAtRoute("GetTeamForLeague",
                    new { leagueId = leagueId, id = teamToReturn.Id},
                    teamToReturn);
            }

            var teamUpdate = _mapper.Map<Team>(team);
            _teamRepository.UpdateTeamForLeague(teamForLeagueFromRepo, teamUpdate);

            if (!_mlbDraftRepository.Save())
            {
                throw new Exception($"Updating team {id} for league {leagueId} failed on save.");
            }

           return NoContent();
        }


        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if(!_teamRepository.TeamExists(id))
            {
                _logger.LogError("Team does not exist.");
                return NotFound();
            }

            var team = _teamRepository.GetTeam(id);

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
