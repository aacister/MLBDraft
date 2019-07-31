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
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private IPlayerRepository _playerRepository;
        private IMapper _mapper;
        private ILogger<PlayersController> _logger;
    
        public PlayersController(IPlayerRepository playerRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<PlayersController> logger)
        {
            _playerRepository = playerRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetPlayers")]
        public ActionResult<IEnumerable<string>> Get()
        {
                var players = _playerRepository.GetPlayers();
                
                if(players == null){
                    _logger.LogWarning("No players were found.");
                    return NotFound();
                }
                
                var playerModels = _mapper.Map<IEnumerable<PlayerModel>>(players);
                _logger.LogInformation($"{playerModels.Count()} players were found.");
                return Ok(playerModels);

        }

        [HttpGet("{id}", Name="GetPlayer")]
        public IActionResult GetPlayer(Guid id)
        {

                var player = _playerRepository.GetPlayer(id);
                
                if(player == null){
                    _logger.LogWarning("Not player found.");
                    return NotFound();
                }

                var playerModel = _mapper.Map<PlayerModel>(player);
                return Ok(playerModel);

        }

        [HttpPost]
        public IActionResult CreatePlayer([FromBody] PlayerCreateModel playerCreateModel){
            if(playerCreateModel == null)
            {
                return BadRequest();
            }

            var playerEntity = _mapper.Map<Player>(playerCreateModel);
            _playerRepository.AddPlayer(playerEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a player failed on save.");
            }

            var playerToReturn = _mapper.Map<PlayerModel>(playerEntity);
            _logger.LogInformation($"Player was created: {playerToReturn.ToString()} ");

            return CreatedAtRoute("GetPlayer",
                    new Player{Id = playerToReturn.Id},
                    playerToReturn);
        }

    }
}
