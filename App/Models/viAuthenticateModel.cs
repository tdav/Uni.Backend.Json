using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class viAuthenticateModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
