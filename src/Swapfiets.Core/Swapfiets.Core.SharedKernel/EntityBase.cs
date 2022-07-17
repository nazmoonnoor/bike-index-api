namespace Swapfiets.Core.SharedKernel
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? CreatedBy { get; set; }
    }
}
