using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class ChangePackage
    {
        public int id { get; set; }

        [ForeignKey("customer_id")]
        public Customer? GetCustomer { get; set; }
        public int? customer_id { get; set; }
        public int packageOld{ get; set; }
        public int packageNew { get; set; }
        public int price { get; set; }
        public bool state { get; set; }
        public DateTime date { get; set; }
    }
}
