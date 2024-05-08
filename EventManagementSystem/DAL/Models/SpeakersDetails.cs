using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    // Model for Speaker Details
    [Table("SpeakersDetails")]
    public class SpeakersDetails
    {
        // Primary key for the speaker
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SpeakerId")]
        public Guid SpeakerId { get; set; }

        // Name of the speaker
        [Required(AllowEmptyStrings = false, ErrorMessage = "SpeakerName is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Column("SpeakerName")]
        public string? SpeakerName { get; set; }

        // Flag indicating whether the speaker is deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

}
