using ED.Models.Command;
using ED.Repositories.Core.Command;
using ED.Repositories.EntityFramework;

namespace ED.Repositories.Command
{
    public class UserCommandRepository : EntityFrameworkRepositoryBase<User>, IUserCommandRepository
    {
         
    }
}