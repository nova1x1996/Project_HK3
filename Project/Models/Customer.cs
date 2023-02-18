using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Project.Models.Domain;

namespace Project.Models
{
    [Table("customer")]

    public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }

		public string card_number { get; set; }
	
	
		public string phone { get; set; }
		public string address { get; set; }

        [ForeignKey("user_id")]
        public virtual ApplicationUser ApplicationUser { set; get; }
        public string? user_id { get; set; }

        public DateTime? services_sub_date { get; set; }
		public DateTime? date_left { get; set; }
		public decimal? payment_monthly { get; set; }

        [ForeignKey("package_id")]
        public virtual Package package { set; get; }
        public int? package_id { get; set; }

        public virtual List<Customer_order>? GetCustomer_Orders { get; set; }
        public virtual List<Recharge>? GetRecharges { get; set; }

    }
}

