using System;
using System.ComponentModel.DataAnnotations;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;


namespace MLBDraft.API.Validators {
    public class TeamNameValidationAttribute : ValidationAttribute
    {

        public TeamNameValidationAttribute(){}

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var teamCreateModel = (TeamCreateModel)validationContext.ObjectInstance;
            var teamRepository = (ITeamRepository) validationContext.GetService(typeof(ITeamRepository));
            if(teamRepository.TeamNameExists(teamCreateModel.Name))
            {
                 return new ValidationResult($"Team name {teamCreateModel.Name} exists.");
                        
            }   
            return ValidationResult.Success;
        }

    }
}