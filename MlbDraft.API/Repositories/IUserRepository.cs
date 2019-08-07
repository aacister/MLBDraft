using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IUserRepository
    {
        bool UserExists(string username);
        User GetUser(string username);
        IEnumerable<User> GetUsers();
     

        void AddUser(User user);

        bool UpdateUser(string username);

        void DeleteUser(User user);


    }
}