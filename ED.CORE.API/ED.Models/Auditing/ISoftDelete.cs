namespace ED.Models.Auditing
{
    public interface ISoftDelete
    {
        bool IsDeleted { get;} 
    }
}