namespace Repository.Core
{
    public class ModelBase
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
    }
}
