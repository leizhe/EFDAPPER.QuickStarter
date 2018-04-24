using System;
using System.Collections.Generic;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;
//using DapperExtensions.Mapper;
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

        public string PhoneNumber { get; set; }

        public long CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public int State { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

        //[Serializable]
        //public sealed class UserOrmMapper : ClassMapper<UserDto>
        //{
        //    public UserOrmMapper()
        //    {
        //        Table("User");
        //        Map(f => f.Roles).Ignore();
        //        AutoMap();
        //    }
        //}

    }
}