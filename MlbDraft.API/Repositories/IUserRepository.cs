using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLBDraft.API.Repositories
{
    public interface IUserRepository
    {
        bool UserExists(string username);
        Task<MlbDraftUser> GetUser(string username);
        Task AddUser(MlbDraftUser user, string password);

  


    }
}