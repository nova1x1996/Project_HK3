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
        public string name { get; set; }
        public int duration { get; set; }
        public string details { get; set; }
        public string status { get; set; }
        public decimal price { get; set; }
        public virtual List<Customer>? customers { get; set; }
    }
}
