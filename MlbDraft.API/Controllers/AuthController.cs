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
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace MLBDraft.API.Controllers
{
    [EnableCors("MlbDraftCors")]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private ITokenGenerator _tokenGenerator;
        private ILogger<AuthController> _logger;
        private IUserRepository _userRepository;
        private IMlbDraftRepository _mlbDraftRepository;
        private IMapper _mapper;
        private SignInManager<MlbDraftUser> _signInMgr;

        private UserManager<MlbDraftUser> _userMgr;
        private IPasswordHasher<MlbDraftUser> _hasher;
        

        public AuthController(
            IUserRepository userRepository,
            IMlbDraftRepository mlbDraftRepository,
            ILogger<AuthController> logger,
            ITokenGenerator tokenGenerator,
            SignInManager<MlbDraftUser> signInMgr,
            UserManager<MlbDraftUser> userMgr,
            IPasswordHasher<MlbDraftUser> hasher,
            IMapper mapper){
            _userRepository = userRepository;
            _mlbDraftRepository = mlbDraftRepository;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _signInMgr = signInMgr;
            _userMgr = userMgr;
            _hasher = hasher;
            _mapper = mapper;
        }
  
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel credModel)
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

                 var user = await _userRepository.GetUser(credModel.UserName);
                if(_hasher.VerifyHashedPassword(user, user.PasswordHash,credModel.Password) == PasswordVerificationResult.Success)
                {
                    //Create Token
                    var token = await _tokenGenerator.CreateToken(credModel.UserName);
                    
                    if(token != null && token.Length>0)
                    {
                        var userModel = _mapper.Map<UserModel>(user);
                        userModel.Token= token;
                        await _signInMgr.SignInAsync(user, false);
                        return Ok(userModel);  
                    }
                    else
                    {
                        _logger.LogError($"Failed to login user {credModel.UserName}.");
                        throw new Exception($"Failed to login user {credModel.UserName}.");
                    }
                }
                else
                {
                    _logger.LogError($"Failed to login user {credModel.UserName}.");
                    throw new Exception($"Failed to login user {credModel.UserName}.");
                }

        }

 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CredentialRegisterModel credModel)
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
                return Forbid();  
            }

            var newUser = new MlbDraftUser()
            {        
                    UserName = credModel.UserName,
                    Name = credModel.Name
            };
            await _userRepository.AddUser(newUser, credModel.Password);

            var user = await _userRepository.GetUser(credModel.UserName);
            if(_hasher.VerifyHashedPassword(user, user.PasswordHash,credModel.Password) == PasswordVerificationResult.Success)
            {
                //Create Token
                var token = await _tokenGenerator.CreateToken(credModel.UserName);
                
                if(token != null && token.Length>0)
                {
                    var userModel = _mapper.Map<UserModel>(user);
                    userModel.Token= token;
                    await _signInMgr.SignInAsync(user, false);
                    return Ok(userModel);  
                }
                else
                {
                    _logger.LogError($"Could not register user {credModel.UserName}. Could not create token.");
                    throw new Exception($"Could not register user {credModel.UserName}. Could not create token.");
                }
            }
            else
            {
                _logger.LogError($"Could not register user {credModel.UserName}. Could not create token.");
                    throw new Exception($"Could not register user {credModel.UserName}. Could not create token.");
            }
            
        }


    }
}