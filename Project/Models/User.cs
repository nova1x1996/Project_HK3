using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    [Table("user")]

    public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }

		public string card_number { get; set; }
		public string name { get; set; }
		public string password { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string address { get; set; }
		
		public DateTime? services_sub_date { get; set; }
		public DateTime? date_left { get; set; }
		public decimal? payment_monthly { get; set; }

        [ForeignKey("package_id")]
        public virtual Package package { set; get; }
        public int? package_id { get; set; }
		
    }
}

