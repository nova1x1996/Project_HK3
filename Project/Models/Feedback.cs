using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string type { get; set; }

     

        [Required]
        public int star { get; set; }
        [Required]
        [StringLength(250,MinimumLength =3,ErrorMessage = "Content must be from 3 to 250 characters.")]
        public string content { get; set; }

        public DateTime? date { get; set; }


        [ForeignKey("customer_id")]
        public Customer? GetCustomer { get; set; }

        public int customer_id { set; get; }
    }
}
