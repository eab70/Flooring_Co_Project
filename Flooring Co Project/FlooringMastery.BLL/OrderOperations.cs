using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
    public class OrderOperations
    {

        public Response<List<Order>> GetOrders(DateTime orderDate)
        {
            var repo = new OrderRepository();
            var response = new Response<List<Order>>();

            try
            {
                var orderList = repo.LoadOrders(orderDate);

                if (orderList == null)
                {
                    response.Success = false;
                    response.Message = "Order not found!";
                }
                else
                {
                    response.Success = true;
                    response.Data = orderList;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        
        public Response<Order> AddNewOrder(Order orderToAdd, DateTime orderDate)
        {
            var repo = new OrderRepository();
            var response = new Response<Order>();

            try
            {
                repo.Add(orderToAdd, orderDate);
                response.Success = true;
                response.Data = orderToAdd;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "The order could not be added. Please verify that inputs are correct.";
            }
            return response;
        }

        public void NewOrderNumber(Order orderToAdd, DateTime orderDate)
        {
            var repo = new OrderRepository();
            var allOrders = repo.LoadOrders(orderDate);

            if (allOrders != null)
            {
                var nextOrderNumber = allOrders.Max(o => o.OrderNumber);
                nextOrderNumber++;
                orderToAdd.OrderNumber = nextOrderNumber;
            }
            else
            {
                orderToAdd.OrderNumber = 1;
            }
            
        }

        public Order NewOrderCalculations(Order orderToAdd)
        {
            var taxMgr = ManagerFactory.CreateTaxManager();
            orderToAdd.TaxRate = taxMgr.GetRate(orderToAdd.State);

            var productMgr = ManagerFactory.CreateProductManagr();
            orderToAdd.CostPerSquareFoot = productMgr.CostPerSquareFoot(orderToAdd.ProductType);
            orderToAdd.LaborCostPerSquareFoot = productMgr.LaborCostPerSquareFoot(orderToAdd.ProductType);

            orderToAdd.MaterialCost = (orderToAdd.CostPerSquareFoot*orderToAdd.Area);
            orderToAdd.LaborCost = (orderToAdd.LaborCostPerSquareFoot*orderToAdd.Area);

            var subTotal = orderToAdd.MaterialCost + orderToAdd.LaborCost;
            orderToAdd.Tax = (orderToAdd.TaxRate*subTotal);
            orderToAdd.Total = (subTotal + orderToAdd.Tax);


            return orderToAdd;
        }

        public Order EditOrderCalculations(Order editingOrder)
        {
            var taxMgr = ManagerFactory.CreateTaxManager();
            editingOrder.TaxRate = taxMgr.GetRate(editingOrder.State);

            editingOrder.MaterialCost = editingOrder.CostPerSquareFoot*editingOrder.Area;
            editingOrder.LaborCost = editingOrder.LaborCostPerSquareFoot*editingOrder.Area;

            var subTotal = editingOrder.MaterialCost + editingOrder.LaborCost;
            editingOrder.Tax = editingOrder.TaxRate*subTotal;
            editingOrder.Total = subTotal + editingOrder.Tax;

            return editingOrder;
        }

       

        public Response<Order> RemoveOrder(Order orderNumber, DateTime orderDate)
        {
            var repo = new OrderRepository();
            var response = new Response<Order>();

            try
            {
                repo.Delete(orderNumber, orderDate);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Order could not be deleted; please enter information again.";
            }

            return response;
        }

        public Response<Order> RemovePreEditedOrder(Order orderBeingRemoved, DateTime editDate)
        {
            var repo = new OrderRepository();
            var response = new Response<Order>();

            try
            {
                repo.Delete(orderBeingRemoved, editDate);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Order could not be deleted; please enter information again.";
            }

            return response;
        }

        public Response<Order> GetOrderToEdit(DateTime editDate, int orderNumber)
        {
            var repo = new OrderRepository();
            var response = new Response<Order>();

            try
            {
                var orderList = repo.LoadOrders(editDate);
                if (orderList == null)
                {
                    response.Success = false;
                    response.Message = "Sorry 'bout you";
                }
                else
                    {
                        var order = orderList.FirstOrDefault(o => o.OrderNumber == orderNumber);

                        if (order == null)
                        {
                            response.Success = false;
                            response.Message = "Sorry, that is an invalid entry.";
                        }
                        else
                        {
                            response.Success = true;
                            response.Data = order;
                        }
                    }
                
            }
            catch
                (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        }

    }
