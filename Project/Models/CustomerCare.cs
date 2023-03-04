using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("customercare")]
    public class CustomerCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Phone From 10 To 11 Numbers")]
        public string Phone { get; set; }

        [ForeignKey("location_id")]
        public Location? GetLocation { get; set; }

        public int? location_id { get; set; }
    }
}
