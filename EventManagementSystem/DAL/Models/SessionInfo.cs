using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    //Model for SessionInfo

    // Model for Session Information
    [Table("SessionInfo")]
    public class SessionInfo
    {
        // Primary key for the session
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SessionId")]
        public Guid SessionId { get; set; }

        // Foreign key referencing the event for which this session belongs
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventId is required!")]
        [Column("EventId")]
        public Guid EventId { get; set; }

        // Title of the session
        [Required(AllowEmptyStrings = false, ErrorMessage = "SessionTitle is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Column("SessionTitle")]
        public string? SessionTitle { get; set; }

        // Speaker Id associated with the session
        [Column("SpeakerId")]
        public Guid SpeakerId { get; set; }

        // Description of the session
        [Column("Description")]
        public string? Description { get; set; }

        // Start time of the session
        [Required(AllowEmptyStrings = false, ErrorMessage = "SessionStart is required!")]
        [Column("StartTime")]
        public DateTime SessionStart { get; set; }

        // End time of the session
        [Required]
        [Column("EndTime")]
        public DateTime SessionEnd { get; set; }

        // URL associated with the session 
        [Column("SessionUrl")]
        public string? SessionUrl { get; set; }

        // Flag indicating whether the session is deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

}
