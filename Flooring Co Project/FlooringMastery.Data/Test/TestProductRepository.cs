using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Test
{
    public class TestProductRepository : IProductRepository
    {
        public List<Product> ListAllProducts()
        {
            return new List<Product>()
            {
                new Product() { ProductType = "Marble", CostPerSquareFoot = 10.00M, LaborCostPerSquareFoot = 20.00M},
                new Product() { ProductType = "Wood", CostPerSquareFoot = 6.00M, LaborCostPerSquareFoot = 10.00M},
                new Product() { ProductType = "Carpet", CostPerSquareFoot = 5.00M, LaborCostPerSquareFoot = 15.00M}
            };
        }
    }
}

    