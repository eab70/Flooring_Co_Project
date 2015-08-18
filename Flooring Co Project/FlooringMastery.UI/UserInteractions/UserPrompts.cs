using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;

namespace FlooringMastery.UI.UserInteractions
{
    public class UserPrompts
    {
        public static int GetOrderNumber()
        {
            var valid = true;
            int orderNumber;
            do
            {
                Console.WriteLine("Please enter an order number: ");
                var userInput = Console.ReadLine();
                valid = int.TryParse(userInput, out orderNumber);

                if (!valid)
                    Console.WriteLine("That was an invalid entry. Please enter a valid number.");
            } while (!valid || orderNumber < 1);

            return orderNumber;
        }

        public static string GetCustomerNameForEdit(string currentCustomerName)
        {
            var input = "";
            do
            {
                Console.WriteLine("Enter a new customer name or leave blank to keep old one: {0}",
                currentCustomerName);
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.Contains(","))
                        Console.WriteLine("Commas are not currently supported. Please try again.");
                    else
                    {
                        return input;
                    }
                }
                
            } while (input.Contains(","));
            return currentCustomerName;
        }


        public static string GetProductTypeForEdit(string currentProductType)
        {
            var input = "";
            var validProduct = false;
            do
            {
                var mgr = ManagerFactory.CreateProductManagr();
                var userProductType = "";
                Console.WriteLine("Enter a new product type or leave blank to keep old one: {0}", currentProductType);
                input = Console.ReadLine();


                if (!string.IsNullOrEmpty(input))
                {
                    if (input.Contains(","))
                    {
                        Console.WriteLine("That was an invalid entry. Press enter to continue.");
                    }
                    else if (input.Length > 2 && !input.Contains(","))
                    {
                        input = input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
                        validProduct = mgr.IsValidProduct(input);
                        if (validProduct)
                            return input;
                        else
                        {
                            Console.WriteLine("That was an invalid entry. Press enter to continue.");
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    validProduct = true;
                }

            } while (!validProduct || input.Contains(","));
            return currentProductType;
        }

        public static decimal GetAreaForEdit(decimal currentArea)
        {
            string input = "";
            bool valid;
            decimal inputNumber;
            do
            {
                Console.WriteLine("Enter a new area or leave blank to keep old value: {0}", currentArea);
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    valid = decimal.TryParse(input, out inputNumber);
                    if (!valid)
                        Console.WriteLine("That was an invalid entry. Please try again.");
                    else
                    {
                        return inputNumber;
                    }
                }
                else
                    valid = true;

            } while (!valid);
            return currentArea;
        }

        public static string GetStateForEdit(string currentState)
        {
            var input = "";
            var Taxmgr = ManagerFactory.CreateTaxManager();
            bool validState = false;
            do
            {
                Console.WriteLine("Enter a new state or leave blank to keep old value: {0}", currentState);
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    validState = Taxmgr.IsValidState(input);
                    if (!validState)
                        Console.WriteLine("That state is not currently supported. Please enter a valid state abbreviation.");
                    else
                    {
                        return input.ToUpper();
                    }
                }
                else
                {
                    validState = true;
                }

            } while (!validState);
            return currentState;
        }

    }
}
