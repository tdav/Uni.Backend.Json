using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Database
{
    public partial class tbData : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; }

        [Required]
        [Column(TypeName = "json")]
        public string DataModel { get; set; }
    }
}
