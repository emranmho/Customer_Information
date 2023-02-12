using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_Information.WebAPI.Models
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CountryName { get; set; }
    }
}
