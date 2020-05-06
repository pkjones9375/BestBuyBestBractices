using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.IO;
using MySql.Data.MySqlClient;

namespace BestBuyBestBractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperProductRepository(conn);


            Console.WriteLine("Would you like to add a new product? Yes/No");

            var response = Console.ReadLine().ToLower();

            while (response == "yes") 
            {
                Console.WriteLine("Type a new Product Name");

                var newProduct = Console.ReadLine();

                Console.WriteLine("How much will it cost?");

                var newPrice = double.Parse(Console.ReadLine());

                Console.WriteLine("What is it's category ID?");

                var newCatID = int.Parse(Console.ReadLine());

                repo.CreateProduct(newProduct, newPrice, newCatID);

                var allProducts = repo.GetProducts();

                foreach (var item in allProducts)
                {
                    Console.WriteLine($"{item.Name} - ${item.Price}");
                }
                Console.WriteLine("Would you like to add a new product? Yes/No");

                response = Console.ReadLine().ToLower();
            } 


            Console.WriteLine("Would you like to update a product? Yes/No");

            var response2 = Console.ReadLine().ToLower() ;

            while (response2 == "yes")
            {
                Console.WriteLine("What is the productID of the product you'd like to update?");
                var productID = int.Parse(Console.ReadLine());

                Console.WriteLine("What is its new name?");

                var productName = Console.ReadLine();

                Console.WriteLine("How much will it cost?");

                var price = double.Parse(Console.ReadLine());

                repo.UpdateProduct(productID, productName, price);

                var allProducts = repo.GetProducts();

                foreach (var item in allProducts)
                {
                    Console.WriteLine($"{item.Name} - ${item.Price}");
                }
                Console.WriteLine("Would you like to update a product? Yes/No");

                response2 = Console.ReadLine().ToLower();

            }

            Console.WriteLine("Would you like to Delete a product? Yes/No");
            var response3 = Console.ReadLine().ToLower();

            while(response3 == "yes")
            {
                Console.WriteLine("What is the productID of the product you would like to delete?");
                var productID = int.Parse(Console.ReadLine());

                repo.DeleteProduct(productID);

                var allProds = repo.GetProducts();
                foreach (var item in allProds)
                {
                    Console.WriteLine($"{item.Name}, ${item.Price}");
                }
                Console.WriteLine("Would you like to Delete a product? Yes/No");
                response3 = Console.ReadLine().ToLower();

                


            }

            
        }
    }
}
