using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models 
{
    public class Association
    {
        [Key]
        public int AssociationId {get;set;}
        [Required]
        public int CategoryId {get;set;}
        [Required]
        public int ProductId {get;set;}
        public Category Category {get;set;}
        public Product Product {get;set;}
    }
    public class WrapperModel
    {
        public Product NewProduct {get;set;}
        public Category NewCategory {get;set;}
        public List<Product> AllProducts {get;set;}
        public List<Category> AllCategories {get;set;}
    }
}