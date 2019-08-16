using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/leagues/{leagueId}/drafts/{draftId}/draftSelections")]
    [ApiController]
    public class DraftSelectionController : ControllerBase
    {
        private MLBDraftContext _context;
        private IMlbDraftRepository _mlbDraftRepository;
        private IDraftSelectionRepository _draftSelectionRepository;
        private IDraftTeamRosterRepository _draftTeamRosterRepository;
        private IDraftRepository _draftRepository;
        private ILeagueRepository _leagueRepository;
        private IPlayerRepository _playerRepository;
        private IMapper _mapper;
        private ILogger<LeaguesController> _logger;
    
        public DraftSelectionController(
        MLBDraftContext context, 
        IDraftSelectionRepository draftSelectionRepository,
        IDraftTeamRosterRepository draftTeamRosterRepository,
        IDraftRepository draftRepository,
        ILeagueRepository leagueRepository,
        IPlayerRepository playerRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<LeaguesController> logger)
        {
            _context = context;
            _draftSelectionRepository = draftSelectionRepository;
            _draftTeamRosterRepository = draftTeamRosterRepository;
            _draftRepository = draftRepository;
            _leagueRepository = leagueRepository;
            _playerRepository = playerRepository;
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
        public IActionResult UpdateDraftSelection(Guid leagueId, Guid draftId, [FromBody] DraftSelectionUpdateModel draftSelectionUpdateModel){
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }
   
            if(draftSelectionUpdateModel == null)
            {
                return BadRequest();
            }

             if(!_playerRepository.PlayerExists(draftSelectionUpdateModel.PlayerId))
            {
                _logger.LogWarning($"Player {draftSelectionUpdateModel.PlayerId} not found.");
                return NotFound();
            }


             if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var transaction = _context.Database.BeginTransaction()){
                try{
                    //Update draft selection
                    draftSelectionUpdateModel.DraftId = draftId;
                    var draftSelectionEntity = _mapper.Map<DraftSelection>(draftSelectionUpdateModel);
                    _draftSelectionRepository.UpdateDraftSelectionToDraft(draftId, draftSelectionEntity);

                    if(!_mlbDraftRepository.Save()){
                        throw new Exception($"Updating draft {draftId} failed on save.");
                    }

                    var player = _playerRepository.GetPlayer(draftSelectionUpdateModel.PlayerId);
                    var roster = _draftTeamRosterRepository.GetDraftTeamRoster(draftId, draftSelectionUpdateModel.TeamId);      
                    var isFilled = _draftTeamRosterRepository.IsDraftTeamRosterPositionFilled(roster.Id, player.Position.Id);     

                    if(isFilled)
                    {
                        transaction.Rollback();
                        throw new Exception("Position is Filled");
                    }
                    else{
                    //Update team roster
                        AddPlayerToRosterPositon(player, roster);

                        _draftTeamRosterRepository.UpdateDraftTeamRoster(roster);
                        if(!_mlbDraftRepository.Save()){
                            throw new Exception($"Updating draft {draftId} failed on save.");
                        }
                    }

                    transaction.Commit();

                    return NoContent();
                }
                catch(Exception){
                    transaction.Rollback();
                    throw;
                }
            }

            
        }

       private void AddPlayerToRosterPositon(Player player, DraftTeamRoster roster)
       {
           var position = player.Position.Abbreviation;
           switch (position)
            {
                case "SP":
                    roster.StartingPitcherId = player.Id;
                    break;
                case "C":
                    roster.CatcherId = player.Id;
                    break;
                case "1B":
                    roster.FirstBaseId = player.Id;
                    break;
                case "2B":
                    roster.SecondBaseId = player.Id;
                    break;
                case "SS":
                    roster.ShortStopId = player.Id;
                    break;
                case "3B":
                    roster.ThirdBaseId = player.Id;
                    break;
                case "OF":
                    if(!roster.Outfield1Id.HasValue)
                        roster.Outfield1Id = player.Id;
                    else if(!roster.Outfield2Id.HasValue)
                        roster.Outfield2Id = player.Id;
                    else if (!roster.Outfield3Id.HasValue)
                        roster.Outfield3Id = player.Id;
                    break;
                default:
                    throw new Exception("Position could not be filled.");
         
            }
       }

    }
}
