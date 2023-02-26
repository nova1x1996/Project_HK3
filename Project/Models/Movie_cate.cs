using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("movie_cate")]
    public class Movie_cate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        [Required]
        [StringLength(50,MinimumLength = 3,ErrorMessage = "Name of Category must be from 3 to 50 characters")]
        public string name { get; set; }
        public List<Movie>? movies { get; set; }
    }
}
