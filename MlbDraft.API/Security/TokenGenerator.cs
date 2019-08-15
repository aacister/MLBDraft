using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MLBDraft.API.Entities;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;
using MLBDraft.API.Converters;
using AutoMapper;

namespace MLBDraft.API.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private IOptions<TokenGeneratorOptions> _options;
        private ILogger<TokenGenerator> _logger;

        private UserManager<MlbDraftUser> _userMgr;
        private IMapper _mapper;
        
        public TokenGenerator(IOptions<TokenGeneratorOptions> options,
            UserManager<MlbDraftUser> userMgr,
            IUserRepository userRepository,
            ILogger<TokenGenerator> logger,
            IMapper mapper){
                _options = options;
                _userMgr = userMgr;
                _logger = logger;
                _mapper = mapper;
        }
        public async Task<string> CreateToken(string username)
        {
            var token = string.Empty;
            try
            {

                var now = DateTime.UtcNow;

                var user = await _userMgr.FindByNameAsync(username);
                if (user != null)
                {
                    var userClaims = await _userMgr.GetClaimsAsync(user);
         
                    var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
                    }.Union(userClaims);


                    var jwt = new JwtSecurityToken(
                        issuer: _options.Value.Issuer,
                        audience: _options.Value.Audience,
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(_options.Value.Expiration),
                        signingCredentials: _options.Value.SigningCredentials);
                    
                    token = new JwtSecurityTokenHandler().WriteToken(jwt);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while creating JWT: {ex}");
                throw;
            }

            return token; 

        }

        private static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}