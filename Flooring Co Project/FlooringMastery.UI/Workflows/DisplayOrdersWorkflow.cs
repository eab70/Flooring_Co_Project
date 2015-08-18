using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class DisplayOrdersWorkflow
    { 

        public void Execute()
        {
            DateTime oDate = GetOrderDateFromUser();
            DisplayOrderInformation(oDate);

        }
        public DateTime GetOrderDateFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("Enter a date in mm/dd/yyyy format: ");
                string input = Console.ReadLine();

                DateTime orderDate;

                if (DateTime.TryParse(input, out orderDate))
                    return orderDate;

                Console.WriteLine("That was not a valid date. Press any key to continue...");

            } while (true);
        }


        private void DisplayOrderInformation(DateTime orderDate)
        {
            var ops = new OrderOperations();
            var response = ops.GetOrders(orderDate);
            Console.Clear();

            if (response.Success)
            {
                PrintOrderDetails(response.Data, orderDate);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("A problem occurred...");
                Console.WriteLine(response.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void PrintOrderDetails(List<Order> list,DateTime orderDate)  //CODE FROM EDDIE AND SEAN
        {
            Console.Clear();
            Console.WriteLine("==========Orders for {0}===========", orderDate.ToString("d"));
            foreach (var order in list)
            {
                Console.WriteLine("Order Number: {0}\t Customer: {1} \tState: {2}",
                    order.OrderNumber, order.CustomerName, order.State);
                Console.WriteLine("\tProduct: {0}, Area: {1}, Total: {2:C}",
                    order.ProductType, order.Area, order.Total);
                Console.WriteLine("--------------------");

            }
        }
    }
    }

