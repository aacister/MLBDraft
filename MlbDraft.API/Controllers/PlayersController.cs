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
using MLBDraft.API.Helpers;
using Microsoft.Extensions.Logging;


namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Authorize("MlbDraftUsers")]
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private IPlayerRepository _playerRepository;
        private IMlbTeamRepository _mlbTeamRepository;
        private IPositionRepository _positionRepository;
        private IMapper _mapper;
        private ILogger<PlayersController> _logger;

        private IUrlHelper _urlHelper;
    
        public PlayersController(IPlayerRepository playerRepository,
        IMlbTeamRepository mlbTeamRepository,
        IMlbDraftRepository mlbDraftRepository,
        IPositionRepository positionRepository,
        IMapper mapper,
        ILogger<PlayersController> logger,
        IUrlHelper urlHelper)
        {
            _playerRepository = playerRepository;
            _mlbTeamRepository = mlbTeamRepository;
            _positionRepository = positionRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
            _urlHelper = urlHelper;
        }


        [HttpGet(Name = "GetPlayers")]
        public IActionResult Get(PlayerParameters playerParams)
        {
                var players = _playerRepository.GetPlayers(playerParams);
                
                if(players == null){
                    _logger.LogWarning("No players were found.");
                    return NotFound();
                }
 
                var previousPageLink = players.HasPrevious ?
                CreatePlayersUri(playerParams,
                PageUriType.PreviousPage) : null;

                var nextPageLink = players.HasNext ? 
                    CreatePlayersUri(playerParams,
                    PageUriType.NextPage) : null;

                var paginationMetadata = new
                {
                    totalCount = players.TotalCount,
                    pageSize = players.PageSize,
                    currentPage = players.CurrentPage,
                    totalPages = players.TotalPages,
                    previousPageLink = previousPageLink,
                    nextPageLink = nextPageLink
                };

                Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
             
                var playerModels = _mapper.Map<IEnumerable<PlayerModel>>(players);
                return Ok(playerModels);

        }

        [HttpGet("{id}", Name="GetPlayer")]
        public IActionResult GetPlayer(Guid id)
        {
            if (!_playerRepository.PlayerExists(id))
            {
                _logger.LogWarning($"No player found for {id}.");
                return NotFound();
            }

            var player = _playerRepository.GetPlayer(id);

            var playerModel = _mapper.Map<PlayerModel>(player);
            return Ok(playerModel);

        }

  
        [HttpPost]
        public IActionResult CreatePlayer([FromBody] PlayerCreateModel playerCreateModel){
            if(playerCreateModel == null)
            {
                return BadRequest();
            }

             if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mlbTeam = _mlbTeamRepository.GetMlbTeam(playerCreateModel.MlbTeamAbbreviation);
            if(mlbTeam == null)
            {
                return BadRequest();
            }

            var position = _positionRepository.GetPosition(playerCreateModel.PositionAbbreviation);
            if(position == null){
                return BadRequest();
            }

            var playerEntity = _mapper.Map<Player>(playerCreateModel);
            playerEntity.Position = position;
            playerEntity.MlbTeam = mlbTeam;
            _playerRepository.AddPlayer(playerEntity);

            if(!_mlbDraftRepository.Save()){
                throw new Exception("Creating a player failed on save.");
            }

            var playerToReturn = _mapper.Map<PlayerModel>(playerEntity);

            return CreatedAtRoute("GetPlayer",
                    new Player{Id = playerToReturn.Id},
                    playerToReturn);
        }

        

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if(!_playerRepository.PlayerExists(id))
            {
                _logger.LogError("Player does not exist.");
                return NotFound();
            }

            var player = _playerRepository.GetPlayer(id);

            _playerRepository.DeletePlayer(player);
            if(!_mlbDraftRepository.Save())
            {
                _logger.LogError($"Could not delete player {id}");
                throw new Exception($"Deleting player {id} failed on save.");
            }
             
            return NoContent();

        }

         private string CreatePlayersUri(
            PlayerParameters playerParams,
            PageUriType type)
        {
            switch (type)
            {
                case PageUriType.PreviousPage:
                    return _urlHelper.Link("GetPlayers",
                      new 
                      {
                          position = playerParams.Position,
                          team = playerParams.Team,
                          pageNumber = playerParams.PageNumber - 1,
                          pageSize = playerParams.PageSize
                      });
                case PageUriType.NextPage:
                    return _urlHelper.Link("GetPlayers",
                      new 
                      {
                          position = playerParams.Position,
                          team = playerParams.Team,
                          pageNumber = playerParams.PageNumber + 1,
                          pageSize = playerParams.PageSize
                      });

                default:
                    return _urlHelper.Link("GetPlayers",
                    new 
                    {
                        position = playerParams.Position,
                        team = playerParams.Team,
                        pageNumber = playerParams.PageNumber,
                        pageSize = playerParams.PageSize
                    });
            }
        }

    }
}
