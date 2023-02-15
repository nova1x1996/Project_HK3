using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("package")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name from 3 to 50 characters.")]
        public string name { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Duration from 1 to 100 month.")]
        public int? duration { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Details from 3 to 50 characters.")]
        public string details { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Status from 3 to 50 characters.")]
        public string status { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Price from 1$ to 10000$.")]
        public decimal? price { get; set; }
        public virtual List<Customer>? customers { get; set; }
    }
}
