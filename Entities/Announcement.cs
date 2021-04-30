using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Entities
{
    public enum AnnouncementTypes
    {
        TaskYouAreAssignedTo,
        TaskOthersAreAssignedTo
        //  Add as required and can be referenced throughout the code base. 
    }

    public class Announcement : EntityBase
    {
        private AnnouncementTypes? announcementType;

        [DataMember]
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type exceeds {1} characters.")]
        public string Type
        {
            get { return announcementType.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    announcementType = null;
                    return;
                }
                if (!Enum.TryParse(value, out AnnouncementTypes type))
                    throw new Exception($"Announcement.Type: {value} is not currently supported.");
                announcementType = type;
            }
        }

        [DataMember]
        [Display(Name = "User")]
        [Required(ErrorMessage = "User is required.")]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
