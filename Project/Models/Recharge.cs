
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{

    [Table("recharge")]
    public class Recharge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string pay_type { get; set; }

        public bool state { get; set; }
        public DateTime date { get; set; }
        public string card_number { set; get; }
        public int  month { get; set; }


        [ForeignKey("customer_id")]
        public Customer GetCustomer { get; set; }
        public int? customer_id { get; set; }

        [ForeignKey("package_id")]
        public Package GetPackage { get; set; }

        public int? package_id { get; set; }
    }
}
