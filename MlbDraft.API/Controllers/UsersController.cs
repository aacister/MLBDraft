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

        [HttpGet(Name = "GetUserss")]
        public IActionResult Get()
        {
                var users =  _userRepository.GetUsers();

                if(users == null){
                    _logger.LogWarning("No users were found.");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<UserModel>>(users));

        }

        [HttpGet("{username}", Name = "GetUser")]
        public  IActionResult Get(string username)
        {
           
               User user = null;
                user =  _userRepository.GetUser(username);
                
                if(user == null ) return NotFound($"User {username} was not found.");
                
                return Ok(_mapper.Map<UserModel>(user));
          
        }


        [HttpPut("{username}")]
        public IActionResult Put(string username, [FromBody] UserModel model)
        {
            if(!_userRepository.UserExists(username))
            {
                _logger.LogError($"{username} User does not exist.");
                return NotFound();
            }

                var userEntity = _userRepository.GetUser(username);

                

                _userRepository.UpdateUser(username);
                if(!_mlbDraftRepository.Save())
                {
                    throw new Exception($"Updating user {username} failed on save.");
                }
                
                return Ok(_mapper.Map<UserModel>(userEntity));
        }

        [HttpDelete("{username}")]
        public IActionResult Delete(string username)
        {

            if(!_userRepository.UserExists(username))
            {
                _logger.LogError($"{username} User does not exist.");
                return NotFound();
            }

            var oldUser =  _userRepository.GetUser(username);

            _userRepository.DeleteUser(oldUser);
            if(!_mlbDraftRepository.Save())
            {
                 _logger.LogError($"Could not delete user {username}");
                throw new Exception($"Deleting user {username} failed on save.");
            }
            
            return NoContent();
            
        }

    }
}