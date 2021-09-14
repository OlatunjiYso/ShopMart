using System;
using System.Collections.Generic;

namespace ShopMart.Models
{
    public class Category
    {
        public long CategoryId { get; set; }
        public long DepartmentId { get; set; }
        public string  CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<SubCategory> Subcategories { get; set; }

    }
}