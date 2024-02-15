using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Service.Models
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "Please enter your user name")]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter your email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter your password")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
