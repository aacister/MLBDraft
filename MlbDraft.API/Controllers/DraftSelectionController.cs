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
    [Route("api/leagues/{leagueId}/drafts/{draftId}/draftSelections")]
    [ApiController]
    public class DraftSelectionController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private IDraftSelectionRepository _draftSelectionRepository;
        private IDraftRepository _draftRepository;
        private ILeagueRepository _leagueRepository;
        private IMapper _mapper;
        private ILogger<LeaguesController> _logger;
    
        public DraftSelectionController(IDraftSelectionRepository draftSelectionRepository,
        IDraftRepository draftRepository,
        ILeagueRepository leagueRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<LeaguesController> logger)
        {
            _draftSelectionRepository = draftSelectionRepository;
            _draftRepository = draftRepository;
            _leagueRepository = leagueRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet(Name = "GetDraftSelections")]
        public IActionResult GetDraftSelections(Guid leagueId, Guid draftId)
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
                var draftSelections = _draftSelectionRepository.GetLeagueDraftSelectionsForLeague(draftId);
                
                if(draftSelections == null){
                    _logger.LogWarning($"No draft selections were found for league {leagueId} and draft {draftId}.");
                    return NotFound();
                }
                
                var draftSelectionModels = _mapper.Map<IEnumerable<DraftSelectionModel>>(draftSelections);
                return Ok(draftSelectionModels);

        }



        [HttpGet("teams/{teamId}", Name = "GetTeamDraftSelections")]
        public IActionResult GetTeamDraftSelections(Guid leagueId, Guid draftId, Guid teamId)
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
                var draftSelections = _draftSelectionRepository.GetLeagueDraftSelectionsForTeam(draftId, teamId);
                
                if(draftSelections == null){
                    _logger.LogWarning($"No draft selections were found for league {leagueId}, draft {draftId}, and team {teamId}.");
                    return NotFound();
                }
                
                var draftSelectionModels = _mapper.Map<IEnumerable<DraftSelectionModel>>(draftSelections);
                return Ok(draftSelectionModels);

        }

        [HttpGet("teams/{teamId}/rounds/{round}", Name="GetDraftSelection")]
        public IActionResult GetDraftSelection(Guid leagueId, Guid draftId, Guid teamId, int round)
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

            if (!_draftSelectionRepository.LeagueDraftSelectionExists(draftId, teamId, round))
            {
                _logger.LogWarning($"No draft selection found for: draft {draftId}, team {teamId}, round {round}.");
                return NotFound();
            }

            var draftSelection = _draftSelectionRepository.GetLeagueDraftSelection(draftId, teamId, round);

            var draftSelectionModel = _mapper.Map<DraftSelectionModel>(draftSelection);
            return Ok(draftSelectionModel);

        }

        [HttpPut]
        public IActionResult CreateDraftSelection(Guid leagueId, Guid draftId, [FromBody] DraftSelectionCreateModel draftSelectionCreateModel){
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            if(draftSelectionCreateModel == null)
            {
                return BadRequest();
            }

             if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            draftSelectionCreateModel.DraftId = draftId;
            var draftSelectionEntity = _mapper.Map<DraftSelection>(draftSelectionCreateModel);
            _draftSelectionRepository.UpdateDraftSelectionToDraft(draftId, draftSelectionEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception($"Updating draft {draftId} failed on save.");
            }

            return NoContent();
        }

    }
}
