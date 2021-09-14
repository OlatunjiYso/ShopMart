using System;
using System.Collections.Generic;

namespace ShopMart.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string  Title { get; set; }
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }
        public double Price {get; set; }
        public double DiscountPercent {get; set; }
        public long DepartmentId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}