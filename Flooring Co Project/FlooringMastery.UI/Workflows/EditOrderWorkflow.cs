using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.UI.UserInteractions;

namespace FlooringMastery.UI.Workflows
{
    public class EditOrderWorkflow
    {
        DisplayOrdersWorkflow displayWorkflow = new DisplayOrdersWorkflow();
        DateTime editDate = new DateTime();
        OrderOperations orderOps = new OrderOperations();
        int orderNumber;
        
        

        public void Execute()
        {
            var response = orderOps.GetOrders(editDate);
               
            do
            {
                editDate = displayWorkflow.GetOrderDateFromUser();
                response = orderOps.GetOrders(editDate);
                if (response.Success == false)
                {
                    Console.WriteLine("That was an invalid entry. Please enter a valid date. Press enter to continue.");
                    Console.ReadLine();
                }
            } while (response.Success == false);

            var orderNumber = UserPrompts.GetOrderNumber();

            //PrintOrderToEdit(editDate, orderNumber);
            
            
            
            var editingOrder = orderOps.GetOrderToEdit(editDate, orderNumber);
            Order OrderBeingEdited = CreateEditOrder(editingOrder);
            orderOps.NewOrderCalculations(OrderBeingEdited);
            OrderBeingEdited.OrderNumber = orderNumber;
            orderOps.AddNewOrder(OrderBeingEdited, editDate);
            orderOps.RemovePreEditedOrder(OrderBeingEdited, editDate);
            
            Console.WriteLine("Here is your updated order information: ");
            Console.WriteLine("========| Updated Order Confirmation | ========");
            Console.WriteLine("Order number: {0}", OrderBeingEdited.OrderNumber);
            Console.WriteLine("Customer Name: {0}", OrderBeingEdited.CustomerName);
            Console.WriteLine("Product Type: {0}", OrderBeingEdited.ProductType);
            Console.WriteLine("Product Cost Per Square Foot: {0:c}", OrderBeingEdited.CostPerSquareFoot);
            Console.WriteLine("Labor Cost Per Square Foot: {0:c}", OrderBeingEdited.LaborCostPerSquareFoot);
            Console.WriteLine("Amount Ordered: {0}sqft", OrderBeingEdited.Area);
            Console.WriteLine("Total Material Cost: {0:c}", OrderBeingEdited.MaterialCost);
            Console.WriteLine("Total Labor Cost: {0:c}", OrderBeingEdited.LaborCost);
            Console.WriteLine("Tax: {0:c}", OrderBeingEdited.Tax);
            Console.WriteLine("Total: {0:c}", OrderBeingEdited.Total);
                        
            Console.ReadLine();

        }

        

        public Order CreateEditOrder(Response<Order> editingOrder)
        {

            editingOrder.Data.OrderNumber = orderNumber;
            editingOrder.Data.CustomerName = UserPrompts.GetCustomerNameForEdit(editingOrder.Data.CustomerName);
            editingOrder.Data.ProductType = UserPrompts.GetProductTypeForEdit(editingOrder.Data.ProductType);
            editingOrder.Data.Area = UserPrompts.GetAreaForEdit(editingOrder.Data.Area);
            editingOrder.Data.State = UserPrompts.GetStateForEdit(editingOrder.Data.State);
       
            return editingOrder.Data;
        }

    }
}
