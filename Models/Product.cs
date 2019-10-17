using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class Product 
    {
        [Key]
        public int ProductId {get;set;}
        [Required]
        [MinLength(2)]
        public string Name {get;set;}
        [Required]
        [MaxLength(100)]
        public string Description {get;set;}
        [Required]
        [Range(0.1, Double.PositiveInfinity)]
        public double Price {get;set;}
        public DateTime Created_at {get;set;} = DateTime.Now;
        public DateTime Updated_at {get;set;} = DateTime.Now;
        public List<Association> Categories {get;set;}
    }
}