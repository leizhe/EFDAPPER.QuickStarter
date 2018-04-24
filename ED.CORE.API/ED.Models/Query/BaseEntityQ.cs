

using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;

namespace ED.Models.Query
{
    public class BaseEntityQ
    {
        [DBKey]
        public long Id { get; set; }
    }
}