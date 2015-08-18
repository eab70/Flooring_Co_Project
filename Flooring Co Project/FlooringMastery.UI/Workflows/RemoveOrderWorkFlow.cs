using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkFlow
    {
        EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
        OrderOperations orderOps = new OrderOperations();
        DisplayOrdersWorkflow displayOrdersWorkflow = new DisplayOrdersWorkflow();
        Order orderToRemove = new Order();  
        Response<Order> response = new Response<Order>(); 
 

        public void Execute()
        {
           
            DateTime removeDate = displayOrdersWorkflow.GetOrderDateFromUser();
            int orderNumber = editOrderWorkflow.GetOrderNumber();
            orderToRemove.OrderNumber = orderNumber;
            orderOps.GetOrders(removeDate);
            response = orderOps.GetOrderToEdit(removeDate, orderNumber);
            
            RemoveOrderToConfirm(response.Data, removeDate);

            
            if (response.Success)
            {
                Console.WriteLine("Order {0} has been deleted.", orderToRemove.OrderNumber);
            }
            else
            {
                Console.WriteLine(response.Message);
            }

        }

        public void RemoveOrderToConfirm(Order orderToRemove, DateTime removeDate)
        {

            var confirmOrder = "";
            bool validEntry = true;

            do
            {
                Console.WriteLine("Would you like to delete this order? Please enter Yes or No.");
                Console.WriteLine("========| Order {0} | ========",orderToRemove.OrderNumber);
                Console.WriteLine("Product Type: {0}", orderToRemove.ProductType);
                Console.WriteLine("Amount Ordered: {0}sqft", orderToRemove.Area);
                Console.WriteLine("Material Cost: {0:c}", orderToRemove.MaterialCost);
                Console.WriteLine("Labor Cost: {0:c}", orderToRemove.LaborCost);
                Console.WriteLine("Tax: {0:c}", orderToRemove.Tax);
                Console.WriteLine("Total: {0:c}", orderToRemove.Total);
                confirmOrder = Console.ReadLine();
                confirmOrder = confirmOrder.Substring(0, 1).ToUpper();
                if (confirmOrder != "Y" && confirmOrder != "N")
                {
                    Console.WriteLine("That is an invalid entry. Please input yes or no.");
                    validEntry = false;
                }
                if (string.IsNullOrEmpty(confirmOrder))
                {
                    Console.WriteLine("You must make a selection. Please input Yes or No.");
                    validEntry = false;
                }
                else
                {
                    validEntry = true;
                }
            } while (!validEntry);

            if (confirmOrder == "Y")
            {
                orderOps.RemoveOrder(orderToRemove, removeDate);
                if (response.Success)
                {
                   Console.WriteLine("Order #: {0} has been successfully removed.", orderToRemove.OrderNumber);
                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }

        }
 
    }
}