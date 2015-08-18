using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class AddOrderWorkflow
    {
        private Order _orderToAdd = new Order();
        

        public void Execute()
        {
            Console.Clear();
            var mgr = ManagerFactory.CreateProductManagr();
            mgr.ListAllProducts();

            var orderOps = new OrderOperations();

            DisplayAllProducts();
            GetCustomerName();
            GetCustomerState();
            GetProductType();
            GetArea();
            Order orderToAdd = orderOps.NewOrderCalculations(_orderToAdd);
            PrintOrderDetails(orderToAdd);
            
            Console.ReadLine();
            OrderToConfirm(orderToAdd);

            Console.ReadLine();
        }

        private void DisplayAllProducts()
        {
            var mgr = ManagerFactory.CreateProductManagr();
            List<Product> ProdList = mgr.ListAllProducts();
            Console.WriteLine("Current SWC Corp Products:");
            Console.WriteLine("=========================");
            foreach (var element in ProdList)
            {
                Console.WriteLine("Product Type: {0}",element.ProductType);
                Console.WriteLine("Cost Per Square Foot: $"+element.CostPerSquareFoot);
                
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadKey();
        }

        public void GetCustomerName()
        {
            string userCustomerName = "";
            do
            {
              Console.Write("Enter customer name: ");
              userCustomerName = Console.ReadLine();
                if (string.IsNullOrEmpty(userCustomerName))
                {
                    Console.Write("That was not valid input. Please enter again.");
                }
                if (userCustomerName.Contains(","))
                {
                    Console.WriteLine("I'm sorry, we cannot accept commas at this time. Please try again.");
                }
            } while (string.IsNullOrEmpty(userCustomerName));

            _orderToAdd.CustomerName = userCustomerName;
        }

        public void GetCustomerState()
        {
            var mgr = ManagerFactory.CreateTaxManager();
            var validState = false;
            //var userCustomerState = "";
            do
            {
              Console.Write("Please enter the two-character customer state abbreviation: ");
              var userCustomerState = Console.ReadLine();
              _orderToAdd.State = userCustomerState.ToUpper();
              validState = mgr.IsValidState(_orderToAdd.State);
              
                if (validState == false)
                {
                    Console.WriteLine("I'm sorry, that state is not currently supported.");
                }
            } while (validState == false);
            
        }

        public void GetProductType()
        {
            var mgr = ManagerFactory.CreateProductManagr();
            var validProduct = false;
            var userProductType = "";
            do
            {
               Console.Write("Please enter a product type: ");
               userProductType = Console.ReadLine();//.Substring(0, 1).ToUpper().Substring(1).ToLower();
                _orderToAdd.ProductType = userProductType.Substring(0,1).ToUpper()+userProductType.Substring(1).ToLower();
               validProduct = mgr.IsValidProduct(_orderToAdd.ProductType);
                if (validProduct == false)
                {
                    Console.WriteLine("I'm sorry, that is not a valid product. Please select again.");
                }
            } while (validProduct == false);
            
        }
        
        public void GetArea()
        {
            var stringArea = "";
            var valid = true;
            decimal area;
            do
            {
                Console.Write("Please enter the square footage of materials needed: ");
                stringArea = Console.ReadLine();
                valid = decimal.TryParse(stringArea, out area);
                if (area < 1)
                {
                    valid = false;
                    Console.WriteLine("That was an invalid entery. Please try again. ");
                }
                if(!valid)
                    Console.WriteLine("Please enter a valid number.");
            } while (!valid);
            _orderToAdd.Area = area;
            
        }

        public void OrderToConfirm(Order orderToAdd)
        {
            var orderOps = new OrderOperations();
            DateTime orderDateToday = DateTime.Today;
            var confirmOrder = "";
            bool validEntry = true; 
            
            do
            {
                Console.WriteLine("Would you like to place this order? Please enter Yes or No.");
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
                orderOps.NewOrderNumber(orderToAdd, orderDateToday);
                var response = orderOps.AddNewOrder(orderToAdd, orderDateToday);
                
                if (response.Success)
                {
                    Console.WriteLine("========| Order Confirmation | ========");
                    Console.WriteLine("Order number: {0}",response.Data.OrderNumber);
                    Console.WriteLine("Product Type: {0}", response.Data.ProductType);
                    Console.WriteLine("Amount Ordered: {0}sqft", response.Data.Area);
                    Console.WriteLine("Material Cost: {0:c}", response.Data.MaterialCost);
                    Console.WriteLine("Labor Cost: {0:c}", response.Data.LaborCost);
                    Console.WriteLine("Tax: {0:c}", response.Data.Tax);
                    Console.WriteLine("Total: {0:c}", response.Data.Total);
                }
            }
            else
            {
                var response = orderOps.AddNewOrder(orderToAdd, orderDateToday);
                Console.WriteLine(response.Message);
            }
            
        }

        private void PrintOrderDetails(Order orderToAdd)
        {
            Console.Clear();
            Console.WriteLine("Order Summary: ");
            Console.WriteLine("Product Type: {0}", orderToAdd.ProductType);
            Console.WriteLine("Area: {0}",orderToAdd.Area);
            Console.WriteLine("Cost of Materials: {0:c}",orderToAdd.MaterialCost);
            Console.WriteLine("Cost of Labor: {0:c}", orderToAdd.LaborCost);
            Console.WriteLine("Tax: {0:c}",orderToAdd.Tax);
            Console.WriteLine("-------------------");
            Console.WriteLine("Total: {0:c}",orderToAdd.Total);
        }   
    }
}

