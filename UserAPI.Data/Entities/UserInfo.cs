using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Data.Entities
{
    public class UserInfo
    {
        [Key]
        public int UserInfoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }


        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? PlaceOfBirth { get; set; }

        [MaxLength(100)]
        public string? JobTitle { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
