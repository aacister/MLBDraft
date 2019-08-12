using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MLBDraftContext _context;
        
        public UserRepository(MLBDraftContext context){
            _context = context;
        }

         public bool UserExists(string username)
        {
            return _context.Users.Any(a => a.Username == username);
        }

        public User GetUser(string username)
        {
            return _context.Users
            .Include(u => u.Teams)  
                .ThenInclude(t => t.League)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Catcher)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.FirstBase)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.SecondBase)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.ThirdBase)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.ShortStop)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Outfield1)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Outfield2)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Outfield3)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.StartingPitcher)
            .FirstOrDefault(a => a.Username == username);
        }

        public IEnumerable<User> GetUsers(){
            return _context.Users
                .Include(u => u.Teams)
                    .ThenInclude(t => t.League)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Catcher)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.FirstBase)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.SecondBase)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.ThirdBase)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.ShortStop)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Outfield1)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Outfield2)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Outfield3)
                .Include(u => u.Teams)
                    .ThenInclude(t => t.StartingPitcher)
                .OrderBy(a => a.Username)
                .ToList();
        }

         public void AddUser(User user)
        { 
            _context.Users.Add(user);

        }

        public bool UpdateUser(string username)
        {

            return true;
        }

        public void DeleteUser(User user)
        {

            _context.Users.Remove(user);
           
        }
    }
}