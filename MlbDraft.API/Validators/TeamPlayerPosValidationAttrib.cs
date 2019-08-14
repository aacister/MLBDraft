using System;
using System.ComponentModel.DataAnnotations;
using MLBDraft.API.Models;
using MLBDraft.API.Repositories;


namespace MLBDraft.API.Validators {
    public class TeamPlayerPosValidationAttribute : ValidationAttribute
    {
        private string _position;

        public TeamPlayerPosValidationAttribute(string position)
        {
            _position = position;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var rosterUpdateModel = (DraftTeamRosterUpdateModel)validationContext.ObjectInstance;
            var positionRepository = (IPositionRepository) validationContext.GetService(typeof(IPositionRepository));
            var playerRepository = (IPlayerRepository) validationContext.GetService(typeof(IPlayerRepository));

            if(_position == "C")
            {
                if(rosterUpdateModel.CatcherId.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.CatcherId.Value))
                    {
                        var position = positionRepository.GetPosition("C");
                        var catcher = playerRepository.GetPlayer(rosterUpdateModel.CatcherId.Value);
        
                        if(catcher.PositionId != position.Id)
                        {
                            return new ValidationResult($"Catcher failed validation.");
                        }
                    }
                }
            }
            else if(_position == "1B"){
                if(rosterUpdateModel.FirstBaseId.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.FirstBaseId.Value))
                    {
                        var position = positionRepository.GetPosition("1B");
                        var firstBase = playerRepository.GetPlayer(rosterUpdateModel.FirstBaseId.Value);
        
                        if(firstBase.PositionId != position.Id)
                        {
                            return new ValidationResult($"1B failed validation.");
                        }
                    }
                }
            }
            else if(_position == "2B"){
                if(rosterUpdateModel.SecondBaseId.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.SecondBaseId.Value))
                    {
                        var position = positionRepository.GetPosition("2B");
                        var secondBase = playerRepository.GetPlayer(rosterUpdateModel.SecondBaseId.Value);
                    
                        if(secondBase.PositionId != position.Id)
                        {
                            return new ValidationResult($"2B failed validation.");
                        }
                    }
                }
            }
             else if(_position == "SS"){
                if(rosterUpdateModel.ShortStopId.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.ShortStopId.Value))
                    {
                        var position = positionRepository.GetPosition("SS");
                        var ss = playerRepository.GetPlayer(rosterUpdateModel.ShortStopId.Value);
                        if(ss.PositionId != position.Id)
                        {
                            return new ValidationResult($"SS failed validation.");
                        }
                    }
                }
             }
            else if(_position == "3B"){
                if(rosterUpdateModel.ThirdBaseId.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.ThirdBaseId.Value))
                    {
                        var position = positionRepository.GetPosition("3B");
                        var thirdBase = playerRepository.GetPlayer(rosterUpdateModel.ThirdBaseId.Value);
                        if(thirdBase.PositionId != position.Id)
                        {
                            return new ValidationResult($"3B failed validation.");
                        }
                    }
                }
            }
            else if(_position == "OF1"){
                if(rosterUpdateModel.Outfield1Id.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.Outfield1Id.Value))
                    {
                        var position = positionRepository.GetPosition("OF");
                        var of1 = playerRepository.GetPlayer(rosterUpdateModel.Outfield1Id.Value);
    
                        if(of1.PositionId != position.Id)
                        {
                            return new ValidationResult($"OF1 failed validation.");
                        }
                    }
                }
            }
            else if(_position == "OF2"){
                if(rosterUpdateModel.Outfield2Id.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.Outfield2Id.Value))
                    {
                        var position = positionRepository.GetPosition("OF");
                        var of2 = playerRepository.GetPlayer(rosterUpdateModel.Outfield2Id.Value);
    
                        if(of2.PositionId != position.Id)
                        {
                            return new ValidationResult($"OF2 failed validation.");
                        }
                    }
                }
            }
            else if(_position == "OF3"){

                if(rosterUpdateModel.Outfield3Id.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.Outfield3Id.Value))
                    {
                        var position = positionRepository.GetPosition("OF");
                        var of3 = playerRepository.GetPlayer(rosterUpdateModel.Outfield3Id.Value);
    
                        if(of3.PositionId != position.Id)
                        {
                            return new ValidationResult($"OF3 failed validation.");
                        }
                    }
                }
            }
            else if(_position == "SP"){
                if(rosterUpdateModel.StartingPitcherId.HasValue)
                {
                    if(playerRepository.PlayerExists(rosterUpdateModel.StartingPitcherId.Value))
                    {
                        var position = positionRepository.GetPosition("SP");
                        var sp = playerRepository.GetPlayer(rosterUpdateModel.StartingPitcherId.Value);
    
                        if(sp.PositionId != position.Id)
                        {
                            return new ValidationResult($"SP failed validation.");
                        }
                    }
                }
            }
            else
            {
                return new ValidationResult("Player Position not found.");
            }


            return ValidationResult.Success;
        }

    }
}