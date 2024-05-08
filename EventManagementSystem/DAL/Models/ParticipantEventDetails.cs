using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    // Model class for Participant Event Details
    [Table("participantEventDetils")]
    public class ParticipantEventDetails
    {
        // Primary key for the participant event details
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }

        // EmailId of the participant associated with the event
        [Required(AllowEmptyStrings = false, ErrorMessage = "EmailId is required!")]
        [Column("participantEmail")]
        public string? ParticipantEmailId { get; set; }

        // EventId of the event associated with the participant
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventId is required!")]
        [Column("EventId")]
        public Guid EventId { get; set; }

        // Flag indicating whether the participant attended the event or not
        [Column("IsAttended")]
        public bool IsAttended { get; set; }

        // Flag indicating whether the participant event details are deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

}
