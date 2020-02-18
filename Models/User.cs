using System;
using System.ComponentModel.DataAnnotations;
namespace asp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastSeen { get; set; }
        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Suspended")]
        public bool IsSuspended {get; set;}

    }
}