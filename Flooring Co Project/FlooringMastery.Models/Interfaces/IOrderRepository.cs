using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        string CreateFilePathFor(DateTime orderDate);
        List<Order> LoadOrders(DateTime orderDate);
        void Add(Order orderToAdd, DateTime orderDate);
        void Delete(Order orderToDelete, DateTime orderDate);
        void OverwriteFile(List<Order> orders, DateTime orderDate);
    }
}
