using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TaxCalculation.Domain.Enums;
using TaxCalculation.Domain.Factories;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculation.Domain.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        /// <summary>
        /// A repository implementation that fetches a list of Orders by reading from a text file
        /// </summary>
        /// <param name="textFilePath">A path to the text file</param>
        /// <returns>A generic list of Orders</Orders></returns>
        public List<Order> ReadOrders(string textFilePath)
        {
            try
            {
                //Validate if the text file path is supplied and it exists 
                if (string.IsNullOrEmpty(textFilePath))
                    throw new ArgumentException("File should not be null or empty.");

                if (!File.Exists(textFilePath))
                    throw new FileNotFoundException(String.Format("File {0} not exists",
                                                    textFilePath));

                //Declare a generic list of Orders,
                List<Order> orders = new List<Order>();
                //Read the supplied text file using a StreamReader class,
                using (StreamReader sr = new StreamReader(textFilePath))
                {
                    Order order = null;
                    while (sr.Peek() >= 0)
                    {
                        string str;
                        string[] strArray;
                        str = sr.ReadLine();

                        //If a line starts with character '#' it denotes that it's headings and should be ignored,
                        if (!str.Contains("#"))
                        {
                            strArray = str.Split(',');

                            if (str.Contains("Order"))
                            {
                                order = new Order();
                                order.OrderNumber = strArray[0];
                                orders.Add(order);
                            }
                            else
                            {
                                //An item with basic and import tax supply ItemType.Basic | ItemType.Import
                                if (strArray[3].ToString() == "BasicImport")
                                {
                                    //Use a lineItemFactory to build a LineItem,
                                    order.AddItem(LineItemFactory.GetLineItem(strArray[1],
                                                                                decimal.Parse(strArray[2].ToString()),
                                                                                int.Parse(strArray[0].ToString()),
                                                                                ItemType.Basic | ItemType.Import));
                                }
                                else
                                {
                                    order.AddItem(LineItemFactory.GetLineItem(strArray[1],
                                                                                decimal.Parse(strArray[2].ToString()),
                                                                                int.Parse(strArray[0].ToString()),
                                                                                ConvertToItemTypeEnum(strArray[3].ToString())));
                                }
                            }
                        }
                    }
                }
                return orders;
            }
            catch (Exception ex)
            {
                //Catch any file exceptions.
                if (ex.GetType() == typeof(IndexOutOfRangeException) || ex.GetType() == typeof(ArgumentException) || ex.GetType() == typeof(FormatException))
                    throw new InvalidDataException(String.Format("File {0} has an invalid format, get further details on the inner exception.",
                                                    textFilePath),ex);
            }
            return new List<Order>();
        }

        private ItemType ConvertToItemTypeEnum(string value)
        {
            var type = Enum.Parse(typeof(ItemType), value);
            return (ItemType)type;
        }
    }
}
