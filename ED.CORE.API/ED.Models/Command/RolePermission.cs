namespace ED.Models.Command
{
    public class RolePermission : BaseEntityC
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Permission Permission { get; set; }
    }
}