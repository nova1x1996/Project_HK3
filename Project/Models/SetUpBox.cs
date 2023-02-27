using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Models
{
    [Table("setup_box")]
    public class SetUpBox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }

        [Required]
        
        public string name { get; set; }

        [Required]
        public string details { get; set; }

        public string? img { get; set; }

        

        [Required]
        [Range(100, 1000, ErrorMessage ="Price from 100 to 1000 USD")]
        public int price { get; set; }

        public virtual List<Customer_order>? GetCustomer_Orders { get; set; }

    }
}
