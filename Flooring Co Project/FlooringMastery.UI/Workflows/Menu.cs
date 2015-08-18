using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.UI.Workflows;

namespace FlooringMastery.UI
{
    public class Menu
    {
            
        public void Execute()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to SWC Corp:");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("\n(Q)uit");

                Console.Write("\n\nEnter choice: ");
                string input = Console.ReadLine();

                if (input == "Q")
                    break;

                ProcessChoice(input);
            } while (true);
           
        }

        private void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    DisplayOrdersWorkflow displaying = new DisplayOrdersWorkflow();
                    displaying.Execute();
                    break;
                case "2":
                    AddOrderWorkflow adding = new AddOrderWorkflow();
                    adding.Execute();
                    break;
                case "3":
                    EditOrderWorkflow editing = new EditOrderWorkflow();
                    editing.Execute();
                    break;
                    
                case "4":
                     RemoveOrderWorkFlow removing = new RemoveOrderWorkFlow();
                    removing.Execute();
                    break;
            }
        }

    }
}
    
