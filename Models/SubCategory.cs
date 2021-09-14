using System;

namespace ShopMart.Models
{
    public class SubCategory
    {
        public long SubCategoryId { get; set; }
        public string SubCategoryName {get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
    }
}