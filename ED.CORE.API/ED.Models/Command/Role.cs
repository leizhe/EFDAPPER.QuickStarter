using System;
using System.Collections.Generic;
using ED.Models.Auditing;

namespace ED.Models.Command
{
    public class Role : BaseEntityC, ICreationAudited
    {
        public string RoleName { get; set; }

        public long CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }
        

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        
    }
}