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
    [Route("api/leagues/{leagueId}/drafts")]
    [ApiController]
    public class DraftsController : ControllerBase
    {
        private MLBDraftContext _context;
        private IMlbDraftRepository _mlbDraftRepository;
        private IDraftRepository _draftRepository;
        private IDraftSelectionRepository _draftSelectionRepository;
        private IDraftTeamRosterRepository _draftTeamRosterRepository;
        private ILeagueRepository _leagueRepository;
        private IPlayerRepository _playerRepository;
        private IMapper _mapper;
        private ILogger<LeaguesController> _logger;
    
        public DraftsController(MLBDraftContext context, 
        IDraftRepository draftRepository,
        IDraftSelectionRepository draftSelectionRepository,
        IDraftTeamRosterRepository draftTeamRosterRepository,
        ILeagueRepository leagueRepository,
        IPlayerRepository playerRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<LeaguesController> logger)
        {
            _context = context;
            _draftRepository = draftRepository;
            _draftSelectionRepository = draftSelectionRepository;
            _draftTeamRosterRepository = draftTeamRosterRepository;
            _leagueRepository = leagueRepository;
            _playerRepository = playerRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetDrafts")]
        public IActionResult Get(Guid leagueId)
        {
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }
                var drafts = _draftRepository.GetDraftsForLeague(leagueId);
                
                if(drafts == null){
                    _logger.LogWarning($"No drafts were found for league {leagueId}.");
                    return NotFound();
                }
                
                var draftModels = _mapper.Map<IEnumerable<DraftModel>>(drafts);
                _logger.LogInformation($"{draftModels.Count()} drafts were found for league {leagueId}.");
                return Ok(draftModels);

        }

        [HttpGet("{id}", Name="GetDraft")]
        public IActionResult GetDraft(Guid leagueId, Guid id)
        {
             if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            if (!_draftRepository.DraftExists(id))
            {
                _logger.LogWarning($"No draft found for {id}.");
                return NotFound();
            }

            var draft = _draftRepository.GetDraft(id);

            if(draft.LeagueId != leagueId)
            {
                return NotFound();
            }

            var draftModel = _mapper.Map<DraftModel>(draft);
            return Ok(draftModel);

        }

        [HttpPost]
        public IActionResult CreateDraft(Guid leagueId){
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            var league = _leagueRepository.GetLeague(leagueId);
            if(league.Teams.Count() < league.MinTeams)
            {
                _logger.LogError("Does not exceed team minimum.");
                return BadRequest();
            }

            using(var transaction = _context.Database.BeginTransaction()){
                try
                {
                    //Add draft
                    var draftModel = new DraftModel();
                    draftModel.StartDate = DateTime.Now;
                    draftModel.LeagueId = leagueId;
                    var draftEntity = _mapper.Map<Draft>(draftModel);
                    _draftRepository.AddDraft(draftEntity);

                    if(!_mlbDraftRepository.Save()){
                        throw new Exception("Creating a draft failed on save.");
                    }

                    //Create draft Selections and draft team rosters
                    CreateDraftSelectionsAndRosters(draftEntity);
                    var draftToReturn = _mapper.Map<DraftModel>(draftEntity);
                    transaction.Commit();
    
                    return CreatedAtRoute("GetDraft",
                    new {leagueId = leagueId,
                    id = draftToReturn.Id},
                    draftToReturn);
                }
                catch(Exception){
                    transaction.Rollback();
                    throw;
                }
            }
           
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid leagueId, Guid id)
        {
             if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            if(!_draftRepository.DraftExists(id))
            {
                _logger.LogError($"Draft {id} does not exist.");
                return NotFound();
            }
            var  draft = _draftRepository.GetDraft(id);

             if(draft.League.Id != leagueId)
            {
                return NotFound();
            }

            _draftRepository.DeleteDraft(draft);
            if(!_mlbDraftRepository.Save())
            {
                _logger.LogError($"Could not delete draft {id}");
                throw new Exception($"Deleting draft {id} failed on save.");
            }
             
            return NoContent();

        }

        private void CreateDraftSelectionsAndRosters(Draft draft)
        {
            var league = _leagueRepository.GetLeague(draft.LeagueId);
            var totalTeams = league.Teams.Count();
            var totalPlayers = _playerRepository.GetPlayerCount();

            var totalRounds = Convert.ToInt32(Math.Floor(totalPlayers/Convert.ToDouble(totalTeams)));
            for(int x=0; x < totalRounds; x++){
                foreach(var team in league.Teams){
                    if(x == 0)
                    {
                        //Create draft rosters on first round loop. Creates roster for each team
                         var draftTeamRosterCreateModel = new DraftTeamRosterCreateModel();
                        draftTeamRosterCreateModel.DraftId = draft.Id;
                        draftTeamRosterCreateModel.TeamId = team.Id;
                        var draftTeamRosterEntity = _mapper.Map<DraftTeamRoster>(draftTeamRosterCreateModel);
                        _draftTeamRosterRepository.AddTeamRosterToDraft(draft.Id, draftTeamRosterEntity);
                        if(!_mlbDraftRepository.Save()){
                            throw new Exception("Creating a draft roster failed on save.");
                        }
                    }

                    var draftSelectionCreateModel = new DraftSelectionCreateModel();
                    draftSelectionCreateModel.DraftId = draft.Id;
                    draftSelectionCreateModel.TeamId = team.Id;
                    draftSelectionCreateModel.Round = x+1;
                    var draftSelectionEntity = _mapper.Map<DraftSelection>(draftSelectionCreateModel);
                    _draftSelectionRepository.AddDraftSelectionToDraft(draft.LeagueId, draft.Id, draftSelectionEntity);
                    if(!_mlbDraftRepository.Save()){
                        throw new Exception("Creating a draft selection failed on save.");
                    }
                }
            }
        }

    }
}
