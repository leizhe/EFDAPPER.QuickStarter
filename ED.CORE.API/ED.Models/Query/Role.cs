using System;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;
//using DapperExtensions.Mapper;
using ED.Models.Auditing;

namespace ED.Models.Query
{
    [DBTable("Role")]
    public class Role : BaseEntityQ, ICreationAudited
    {
        public string RoleName { get; set; }

        public long CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }

        //[Serializable]
        //public sealed class RoleOrmMapper : ClassMapper<RoleDto>
        //{
        //    public RoleOrmMapper()
        //    {
        //        Table("Role");
        //        AutoMap();
        //    }
        //}
    }
}