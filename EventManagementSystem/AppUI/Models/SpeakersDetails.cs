using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AppUI.Models
{
   // Model for Speaker Details
    public class SpeakersDetails
    {
        
        public Guid SpeakerId { get; set; }

        // Name of the speaker
        [Required(AllowEmptyStrings = false, ErrorMessage = "SpeakerName is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string? SpeakerName { get; set; }

        // Flag indicating whether the speaker is deleted or not
        public bool IsDeleted { get; set; }
    }

}
