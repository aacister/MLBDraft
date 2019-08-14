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
    [Route("api/leagues/{leagueId}/drafts/{draftId}/rosters")]
    [ApiController]
    public class DraftTeamRostersController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private IDraftSelectionRepository _draftSelectionRepository;
        private IDraftTeamRosterRepository _draftTeamRosterRepository;
        private IDraftRepository _draftRepository;
        private ILeagueRepository _leagueRepository;
        private IMapper _mapper;
        private ILogger<DraftTeamRostersController> _logger;
    
        public DraftTeamRostersController(
        IDraftSelectionRepository draftSelectionRepository,
        IDraftTeamRosterRepository draftTeamRosterRepository,
        IDraftRepository draftRepository,
        ILeagueRepository leagueRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<DraftTeamRostersController> logger)
        {
            _draftSelectionRepository = draftSelectionRepository;
            _draftTeamRosterRepository = draftTeamRosterRepository;
            _draftRepository = draftRepository;
            _leagueRepository = leagueRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet(Name = "GetDraftTeamRosters")]
        public IActionResult GetDraftTeamRosters(Guid leagueId, Guid draftId)
        {
                if (!_leagueRepository.LeagueExists(leagueId))
                {
                    _logger.LogWarning($"No league found for {leagueId}.");
                    return NotFound();
                }

                if(!_draftRepository.DraftExistsForLeague(leagueId, draftId))
                {
                    _logger.LogWarning($"Draft {draftId} not found for league {leagueId}.");
                    return NotFound();
                }

                var draftRosters = _draftTeamRosterRepository.GetDraftTeamRosters(leagueId);
                
                if(draftRosters == null){
                    _logger.LogWarning($"No draft rosters were found for league {leagueId}.");
                    return NotFound();
                }
                
                var draftRosterModels = _mapper.Map<IEnumerable<DraftTeamRosterModel>>(draftRosters);
                return Ok(draftRosterModels);

        }



        [HttpGet("teams/{teamId}", Name = "GetTeamDraftTeamRosters")]
        public IActionResult GetTeamDraftTeamRostersForTeam(Guid leagueId, Guid draftId, Guid teamId)
        {
                if (!_leagueRepository.LeagueExists(leagueId))
                {
                    _logger.LogWarning($"No league found for {leagueId}.");
                    return NotFound();
                }

                if(!_draftRepository.DraftExistsForLeague(leagueId, draftId))
                {
                    _logger.LogWarning($"Draft {draftId} not found for league {leagueId}.");
                    return NotFound();
                }

                var draftRoster = _draftTeamRosterRepository.GetDraftTeamRoster(draftId, teamId);
                
                if(draftRoster == null){
                    _logger.LogWarning($"No draft rosters were found for league {leagueId}, draft {draftId}, and team {teamId}.");
                    return NotFound();
                }
                
                var draftRosterModel = _mapper.Map<DraftTeamRosterModel>(draftRoster);
                return Ok(draftRosterModel);

        }
    }
}