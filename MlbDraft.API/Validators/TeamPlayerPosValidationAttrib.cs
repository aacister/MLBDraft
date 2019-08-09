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
            var teamUpdateModel = (TeamUpdateModel)validationContext.ObjectInstance;
            var positionRepository = (IPositionRepository) validationContext.GetService(typeof(IPositionRepository));
            var playerRepository = (IPlayerRepository) validationContext.GetService(typeof(IPlayerRepository));

            if(_position == "C")
            {
                if(teamUpdateModel.CatcherId.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.CatcherId.Value))
                    {
                        var position = positionRepository.GetPosition("C");
                        var catcher = playerRepository.GetPlayer(teamUpdateModel.CatcherId.Value);
        
                        if(catcher.PositionId != position.Id)
                        {
                            return new ValidationResult($"Catcher failed validation.");
                        }
                    }
                }
            }
            else if(_position == "1B"){
                if(teamUpdateModel.FirstBaseId.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.FirstBaseId.Value))
                    {
                        var position = positionRepository.GetPosition("1B");
                        var firstBase = playerRepository.GetPlayer(teamUpdateModel.FirstBaseId.Value);
        
                        if(firstBase.PositionId != position.Id)
                        {
                            return new ValidationResult($"1B failed validation.");
                        }
                    }
                }
            }
            else if(_position == "2B"){
                if(teamUpdateModel.SecondBaseId.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.SecondBaseId.Value))
                    {
                        var position = positionRepository.GetPosition("2B");
                        var secondBase = playerRepository.GetPlayer(teamUpdateModel.SecondBaseId.Value);
                    
                        if(secondBase.PositionId != position.Id)
                        {
                            return new ValidationResult($"2B failed validation.");
                        }
                    }
                }
            }
             else if(_position == "SS"){
                if(teamUpdateModel.ShortStopId.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.ShortStopId.Value))
                    {
                        var position = positionRepository.GetPosition("SS");
                        var ss = playerRepository.GetPlayer(teamUpdateModel.ShortStopId.Value);
                        if(ss.PositionId != position.Id)
                        {
                            return new ValidationResult($"SS failed validation.");
                        }
                    }
                }
             }
            else if(_position == "3B"){
                if(teamUpdateModel.ThirdBaseId.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.ThirdBaseId.Value))
                    {
                        var position = positionRepository.GetPosition("3B");
                        var thirdBase = playerRepository.GetPlayer(teamUpdateModel.ThirdBaseId.Value);
                        if(thirdBase.PositionId != position.Id)
                        {
                            return new ValidationResult($"3B failed validation.");
                        }
                    }
                }
            }
            else if(_position == "OF1"){
                if(teamUpdateModel.Outfield1Id.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.Outfield1Id.Value))
                    {
                        var position = positionRepository.GetPosition("OF");
                        var of1 = playerRepository.GetPlayer(teamUpdateModel.Outfield1Id.Value);
    
                        if(of1.PositionId != position.Id)
                        {
                            return new ValidationResult($"OF1 failed validation.");
                        }
                    }
                }
            }
            else if(_position == "OF2"){
                if(teamUpdateModel.Outfield2Id.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.Outfield2Id.Value))
                    {
                        var position = positionRepository.GetPosition("OF");
                        var of2 = playerRepository.GetPlayer(teamUpdateModel.Outfield2Id.Value);
    
                        if(of2.PositionId != position.Id)
                        {
                            return new ValidationResult($"OF2 failed validation.");
                        }
                    }
                }
            }
            else if(_position == "OF3"){

                if(teamUpdateModel.Outfield3Id.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.Outfield3Id.Value))
                    {
                        var position = positionRepository.GetPosition("OF");
                        var of3 = playerRepository.GetPlayer(teamUpdateModel.Outfield3Id.Value);
    
                        if(of3.PositionId != position.Id)
                        {
                            return new ValidationResult($"OF3 failed validation.");
                        }
                    }
                }
            }
            else if(_position == "SP"){
                if(teamUpdateModel.StartingPitcherId.HasValue)
                {
                    if(playerRepository.PlayerExists(teamUpdateModel.StartingPitcherId.Value))
                    {
                        var position = positionRepository.GetPosition("SP");
                        var sp = playerRepository.GetPlayer(teamUpdateModel.StartingPitcherId.Value);
    
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