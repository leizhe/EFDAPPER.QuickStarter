using ED.Models.Query;
using ED.Repositories.Core.Query;
using ED.Repositories.Dapper;

namespace ED.Repositories.Query
{
    public class UserQueryRepository : DapperRepositoryBase<User>, IUserQueryRepository
    {
        public UserQueryRepository(DapperContext context) : base(context)
        {
        }
    }
}