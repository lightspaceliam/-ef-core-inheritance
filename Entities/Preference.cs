using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities
{
    [Table("Preferences")]
    public abstract class Preference : EntityBase
    {
        [StringLength(150, ErrorMessage = "Discriminator e")]
        public string Discriminator { get; set; }

        [DataMember]
        [Display(Name = "User")]
        [Required(ErrorMessage = "User is required.")]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }

    public class AnnouncementPreference : Preference
    {
        [DataMember]
        [Display(Name = "Announcement")]
        [Required(ErrorMessage = "Announcement is required.")]
        public Guid AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }
    }

    public enum UserPreferenceTypes
    {
        DateFormat,
        //  Add as required and can be referenced throughout the code base. 
    }

    public class UserPreference : Preference
    {
        private UserPreferenceTypes? userPreferenceType;

        [DataMember]
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type exceeds {1} characters.")]
        public string Type
        {
            get { return userPreferenceType.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    userPreferenceType = null;
                    return;
                }
                if (!Enum.TryParse(value, out UserPreferenceTypes type))
                    throw new Exception($"UserPreference.Type: {value} is not currently supported.");
                userPreferenceType = type;
            }
        }

        /// <summary>
        /// Generic field with no length constraint. Could store anything from a date format, JSON, …
        /// </summary>
        [DataMember]
        [Display(Name = "Requirement")]
        [Required(ErrorMessage = "Requirement is required.")]
        public string Requirement { get; set; }
    }
}
