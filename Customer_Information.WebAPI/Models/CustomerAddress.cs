using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer_Information.WebAPI.Models
{
    public class CustomerAddress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; }
    }
}
