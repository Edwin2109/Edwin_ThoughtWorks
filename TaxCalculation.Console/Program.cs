using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain;
using TaxCalculation.Domain.Enums;
using TaxCalculation.Domain.Factories;
using TaxCalculation.Domain.Interfaces;
using TaxCalculation.Domain.Repositories;


namespace TaxCalculation.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrdersRepository repository = new OrdersRepository();
            //
            var orders = repository.ReadOrders("SalesData.txt");

            foreach (var order in orders)
            {
                System.Console.WriteLine(order.OrderNumber);
                foreach(var lineItem in order.LineItems)
                {
                    System.Console.WriteLine(lineItem.GetQuantity() + " " + lineItem.Name + " " + decimal.Round(lineItem.GetTotal(),2));
                }
                Console.WriteLine("Sales Taxes: " + order.GetOrderLineTax());
                Console.WriteLine("Total: " + order.GetOrderTotal());
                Console.WriteLine(Environment.NewLine);
            }
            Console.ReadLine();
        }

        
    }
}
