using System;
using System.ComponentModel.DataAnnotations;

namespace FriendlyQuiz.Entities
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}