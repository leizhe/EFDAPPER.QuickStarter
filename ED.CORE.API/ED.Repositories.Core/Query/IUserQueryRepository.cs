using System.Collections.Generic;
using System.Linq;
using ED.Models.Query;

namespace ED.Repositories.Core.Query
{
    public interface IUserQueryRepository : IDapperQueryRepository<User>
    {
        IQueryable<User> GetAll();

        //User GetById();
    }
}