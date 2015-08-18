using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data
{
    public class ProductRepository : IProductRepository
    {
        private const string FilePath = @"DataFiles\Products.txt";

        public List<Product> ListAllProducts()
        {
            if (!File.Exists(FilePath))
                throw new Exception("Product file was not found!");

            var products = new List<Product>();
            var reader = File.ReadAllLines(FilePath);

            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');
                var product = new Product();

                product.ProductType = columns[0];
                product.CostPerSquareFoot = decimal.Parse(columns[1]);
                product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                products.Add(product);
            }

            return products;
        }
    }
}
