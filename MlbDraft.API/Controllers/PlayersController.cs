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
using MLBDraft.API.Helpers;
using Microsoft.Extensions.Logging;


namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IMlbDraftRepository _mlbDraftRepository;
        private IPlayerRepository _playerRepository;
        private IMapper _mapper;
        private ILogger<PlayersController> _logger;

        private IUrlHelper _urlHelper;
    
        public PlayersController(IPlayerRepository playerRepository,
        IMlbDraftRepository mlbDraftRepository,
        IMapper mapper,
        ILogger<PlayersController> logger,
        IUrlHelper urlHelper)
        {
            _playerRepository = playerRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _mapper = mapper;
            _logger = logger;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetPlayers")]
        public IActionResult Get(PlayerParametersModel playerParamsModel)
        {
                var players = _playerRepository.GetPlayers(playerParamsModel.PageNumber, playerParamsModel.PageSize);
                
                if(players == null){
                    _logger.LogWarning("No players were found.");
                    return NotFound();
                }

                var previousPageLink = players.HasPrevious ?
                CreatePlayersUri(playerParamsModel,
                PageUriType.PreviousPage) : null;

                var nextPageLink = players.HasNext ? 
                    CreatePlayersUri(playerParamsModel,
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

            var playerEntity = _mapper.Map<Player>(playerCreateModel);
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
            PlayerParametersModel playerParamsModel,
            PageUriType type)
        {
            switch (type)
            {
                case PageUriType.PreviousPage:
                    return _urlHelper.Link("GetPlayers",
                      new
                      {
                          pageNumber = playerParamsModel.PageNumber - 1,
                          pageSize = playerParamsModel.PageSize
                      });
                case PageUriType.NextPage:
                    return _urlHelper.Link("GetPlayers",
                      new
                      {
                          pageNumber = playerParamsModel.PageNumber + 1,
                          pageSize = playerParamsModel.PageSize
                      });

                default:
                    return _urlHelper.Link("GetPlayers",
                    new
                    {
                        pageNumber = playerParamsModel.PageNumber,
                        pageSize = playerParamsModel.PageSize
                    });
            }
        }

    }
}
