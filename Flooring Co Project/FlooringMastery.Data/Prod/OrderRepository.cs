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
    public class OrderRepository : IOrderRepository
    {
        public string CreateFilePathFor(DateTime orderDate)
        {
            return string.Format(@"DataFiles\Orders\Orders_{0}.txt", orderDate.ToString("MMddyyyy"));
        }

        public List<Order> LoadOrders(DateTime orderDate)
        {
            string path = CreateFilePathFor(orderDate);
            List<Order> theOrders = new List<Order>();

            if (File.Exists(path))
            {
                var reader = File.ReadAllLines(path);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var order = new Order();

                    order.OrderNumber = int.Parse(columns[0]);
                    order.CustomerName = columns[1];
                    order.State = columns[2];
                    order.TaxRate = decimal.Parse(columns[3]);
                    order.ProductType = columns[4];
                    order.Area = decimal.Parse(columns[5]);
                    order.CostPerSquareFoot = decimal.Parse(columns[6]);
                    order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    order.MaterialCost = decimal.Parse(columns[8]);
                    order.LaborCost = decimal.Parse(columns[9]);
                    order.Tax = decimal.Parse(columns[10]);
                    order.Total = decimal.Parse(columns[11]);

                    theOrders.Add(order);
                }
            }
            else
            {
                return null;
            }
            return theOrders;
        }

        public void Add(Order orderToAdd, DateTime orderDate)
        {
            var orders = LoadOrders(orderDate);
            if (orders == null)
            {
                orders = new List<Order>();
            }

            orders.Add(orderToAdd);

            OverwriteFile(orders, orderDate);
        }
        public void Delete(Order orderToDelete, DateTime orderDate)
        {
            var orders = LoadOrders(orderDate);

            orders.Remove(orders.First(o => o.OrderNumber == orderToDelete.OrderNumber));

            OverwriteFile(orders, orderDate);
        }

        
        public void OverwriteFile(List<Order> orders, DateTime orderDate)
        {

            var fullPath = string.Format(@"DataFiles\Orders\Orders_{0}.txt", orderDate.ToString("MMddyyyy"));

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            using (var writer = File.CreateText(fullPath))
            {
                writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                foreach (var order in orders)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        order.OrderNumber,
                        order.CustomerName,
                        order.State,
                        order.TaxRate,
                        order.ProductType,
                        order.Area,
                        order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot,
                        order.MaterialCost,
                        order.LaborCost,
                        order.Tax,
                        order.Total
                        );
                }
            }
        }


    }
}
