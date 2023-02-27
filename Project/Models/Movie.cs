using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("movie")]
    public class Movie
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        [Required]
        [StringLength(150,MinimumLength = 3,ErrorMessage ="Name From 3 To 150 Characters")]
        public string name { get; set; }

        public string? img { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Name From 3 To 250 Characters")]
        public string content { get; set; }


        [Required]
        [Range(3,500,ErrorMessage ="Price from 3 to 500 $")]
        public decimal price { get; set; }


        [ForeignKey("movie_cate_id")]
        public Movie_cate? movie_Cate { set; get; }
        [Required]
        public int movie_cate_id { set; get; }

        public virtual List<Customer_order>? GetCustomer_Orders { get; set; }
    }
}
