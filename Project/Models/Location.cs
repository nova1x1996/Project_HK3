using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("location")]
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }    
        public CustomerCare GetCustomerCare { get; set; }
    }
}
