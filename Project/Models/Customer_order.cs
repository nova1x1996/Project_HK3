using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Project.Models
{
    [Table("customer_order")]
    public class Customer_order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string pay_type { get; set; }
        public decimal total_money { get; set; }
        public bool state { get; set; }
        public int? monthPackage { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("customer_id")]
        public Customer GetCustomer{ get; set; }
        public int? customer_id { get; set; }

        [ForeignKey("package_id")]
        public Package GetPackage { get; set; }
        public int? package_id { get; set; }

        [ForeignKey("movie_id")]
        public Movie GetMovie { get; set; }
        public int? movie_id { get; set; }

        [ForeignKey("setUpBox_id")]
        public SetUpBox GetSetUpBox { get; set; }
        public int? setUpBox_id { get; set; }

    }
}
