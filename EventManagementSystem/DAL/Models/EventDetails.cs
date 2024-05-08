using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{


    [Table("EventDetails")]
    public class EventDetails
    {
        // Primary key for the event
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("EventId")]
        public Guid EventId { get; set; }

        // Name of the event
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventName is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Column("EventName")]
        public string? EventName { get; set; }

        // Category of the event
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventCategory is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Column("EventCategory")]
        public string? EventCategory { get; set; }

        // Date of the event
        [Required(AllowEmptyStrings = false, ErrorMessage = "EventDate is required!")]
        [Column("EventDate")]
        public DateTime EventDate { get; set; }

        // Description of the event
        [Column("EventDescription")]
        public string? Description { get; set; }

        // Status of the  event (Active/In-Active)
        [Required]
        [RegularExpression("Active|In-Active")]
        [Column("Status")]
        public string? Status { get; set; }

        // Flag indicating whether the event is deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

}
