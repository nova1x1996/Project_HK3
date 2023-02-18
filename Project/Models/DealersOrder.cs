using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Project.Models.Domain;

namespace Project.Models
{
    [Table("dealers_order")]
    public class DealersOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("dealers_id")]
        public Dealers? GetDealer { get; set; }
        public int? dealers_id { get; set; }

        [ForeignKey("setup_box_id")]
        public SetUpBox? GetSetUpBox { get; set; }
        public int? setup_box_id { get; set; }
        public bool status { get; set; }
        public DateTime date { get; set; }
    }
}
