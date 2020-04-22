using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Controllers;
using Store.WebUI.Models;

namespace Store.UnitTests
{
  [TestClass]
  public class CartTests
  {
    [TestMethod]
    public void Can_Add_New_Lines()
    {
      var product1 = new Product {Id = 1, Name = "Product1"};
      var product2 = new Product {Id = 2, Name = "Product2"};
      var cart = new Cart();
      cart.AddItem(product1, 1);
      cart.AddItem(product2, 1);

      var results = cart.Lines.ToList();

      Assert.AreEqual(results.Count, 2);
      Assert.AreEqual(results[0].Product, product1);
      Assert.AreEqual(results[1].Product, product2);
    }

    [TestMethod]
    public void Can_Add_Quantity_For_Existing_Lines()
    {
      var product1 = new Product {Id = 1, Name = "Product1"};
      var product2 = new Product {Id = 2, Name = "Product2"};
      var cart = new Cart();
      cart.AddItem(product1, 1);
      cart.AddItem(product2, 1);
      cart.AddItem(product1, 5);

      var results = cart.Lines.OrderBy(c => c.Product.Id).ToList();

      Assert.AreEqual(results.Count, 2);
      Assert.AreEqual(results[0].Quantity, 6);
      Assert.AreEqual(results[1].Quantity, 1);
    }

    [TestMethod]
    public void Can_Remove_Line()
    {
      var product1 = new Product {Id = 1, Name = "Product1"};
      var product2 = new Product {Id = 2, Name = "Product2"};
      var product3 = new Product {Id = 3, Name = "Product3"};
      var cart = new Cart();
      cart.AddItem(product1, 1);
      cart.AddItem(product2, 4);
      cart.AddItem(product3, 2);
      cart.AddItem(product2, 1);
      cart.RemoveLine(product2);

      Assert.AreEqual(cart.Lines.Count(c => c.Product == product2), 0);
      Assert.AreEqual(cart.Lines.Count(), 2);
    }

    [TestMethod]
    public void Calculate_Cart_Total()
    {
      var product1 = new Product {Id = 1, Name = "Product1", Price = 100};
      var product2 = new Product {Id = 2, Name = "Product2", Price = 55};
      var cart = new Cart();
      cart.AddItem(product1, 1);
      cart.AddItem(product2, 1);
      cart.AddItem(product1, 5);

      var result = cart.ComputeTotalValue();

      Assert.AreEqual(result, 655);
    }

    [TestMethod]
    public void Can_Clear_Contents()
    {
      var product1 = new Product {Id = 1, Name = "Product1", Price = 100};
      var product2 = new Product {Id = 2, Name = "Product2", Price = 55};
      var cart = new Cart();
      cart.AddItem(product1, 1);
      cart.AddItem(product2, 1);
      cart.AddItem(product1, 5);
      cart.Clear();

      Assert.AreEqual(cart.Lines.Count(), 0);
    }


    [TestMethod]
    public void Can_Add_To_Cart()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
      }.AsQueryable());
      var cart = new Cart();
      var controller = new CartController(mock.Object, null);
      controller.AddToCart(cart, 1, null);

      Assert.AreEqual(cart.Lines.Count(), 1);
      Assert.AreEqual(cart.Lines.ToList()[0].Product.Id, 1);
    }

    [TestMethod]
    public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
      }.AsQueryable());
      var cart = new Cart();
      var controller = new CartController(mock.Object, null);

      var result = controller.AddToCart(cart, 2, "myUrl");

      Assert.AreEqual(result.RouteValues["action"], "Index");
      Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
    }

    [TestMethod]
    public void Can_View_Cart_Contents()
    {
      var cart = new Cart();
      var target = new CartController(null, null);

      var result = (CartIndexViewModel) target.Index(cart, "myUrl").ViewData.Model;

      Assert.AreSame(result.Cart, cart);
      Assert.AreEqual(result.ReturnUrl, "myUrl");
    }

    [TestMethod]
    public void Cannot_Checkout_Empty_Cart()
    {
      var mock = new Mock<IOrderProcessor>();
      var cart = new Cart();
      var shippingDetails = new ShippingDetails();
      var controller = new CartController(null, mock.Object);

      var result = controller.Checkout(cart, shippingDetails);

      mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

      Assert.AreEqual("", result.ViewName);
      Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
    }

    [TestMethod]
    public void Cannot_Checkout_Invalid_ShippingDetails()
    {
      var mock = new Mock<IOrderProcessor>();
      var cart = new Cart();
      cart.AddItem(new Product(), 1);
      var controller = new CartController(null, mock.Object);
      controller.ModelState.AddModelError("error", "error");

      var result = controller.Checkout(cart, new ShippingDetails());

      mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

      Assert.AreEqual("", result.ViewName);
      Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
    }

    [TestMethod]
    public void Can_Checkout_And_Submit_Order()
    {
      var mock = new Mock<IOrderProcessor>();
      var cart = new Cart();
      cart.AddItem(new Product(), 1);
      var controller = new CartController(null, mock.Object);

      var result = controller.Checkout(cart, new ShippingDetails());

      mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

      Assert.AreEqual("Completed", result.ViewName);
      Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
    }
  }
}