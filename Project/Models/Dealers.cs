using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Project.Models.Domain;

namespace Project.Models
{
    [Table("dealers")]
    public class Dealers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("user_id")]
        public virtual ApplicationUser ApplicationUser { set; get; }
        public string? user_id { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}
