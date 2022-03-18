using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;

namespace App.Database
{
    /// <summary>
    /// таблица Пользователи
    /// </summary>

    public partial class tbUser : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SurName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(100)]
        public string Patronymic { get; set; }


        [IndexColumn(IsUnique = true)]
        [StringLength(10)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
        public spRole Role { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{SurName} {Name} {Patronymic}";
        }
    }
}
