using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;
using MLBDraft.API.Entities;
using Microsoft.Extensions.Logging;


namespace MLBDraft.API.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private ITeamRepository _teamRepository;
        private ILeagueRepository _leagueRepository;
        private IMapper _mapper;
        private ILogger<TeamsController> _logger;
    
        public TeamsController(ITeamRepository teamRepository,
        ILeagueRepository leagueRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<TeamsController> logger)
        {
            _teamRepository = teamRepository;
            _leagueRepository = leagueRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetTeams")]
        public ActionResult<IEnumerable<string>> Get()
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

                var team = _teamRepository.GetTeam(id);
                
                if(team == null){
                    _logger.LogWarning("No team found.");
                    return NotFound();
                }

                var teamModel = _mapper.Map<TeamModel>(team);
                return Ok(teamModel);

        }
/* 
        [HttpGet("({ids})", Name="GetPlayersByIds")]
        public IActionResult GetPlayersByIds(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {           
            if (ids == null)
            {
                return BadRequest();
            }

            var playerEntities = _playerRepository.GetPlayers(ids);

            if (ids.Count() != playerEntities.Count())
            {
                return NotFound();
            }

            var playersToReturn = _mapper.Map<IEnumerable<Player>>(playerEntities);
            return Ok(playersToReturn);
        }
*/
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
            
            _teamRepository.AddTeam(teamEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a team failed on save.");
            }

            var teamToReturn = _mapper.Map<TeamModel>(teamEntity);

            return CreatedAtRoute("GetTeam",
                    new Team{Id = teamToReturn.Id},
                    teamToReturn);
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
