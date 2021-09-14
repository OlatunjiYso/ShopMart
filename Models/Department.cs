using System;
using System.Collections.Generic;

namespace ShopMart.Models
{
    public class Department
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}