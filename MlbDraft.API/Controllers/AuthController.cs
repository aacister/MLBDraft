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
using MLBDraft.API.Converters;
using MLBDraft.API.Repositories;
using MLBDraft.API.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private ITokenGenerator _tokenGenerator;
        private IPasswordHasher _passwordHasher;
        private ILogger<AuthController> _logger;
        private IUserRepository _userRepository;
        private IMlbDraftRepository _mlbDraftRepository;
        private IMapper _mapper;

        public AuthController(
            IUserRepository userRepository,
            IMlbDraftRepository mlbDraftRepository,
            ILogger<AuthController> logger,
            ITokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IMapper mapper){
            _userRepository = userRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
  
        [HttpPost("login")]
        public IActionResult Login([FromBody] CredentialModel credModel)
        {
                if(credModel == null){
                    return BadRequest();
                }

                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(!_userRepository.UserExists(credModel.UserName))
                {
                    _logger.LogError($"{credModel.UserName} does not exist.");
                    return NotFound();
                }

                var user = _userRepository.GetUser(credModel.UserName);
                if (user.Hash.SequenceEqual(_passwordHasher.Hash(credModel.Password, user.Salt)))
                {
                     var userModel = _mapper.Map<UserModel>(user);
                        //Create token
                        var token = _tokenGenerator.CreateToken(userModel.UserName);
                        
                        if(token.Length >0)
                        {
                            userModel.Token = token;
                            return Ok(userModel);
                        }
                        else
                        {
                            _logger.LogError("Failed to login.  Failed to create token.");
                            throw new Exception("Failed to login.  Failed to create token.");
                        }
                       
                }
                else
                {
                    _logger.LogError("Failed to login.  Failed to create token.");
                    throw new Exception("Failed to login.  Failed to create token.");
                }
        }

 
        [HttpPost("register")]
        public IActionResult Register([FromBody] CredentialModel credModel)
        {
            if(credModel == null){
                    return BadRequest();
                }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(_userRepository.UserExists(credModel.UserName))
            {
                _logger.LogError($"{credModel.UserName} user exists.");
                return Forbid();  //Need to create custom error code for 409 Conflict
            }
                
            var user = _mapper.Map<User>(credModel);
            user.Salt = CreateSalt();
            user.Hash = _passwordHasher.Hash(credModel.Password, user.Salt);
            _userRepository.AddUser(user);
            
            if(!_mlbDraftRepository.Save())
            {
                 _logger.LogError($"Could not register user {credModel.UserName}");
                 throw new Exception($"Could not register user {credModel.UserName}. Failed on save.");
            }
            user = _userRepository.GetUser(credModel.UserName);
            var userModel = _mapper.Map<UserModel>(user);
            //Create Token
            var token = _tokenGenerator.CreateToken(credModel.UserName);
            
            if(token != null && token.Length>0)
            {
                userModel.Token= token;
                return Ok(userModel);  
            }
            else
            {
                _logger.LogError($"Could not register user {credModel.UserName}. Could not create token.");
                 throw new Exception($"Could not register user {credModel.UserName}. Could not create token.");
            }
        }

        private byte[] CreateSalt()
        {
                byte[] salt = new byte[24];
                var keyGenerator = RandomNumberGenerator.Create();
                keyGenerator.GetBytes(salt);
                return salt;
        }

    }
}