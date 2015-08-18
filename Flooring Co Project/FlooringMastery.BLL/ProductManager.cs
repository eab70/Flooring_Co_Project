using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
    public class ProductManager
    {
         private IProductRepository _myProductRepository;

        public ProductManager(IProductRepository aProductRepository)
        {
            _myProductRepository = aProductRepository;
        }

        public List<Product> ListAllProducts()
        {
            return _myProductRepository.ListAllProducts();
        }

        public decimal CostPerSquareFoot (string productType)
        {
            var allProducts = _myProductRepository.ListAllProducts();

            var product = allProducts.First(p => p.ProductType == productType);

            return product.CostPerSquareFoot;
        }

        public decimal LaborCostPerSquareFoot (string productType)
        {
            var allProducts = _myProductRepository.ListAllProducts();

            var product = allProducts.First(p => p.ProductType == productType);

            return product.LaborCostPerSquareFoot;
        }

        public bool IsValidProduct(string productType)
        {
            var allProducts = _myProductRepository.ListAllProducts();

            return allProducts.Any(p => p.ProductType == productType);
        }
    }
}

