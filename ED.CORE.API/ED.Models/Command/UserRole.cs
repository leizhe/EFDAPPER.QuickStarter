namespace ED.Models.Command
{
    public class UserRole : BaseEntityC
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}