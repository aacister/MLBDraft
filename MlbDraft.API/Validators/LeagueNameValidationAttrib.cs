using System;
using System.ComponentModel.DataAnnotations;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;


namespace MLBDraft.API.Validators {
    public class LeagueNameValidationAttribute : ValidationAttribute
    {
        public LeagueNameValidationAttribute(){}

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var leagueCreateModel = (LeagueCreateModel)validationContext.ObjectInstance;
            var leagueRepository = (ILeagueRepository) validationContext.GetService(typeof(ILeagueRepository));
            if(leagueRepository.LeagueNameExists(leagueCreateModel.Name))
            {
                return new ValidationResult($"League name {leagueCreateModel.Name} exists.");           
            }
            return ValidationResult.Success;
        }

    }
}