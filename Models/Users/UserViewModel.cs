using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models.Users
{
    /// <summary>
    /// Model for single user
    /// </summary>
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? Modified { get; set; }
        public int BooksBorrowed { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveString { get; set; }
    }
}
