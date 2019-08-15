using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MLBDraft.API.Entities;

namespace MLBDraft.API.Security
{
    public class MlbDraftIdentityInitializer : IMlbDraftIdentityInitializer
    {
        private UserManager<MlbDraftUser> _userMgr;

        public MlbDraftIdentityInitializer(UserManager<MlbDraftUser> userMgr){

            _userMgr = userMgr;
        }
        public async Task Seed()
        {
                var user = await _userMgr.FindByNameAsync("shawnwildermuth");

                if (user == null)
                {
                    user = new MlbDraftUser()
                    {        
                        UserName = "username",
                        Name = "Test User #1"
                    };

                    var userResult = await _userMgr.CreateAsync(user, "password");
                    var claimResult = await _userMgr.AddClaimAsync(user, new Claim("MlbDraftUser", "True"));

                    if (!userResult.Succeeded || !claimResult.Succeeded)
                    {
                    throw new InvalidOperationException("Failed to build user and roles");
                    }
                }
        }

            
        
    }

    
}