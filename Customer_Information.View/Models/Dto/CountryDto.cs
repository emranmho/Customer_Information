using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer_Information.View.Models.Dto
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
