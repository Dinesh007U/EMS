using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class ParticipantEventDetails
    {
       
        public Guid ID { get; set; }

        // EmailId of the participant associated with the event
        [Required(AllowEmptyStrings = false, ErrorMessage = "EmailId is required!")]
        public string? ParticipantEmailId { get; set; }

        // EventId of the event associated with the participant
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventId is required!")]
        public Guid EventId { get; set; }

        // Flag indicating whether the participant attended the event or not
        [Column("IsAttended")]
        public bool IsAttended { get; set; }

        // Flag indicating whether the participant event details are deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
