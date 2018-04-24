
namespace ED.Models.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        long? CreatorUserId { get; } 
    }
}