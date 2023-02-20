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
        public string Phone { get; set; }

        [ForeignKey("location_id")]
        public Location? GetLocation { get; set; }

        public int? location_id { get; set; }
    }
}
