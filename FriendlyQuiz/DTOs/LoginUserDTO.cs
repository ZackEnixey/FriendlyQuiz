using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendlyQuiz.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [StringLength(maximumLength:15,MinimumLength =3, ErrorMessage ="Username must be between 3 and 15 characters")]
        public string Username { get; set; }
        [StringLength(maximumLength:20,MinimumLength =6,ErrorMessage ="Password must be between 6 and 20 characters")]
        [Required]
        public string Password { get; set; }
    }
}
