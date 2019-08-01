using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return _context.Users.FirstOrDefault(a => a.Username == username);
        }

        public IEnumerable<User> GetUsers(){
            return _context.Users
                .OrderBy(a => a.Username)
                .ToList();
        }
        public IEnumerable<User> GetUsers(IEnumerable<string> usernames)
        {
            return _context.Users.Where(a => usernames.Contains(a.Username))
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