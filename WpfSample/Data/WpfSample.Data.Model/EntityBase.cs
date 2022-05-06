namespace WpfSample.Data.Model
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }
    }
}
