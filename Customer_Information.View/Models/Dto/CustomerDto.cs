using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_Information.View.Models.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        public int CountryId { get; set; }
        [ValidateNever]
        public CountryDto Country { get; set; }
        public string? CountryName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int MeritialStatus { get; set; }
        [ValidateNever]
        public byte[] CustomerPhoto { get; set; }
       // [ValidateNever]
        //public IFormFile CustomerPhotoFile { get; set; }

    }
}
