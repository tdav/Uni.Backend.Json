using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class viUserRegister
    {
        [Required]
        [StringLength(100)]
        public string SurName { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Patronymic { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }
        public int RegionId { get; set; }
        public int DistrictId { get; set; }
    }

}
