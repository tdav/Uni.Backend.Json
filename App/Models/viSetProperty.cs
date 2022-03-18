using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class viSetProperty
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
