using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MLBDraft.API.Entities;
using MLBDraft.API.Repositories;

namespace MLBDraft.API.Security
{
    public class MlbDraftIdentityInitializer : IMlbDraftIdentityInitializer
    {
        private IUserRepository _userRepository;
        private IMlbDraftRepository _mlbDraftRepository;

        public MlbDraftIdentityInitializer(IUserRepository userRepository,
        IMlbDraftRepository mlbDraftRepository){

            _userRepository = userRepository;
            _mlbDraftRepository = mlbDraftRepository;
        }
        public async Task Seed()
        {

                if(!_userRepository.UserExists("username")) {
                    var newUser = new MlbDraftUser()
                    {        
                        UserName = "username",
                        Name = "Test User #1"
                    };

                    await _userRepository.AddUser(newUser, "Password1!");
                    if(!_mlbDraftRepository.Save()){
                        throw new Exception("Could not seed Identity.");
                     }

                }
        }

    }

    
}