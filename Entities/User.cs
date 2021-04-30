using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities
{
    public class User : EntityBase
    {
        [NotMapped]
        public string Name => $"{FirstName} {LastName}";

        [DataMember]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name exceeds {1} characters.")]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(150, ErrorMessage = "Last name exceeds {1} characters.")]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(320, ErrorMessage = "Email exceeds {1} characters.")]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }

        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
    }
}
