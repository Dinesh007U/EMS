using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AppUI.Models
{
    /// <summary>
    /// Creating Model class for handle event details data
    /// </summary>
    public class EventDetails
    {
        public Guid EventId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Event Name")]
        public string EventName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Event Category")]
        public string EventCategory { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Event Date")]
        public DateTime EventDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Event Description")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Event Status")]
        public string Status { get; set; }
      
        public bool IsDeleted { get; set; }
    }
}
