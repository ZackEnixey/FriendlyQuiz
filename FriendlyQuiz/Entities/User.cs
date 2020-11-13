using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendlyQuiz.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        public DateTime Created
        {
            get
            {
                return dateCreated ?? DateTime.Now;
            }
            set {dateCreated = value; }
        }

        private DateTime? dateCreated = null;

        public DateTime LastActive
        {
            get
            {
                return lastActive ?? DateTime.Now;
            }
            set { lastActive = value; }
        }

        private DateTime? lastActive = null;

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        [MaxLength(30)]
        public string Email { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
