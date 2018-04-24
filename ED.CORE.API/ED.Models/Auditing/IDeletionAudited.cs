using System;

namespace ED.Models.Auditing
{
    public interface IDeletionAudited : ISoftDelete
    {

        long DeleterUserId { get; set; }

       
        DateTime? DeletionTime { get; set; } 
    }
}