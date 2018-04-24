using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ED.Models.Auditing;

namespace ED.Models.Command
{
    public class User : BaseEntityC, ICreationAudited
    {

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public string Email { get; set; }

        public string RealName { get; set; }

        public string PhoneNumber { get; set; }

        public long CreatorUserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }

        public int State { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new List<UserRole>();
        }


      
    }
}