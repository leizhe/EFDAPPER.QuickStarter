using System;

namespace ED.Models.Auditing
{
    public interface IModificationAudited
    {

        DateTime? LastModificationTime { get; set; }


        long? LastModifierUserId { get; set; }
    }
}