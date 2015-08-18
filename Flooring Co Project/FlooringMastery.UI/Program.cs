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
    public class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.Execute();

            
            var addOrderWorkFlow = new AddOrderWorkflow();
            addOrderWorkFlow.Execute();

            
        }
    }
}
