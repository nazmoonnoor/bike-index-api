namespace Swapfiets.Theft.Core.Domains
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? CreatedBy { get; set; }
    }
}
