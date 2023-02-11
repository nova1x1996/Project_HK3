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

		public int card_number { get; set; }
		public string? name { get; set; }
		public string? password { get; set; }
		public string? email { get; set; }
		public string? phone { get; set; }

    }
}

