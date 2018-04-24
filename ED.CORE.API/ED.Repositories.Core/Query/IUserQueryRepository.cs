using ED.Models.Query;

namespace ED.Repositories.Core.Query
{
    public interface IUserQueryRepository : IDapperQueryRepository<User>
    {
     
        bool Exist(string username, string password);

        bool IsHasSameName(string name, int userId);

    }
}