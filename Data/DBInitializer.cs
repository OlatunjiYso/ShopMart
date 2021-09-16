using System;
using System.Linq;
using ShopMart.Models;

namespace ShopMart.Data
{
    public static class DBInitializer
    {
       public static void Initialize(ShopMartContext shopMartContext)
        {
            shopMartContext.Database.EnsureCreated();

            // Look for any User in the DB.
            if(shopMartContext.Customers.Any())
            {
                return;
            }

            // Seeding Customers
            var customers = new Customer[]
            {
                new Customer{  Email="john@doe.com", FirstName="John", LastName="Doe", Password="P@$$word"},
                new Customer{  Email="jane@doe.com", FirstName="Jane", LastName="Doe", Password="P@$$word"},
                new Customer{  Email="janny@doe.com", FirstName="Janny", LastName="Doe", Password="P@$$word"}
            };
            foreach(Customer customer in customers)
            {
                shopMartContext.Customers.Add(customer);
            }
            shopMartContext.SaveChanges();

            // Seeding departments
            var departments = new Department[]
            {
                new Department{  DepartmentName="Grocries", Description="A collection of Grocries products"},
                new Department{  DepartmentName="Bakery", Description="A collection of Bakery products"},
                new Department{  DepartmentName="Makeup", Description="A collection of Makeup products"},
                new Department{  DepartmentName="Furniture", Description="Our Furniture products"}
            };
            foreach(Department department in departments)
            {
                shopMartContext.Departments.Add(department);
            };
            shopMartContext.SaveChanges();

            // Seeding Categories
            var categories = new Category[]
            {
                new Category{ CategoryName="Fruit & Vegetables", Description="Fresh fruits and soumptous green vegetables.", DepartmentId=1},
                new Category{ CategoryName="Fish & Meat", Description="Fresh fishes and meat", DepartmentId=1},
                new Category{  CategoryName="Snacks", Description="Suomptuous Snacks.", DepartmentId=1},

                new Category{ CategoryName="All Bakery", Description="Bakery products", DepartmentId=2},

                new Category{ CategoryName="Face", Description="Face Items", DepartmentId=3},
                new Category{ CategoryName="Deodorants", Description="Smell nice always.", DepartmentId=3},
                new Category{ CategoryName="Accesories", Description="Accesories.", DepartmentId=3},

                new Category{ CategoryName="Bed", Description="Cool beds", DepartmentId=4},
                new Category{ CategoryName="Chair", Description="Comfy Chairs.", DepartmentId=4},
                new Category{ CategoryName="Tables", Description="Clean Tables.", DepartmentId=4}
            };
            foreach(Category category in categories)
            {
                shopMartContext.Categories.Add(category);
            }
            shopMartContext.SaveChanges();
        }
    }
}
