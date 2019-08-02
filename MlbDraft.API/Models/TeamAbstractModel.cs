namespace MLBDraft.API.Models
{
    public abstract class TeamAbstractModel
    {
        public virtual string Name { get; set; }
       
        public virtual string OwnerId { get; set; }
    }
}