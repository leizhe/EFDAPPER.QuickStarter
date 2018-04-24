using System.Collections.Generic;

namespace ED.Models.Command
{
    public class Permission : BaseEntityC
    {
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}