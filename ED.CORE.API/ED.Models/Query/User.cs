using System;
using System.Collections.Generic;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;
using ED.Models.Auditing;

namespace ED.Models.Query
{
    [DBTable("User")]
    public class User : BaseEntityQ, ICreationAudited
    {

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string RealName { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public int State { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            this.UserRoles = new List<UserRole>();
        }
        
    }
}