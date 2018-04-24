using System.Collections.Generic;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;

namespace ED.Models.Query
{
    [DBTable("Permission")]
    public class Permission : BaseEntityQ
    {
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}