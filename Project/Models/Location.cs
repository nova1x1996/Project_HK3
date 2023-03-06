using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("location")]
    public class Location
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name From 3 To 30 Characters")]
        public string Name { get; set; }    
        public CustomerCare? GetCustomerCare { get; set; }
    }
}
