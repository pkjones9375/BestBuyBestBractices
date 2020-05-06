using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestBractices
{
    interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void CreateProduct(string name, double price, int categoryID);
        void UpdateProduct(int ProductID, string name, double price);
        void DeleteProduct(int ProductID);

    }
}
