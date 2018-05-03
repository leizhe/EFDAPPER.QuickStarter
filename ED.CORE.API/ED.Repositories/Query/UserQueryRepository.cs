using System.Collections.Generic;
using System.Linq;
using Dapper;
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

        public IQueryable<User> GetAll()
        {
          
            using (var db = Context.GetConnection())
            {
                var sql = @"SELECT * FROM [User] AS u 
                        LEFT JOIN [UserRole] AS ur ON u.Id=ur.UserId
                        LEFT JOIN [Role] AS r ON ur.RoleId=r.Id";
                //var lookup = new Dictionary<long, User>();
                var q= db.Query<User, UserRole, Role, User>(sql, (u, ur, r) =>
                    {
                        var lst = new List<User>();
                        var tmp = lst.FirstOrDefault(p => p.Id == u.Id);
                        if (tmp == null)
                        {
                            tmp = u;
                        }

                        var tmpUr = tmp.UserRoles.FirstOrDefault(p => p.Id == ur.Id);
                        if (tmpUr == null)
                        {
                            if (ur!=null)
                            {
                                tmpUr = ur;
                                tmp.UserRoles.Add(tmpUr);
                                tmpUr.Role = r;
                            }
                           
                        }
                        return u;
                    },
                    splitOn: "Id").AsQueryable();
                return q;
                // return lst.AsQueryable();
            }
        }

       
    }
}