using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLBDraft.API.Entities;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;
using MLBDraft.API.Converters;



namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private ILogger<UsersController> _logger;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private IMlbDraftRepository _mlbDraftRepository;

        public UsersController(IUserRepository userRepository,
            IMlbDraftRepository mlbDraftRepository,
            ILogger<UsersController> logger,
            IMapper mapper){
            _userRepository= userRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet("{username}", Name = "GetUser")]
        public  async Task<IActionResult> Get(string username)
        {
            if(!_userRepository.UserExists(username))
            {
                _logger.LogError($"User {username} not found.");
                return NotFound();
            }
            
            var user =   await _userRepository.GetUser(username);
            return Ok(_mapper.Map<UserModel>(user));
          
        }

       

    }
}