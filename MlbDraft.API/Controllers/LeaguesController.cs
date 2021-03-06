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
    [Route("api/leagues")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private ILeagueRepository _leagueRepository;
        private IMapper _mapper;
        private ILogger<LeaguesController> _logger;
    
        public LeaguesController(ILeagueRepository leagueRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<LeaguesController> logger)
        {
            _leagueRepository = leagueRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetLeagues")]
        public IActionResult Get()
        {
                var leagues = _leagueRepository.GetLeagues();
                
                if(leagues == null){
                    _logger.LogWarning("No leagues were found.");
                    return NotFound();
                }
                
                var leagueModels = _mapper.Map<IEnumerable<LeagueModel>>(leagues);
                _logger.LogInformation($"{leagueModels.Count()} leagues were found.");
                return Ok(leagueModels);

        }

        [HttpGet("{id}", Name="GetLeague")]
        public IActionResult GetLeague(Guid id)
        {
            if (!_leagueRepository.LeagueExists(id))
            {
                _logger.LogWarning($"No league found for {id}.");
                return NotFound();
            }

            var league = _leagueRepository.GetLeague(id);

            var leagueModel = _mapper.Map<LeagueModel>(league);
            return Ok(leagueModel);

        }

        [HttpPost]
        public IActionResult CreateLeague([FromBody] LeagueCreateModel leagueCreateModel){
            if(leagueCreateModel == null)
            {
                return BadRequest();
            }

             if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var leagueEntity = _mapper.Map<League>(leagueCreateModel);
            _leagueRepository.AddLeague(leagueEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a league failed on save.");
            }

            var leagueToReturn = _mapper.Map<LeagueModel>(leagueEntity);

            return CreatedAtRoute("GetLeague",
                    new League{Id = leagueToReturn.Id},
                    leagueToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if(!_leagueRepository.LeagueExists(id))
            {
                _logger.LogError($"League {id} does not exist.");
                return NotFound();
            }
            var  league = _leagueRepository.GetLeague(id);

            _leagueRepository.DeleteLeague(league);
            if(!_mlbDraftRepository.Save())
            {
                _logger.LogError($"Could not delete league {id}");
                throw new Exception($"Deleting league {id} failed on save.");
            }
             
            return NoContent();

        }

    }
}
