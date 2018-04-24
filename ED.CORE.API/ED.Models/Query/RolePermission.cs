using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;

namespace ED.Models.Query
{
    [DBTable("RolePermission")]
    public class RolePermission : BaseEntityQ
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        public virtual Command.Role Role { get; set; }

        public virtual Command.Permission Permission { get; set; }
    }
}