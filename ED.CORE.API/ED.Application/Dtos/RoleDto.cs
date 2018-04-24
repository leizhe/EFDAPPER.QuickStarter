using System.Collections.Generic;

namespace ED.Application.Dtos
{
    public class RoleDto : BaseEntityDto
    {
        public List<BaseEntityDto> Permissions { get; set; } 
    }
}