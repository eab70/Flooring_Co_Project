//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FlooringMastery.Data;
//using NUnit.Framework;

//namespace FlooringMastery.Tests
//{
//    class ProductRepositoryTest
//    {
//        [TestFixture]
//        public class ProductRepositoryTests
//        {
//            [Test]
//            public void CanLoadOrder()
//            {
//                var repo = new OrderRepository();
//                DateTime orderDate = new DateTime(2015, 5, 3);

//                var ordersList = repo.LoadOrders(orderDate);

//                Assert.AreEqual(1, ordersList[0].OrderNumber);
//                Assert.AreEqual("Gucci", ordersList[0].CustomerName);

//            }
//        }
//    }
//}
