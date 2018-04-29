using System;
using System.Collections.Generic;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;
using ED.Models.Auditing;

namespace ED.Models.Query
{
    [DBTable("Role")]
    public class Role : BaseEntityQ, ICreationAudited
    {
        public string RoleName { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }

        [DBIgnore]
        public  ICollection<RolePermission> RolePermissions { get; set; }

        [DBIgnore]
        public  ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserRoles = new HashSet<UserRole>();
        }

       
    }
}