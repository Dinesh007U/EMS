using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AppUI.Models
{
    public class SessionInfo
    {

        
        public Guid SessionId { get; set; }

        // Foreign key referencing the event for which this session belongs
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventId is required!")]
        public Guid EventId { get; set; }

        // Title of the session
        [Required(AllowEmptyStrings = false, ErrorMessage = "SessionTitle is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string? SessionTitle { get; set; }

        // Speaker Id associated with the session
        public Guid SpeakerId { get; set; }

        // Description of the session
        public string? Description { get; set; }

        // Start time of the session
        [Required(AllowEmptyStrings = false, ErrorMessage = "SessionStart is required!")]
        public DateTime SessionStart { get; set; }

        // End time of the session
        [Required]
        public DateTime SessionEnd { get; set; }

        // URL associated with the session 
        public string? SessionUrl { get; set; }

        // Flag indicating whether the session is deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
