using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Service.Models
{
    public class UserInfoForCreationDto
    {
        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;


        public string? Phone { get; set; }


        [MaxLength(100)]
        public string? PlaceOfBirth { get; set; }


        [MaxLength(100)]
        public string? JobTitle { get; set; }
    }
}
