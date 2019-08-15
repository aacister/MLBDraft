using MLBDraft.API.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MLBDraft.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MLBDraftContext _context;
        private UserManager<MlbDraftUser> _userMgr;
        
        public UserRepository(MLBDraftContext context, 
        UserManager<MlbDraftUser> userMgr){
            _context = context;
            _userMgr = userMgr;
        }

         public bool UserExists(string username)
        {
            return _userMgr.Users.Any(r => r.UserName == username);

        }

        public async Task<MlbDraftUser> GetUser(string username)
        {
            var user = await _userMgr.FindByNameAsync(username);
            user.Teams = _context.Teams
                .Include(t => t.Catcher)
                .Include(t => t.FirstBase)
                .Include(t => t.SecondBase)
                .Include(t => t.ThirdBase)
                .Include(t => t.ShortStop)
                .Include(t => t.Outfield1)
                .Include(t => t.Outfield2)
                .Include(t => t.Outfield3)
                .Include(t => t.StartingPitcher)
            .Where(t => t.OwnerId == username).ToList();
            return user;
        }

         public async Task AddUser(MlbDraftUser user, string password)
        { 
            var userResult = await _userMgr.CreateAsync(user, password);
            var claimResult = await _userMgr.AddClaimAsync(user, new Claim("MlbDraftUser", "True"));  

            if(!userResult.Succeeded || !claimResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to build user and claims.");
            }

        }

    }
}