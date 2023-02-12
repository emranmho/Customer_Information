using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Customer_Information.WebAPI.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string FatherName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string MotherName { get; set; }
        public int MeritialStatus { get; set; }
        public byte[]? CustomerPhoto { get; set; }
    }
}
