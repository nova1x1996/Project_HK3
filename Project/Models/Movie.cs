﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("movie")]
    public class Movie
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        public string name { get; set; }
        public string? img { get; set; }
        public string content { get; set; }

        
        public decimal price { get; set; }


        [ForeignKey("movie_cate_id")]
        public Movie_cate? movie_Cate { set; get; }
        public int movie_cate_id { set; get; }

        public virtual List<Customer_order>? GetCustomer_Orders { get; set; }
    }
}
