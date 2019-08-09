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
    [Route("api/leagues/{leagueId}/drafts/{draftid}/draftSelection")]
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
        public IActionResult Get(Guid leagueId, Guid draftId)
        {
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            if(!_draftRepository.DraftExistsForLeague(leagueId, draftId)
            {
                _logger.LogWarning($"Draft {draftId} not found for league {leagueId}.");
                return NotFound();
            }
                var drafts = _draftSeletionRepository.GetDraftSelections((leagueId);)
                
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

            if(draft.League.Id != leagueId)
            {
                return NotFound();
            }

            var draftModel = _mapper.Map<DraftModel>(draft);
            return Ok(draftModel);

        }

        [HttpPost]
        public IActionResult CreateDraft(Guid leagueId, [FromBody] DraftCreateModel draftCreateModel){
            if (!_leagueRepository.LeagueExists(leagueId))
            {
                _logger.LogWarning($"No league found for {leagueId}.");
                return NotFound();
            }

            if(draftCreateModel == null)
            {
                return BadRequest();
            }

             if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var draftEntity = _mapper.Map<Draft>(draftCreateModel);
            _draftRepository.AddDraft(draftEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a draft failed on save.");
            }

            var draftToReturn = _mapper.Map<DraftModel>(draftEntity);

            return CreatedAtRoute("GetDraft",
                    new Draft{Id = draftToReturn.Id},
                    draftToReturn);
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

    }
}
