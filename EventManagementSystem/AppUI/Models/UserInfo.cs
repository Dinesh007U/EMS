using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{

    // Model class for User Information
    public class UserInfo
    {
        // Primary key for the user, using EmailId as a unique identifier

        [Required(ErrorMessage ="Enter EmailId")]
        public string? EmailId { get; set; }

        // Username of the user
       
        public string? UserName { get; set; }

        // Role of the user (Admin/Participant)
       
        public string? Role { get; set; }

        // Password of the user
        [Required(ErrorMessage ="Enter password"), StringLength(maximumLength: 20, MinimumLength = 6)]
        public string? Password { get; set; }

        // Flag indicating whether the user is deleted or not
        public bool IsDeleted { get; set; }
    }

}
