using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Dapper;

namespace BestBuyBestBractices
{
    class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products(Name, Price, CategoryID) Values (@Name, @Price, @categoryID);",
            new { Name = name, Price = price, CategoryID = categoryID });
        }

        public void UpdateProduct(int productID, string name, double price)
        {
            _connection.Execute("UPDATE Products SET Name = @name, Price = @price  WHERE ProductID = @productID;",
                new { productID = productID, name = name, price = price });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("Delete FROM Sales Where ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("Delete FROM Reviews Where ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM PRODUCTS WHERE ProductID = @productID;",
                new { ProductID = productID });
            
        }

        public IEnumerable<Product> GetProducts()
        {
            return _connection.Query<Product>("Select * FROM Products;");
        }
    }
}
