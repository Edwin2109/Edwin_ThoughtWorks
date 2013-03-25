using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using TaxCalculation.Domain;
using TaxCalculation.Domain.Enums;
using TaxCalculation.Domain.Factories;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculatorTests
{
    [TestFixture]
    public class OrdersRepositoryTest
    {
        private IOrdersRepository mock;
        private ITax stub;
        private OrdersRepositoryTestClass ordersRepositoryTestClass;
        private List<Order> listOrders = null;

        [SetUp]
        public void Setup()
        {
            mock = MockRepository.GenerateMock<IOrdersRepository>();
            stub = MockRepository.GenerateStub<ITax>();
            ordersRepositoryTestClass = new OrdersRepositoryTestClass();
            listOrders = new List<Order>();

            Order order = new Order();
            order.AddItem(LineItemFactory.GetLineItem("book", 12.49M,1,ItemType.Exempt));
            order.AddItem(LineItemFactory.GetLineItem("CD", 14.99M, 1,ItemType.Basic));
            order.AddItem(LineItemFactory.GetLineItem("chocolate bar", 0.85M, 1, ItemType.Exempt));
            listOrders.Add(order);

            order = new Order();
            order.AddItem(LineItemFactory.GetLineItem("imported chocolates", 10.0M, 1, ItemType.Import));
            order.AddItem(LineItemFactory.GetLineItem("imported perfume", 47.5M, 1, ItemType.Basic | ItemType.Import));
            listOrders.Add(order);

            order = new Order();
            order.AddItem(LineItemFactory.GetLineItem("imported perfume", 27.99M, 1, ItemType.Basic | ItemType.Import));
            order.AddItem(LineItemFactory.GetLineItem("perfume", 18.99M, 1, ItemType.Basic));
            order.AddItem(LineItemFactory.GetLineItem("pills", 19.75M, 1, ItemType.Exempt));
            order.AddItem(LineItemFactory.GetLineItem("imported chocolates", 1.25M,1, ItemType.Import));
            listOrders.Add(order);
        }

        //[TestCase("", Description = "Empty File Test",
        //ExpectedException = typeof(ArgumentException))]

        //[TestCase(null, Description = "Null Test",
        //ExpectedException = typeof(ArgumentException))]

        //[TestCase("SalesData.txt", Description = "Invalid File Test",
        //ExpectedException = typeof(FileNotFoundException))]

        [TestCase("SalesData.txt",Description = "Positive Test")]
        public void GetOrdersVerifiedMethodCallTestCase(string filePath)
        {
            mock.Expect(s => s.ReadOrders(filePath)).Return(listOrders);
            ordersRepositoryTestClass.ActOnIOrdersRepository(mock, filePath).ToList();
            //Verifying that the method (ReadOrders) was was called. 
            mock.VerifyAllExpectations();
        }

        [TestCase("SalesData.txt", Description = "GetOrdersNotEmpty Test")]
        public void GetOrdersNotEmptyTestCases(string filePath)
        {
            List<Order> orders = new List<Order>();
            mock.Expect(s => s.ReadOrders(filePath)).Return(listOrders);

            var actualOrders = ordersRepositoryTestClass.ActOnIOrdersRepository(mock,filePath).ToList();

            CollectionAssert.IsNotEmpty(actualOrders);
        }
    }

    public class OrdersRepositoryTestClass
    {
        public List<Order> ActOnIOrdersRepository(IOrdersRepository ordersRepository, string filePath)
        {
            return ordersRepository.ReadOrders(filePath);
        }
    }

}
