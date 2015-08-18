using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using NUnit.Framework;
using FlooringMastery.Models;

namespace FlooringMastery.Tests
{
    internal class OrderRepositoryTest
    {

        [TestFixture]
        public class OrderRepositoryTests
        {
            [Test]
            public void CanLoadOrder()
            {
                var repo = new OrderRepository();
                DateTime orderDate = new DateTime(2015,5,3);

                var ordersList = repo.LoadOrders(orderDate);


                Assert.AreEqual(1, ordersList[0].OrderNumber);
                Assert.AreEqual("Gucci", ordersList[0].CustomerName);

            }
            //The test code is the issue, not the production code. Redo test code later. 
            [Test]
            public void CanAddOrder()
            {
                var repo = new OrderRepository();
                DateTime orderDate = new DateTime(2015,05,03);

                Order orderToAdd = new Order()
                {
                    OrderNumber = 3,
                    CustomerName = "Wise",
                    State = "OH",
                    TaxRate = 6.25M,
                    ProductType = "Wood",
                    Area = 100.00M,
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M,
                    MaterialCost = 515.00M,
                    LaborCost = 475.00M,
                    Tax = 61.88M,
                    Total = 1051.88M
                };

                //public void OverwriteFile(List<Order> orders, DateTime orderDate)

                var ordersList = repo.LoadOrders(orderDate);

                repo.Add(orderToAdd, orderDate);

                //repo.OverwriteFile(ordersList, orderDate);

                Assert.AreEqual(3, ordersList[ordersList.Count-1].OrderNumber);
                Assert.AreEqual("Wise", ordersList[ordersList.Count - 1].CustomerName);

            }
        }
    }
}

