using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Test;

namespace FlooringMastery.BLL
{
    public class ManagerFactory
    {
        public static TaxManager CreateTaxManager()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            if(mode == "Test")
                return new TaxManager(new TestStateTaxInformationRespository());
            else
                throw new Exception("The production repository does not exist yet because David hasn't coughed up the file.");
        }

        public static ProductManager CreateProductManagr()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            if(mode == "Test")
                return new ProductManager(new TestProductRepository());
            else
            {
                throw new Exception("The production repository does not exist yet!");
            }
        }

        //public static OrderOperations CreateOrderOperations()
        //{
        //    string mode = ConfigurationManager.AppSettings["Mode"];
        //    if(mode == "Test")
        //        return new OrderOperations(new TestOrderRepository());
        //    else
        //    {
        //        return new OrderOperations();
        //    }
        //}
    }
}
