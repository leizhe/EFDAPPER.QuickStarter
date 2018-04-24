using System;

namespace ED.Models.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; } 
    }
}