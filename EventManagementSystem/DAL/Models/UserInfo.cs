using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    // Model class for User Information
    [Table("UserInfo")]
    public class UserInfo
    {
        // Primary key for the user, using EmailId as a unique identifier
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("EmailId")]
        public string? EmailId { get; set; }

        // Username of the user
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Column("UserName")]
        public string? UserName { get; set; }

        // Role of the user (Admin/Participant)
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is required!")]
        [RegularExpression("Admin|Participant")]
        [Column("Role")]
        public string? Role { get; set; }

        // Password of the user
        [Required, StringLength(maximumLength: 20, MinimumLength = 6)]
        [Column("Password")]
        public string? Password { get; set; }

        // Flag indicating whether the user is deleted or not
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

}
