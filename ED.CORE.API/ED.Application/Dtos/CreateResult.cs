using System.Runtime.InteropServices.ComTypes;

namespace ED.Application.Dtos
{
    public class CreateResult<T> : OutputBase
    {
        public T Id { get; set; }
        public bool IsCreated { get; set; }
    }
}