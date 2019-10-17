using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get;set;}
        [Required]
        [MinLength(2)]
        public string Name {get;set;}
        public DateTime Created_at {get;set;} = DateTime.Now;
        public DateTime Updated_at {get;set;} = DateTime.Now;
        public List<Association> Products {get;set;}
    }
}