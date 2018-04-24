using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;

namespace ED.Models.Query
{
    [DBTable("UserRole")]
    public class UserRole : BaseEntityQ
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
        
    }
}