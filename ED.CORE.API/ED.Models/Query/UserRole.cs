using System;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;

//using DapperExtensions.Mapper;

namespace ED.Models.Query
{
    [DBTable("UserRole")]
    public class UserRole : BaseEntityQ
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }


        //[Serializable]
        //public sealed class UserRoleOrmMapper : ClassMapper<UserRoleDto>
        //{
        //    public UserRoleOrmMapper()
        //    {
        //        Table("UserRole");
        //        Map(f => f.User).Ignore();
        //        Map(f => f.Role).Ignore();
        //        AutoMap();
        //    }
        //}
    }
}