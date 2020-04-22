using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Controllers;

namespace Store.UnitTests
{
  [TestClass]
  public class AdminTests
  {
    [TestMethod]
    public void Index_Contains_All_Products()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1"},
        new Product {Id = 2, Name = "Product2"},
        new Product {Id = 3, Name = "Product3"},
        new Product {Id = 4, Name = "Product4"},
        new Product {Id = 5, Name = "Product5"}
      });
      var controller = new AdminController(mock.Object);

      var result = ((IEnumerable<Product>) controller.Index().ViewData.Model).ToList();

      Assert.AreEqual(result.Count(), 5);
      Assert.AreEqual("Product1", result[0].Name);
      Assert.AreEqual("Product2", result[1].Name);
      Assert.AreEqual("Product3", result[2].Name);
    }

    [TestMethod]
    public void Can_Edit_Product()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1"},
        new Product {Id = 2, Name = "Product2"},
        new Product {Id = 3, Name = "Product3"},
        new Product {Id = 4, Name = "Product4"},
        new Product {Id = 5, Name = "Product5"}
      });
      var controller = new AdminController(mock.Object);
      var product1 = controller.Edit(1).ViewData.Model as Product;
      var product2 = controller.Edit(2).ViewData.Model as Product;
      var product3 = controller.Edit(3).ViewData.Model as Product;

      Assert.AreEqual(1, product1?.Id);
      Assert.AreEqual(2, product2?.Id);
      Assert.AreEqual(3, product3?.Id);
    }

    [TestMethod]
    public void Cannot_Edit_Nonexistent_Product()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1"},
        new Product {Id = 2, Name = "Product2"},
        new Product {Id = 3, Name = "Product3"},
        new Product {Id = 4, Name = "Product4"},
        new Product {Id = 5, Name = "Product5"}
      });
      var controller = new AdminController(mock.Object);

      var result = controller.Edit(6).ViewData.Model as Product;

      Assert.IsNull(result);
    }

    [TestMethod]
    public void Can_Save_Valid_Changes()
    {
      var mock = new Mock<IProductsRepository>();
      var controller = new AdminController(mock.Object);
      var product = new Product {Name = "Test"};

      var result = controller.Edit(product, null);

      mock.Verify(m => m.SaveProduct(product));
      Assert.IsNotInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Cannot_Save_Invalid_Changes()
    {
      var mock = new Mock<IProductsRepository>();
      var controller = new AdminController(mock.Object);
      var product = new Product {Name = "Test"};
      controller.ModelState.AddModelError("error", "error");

      var result = controller.Edit(product, null);

      mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Can_Delete_Valid_Products()
    {
      var product = new Product {Id = 2, Name = "Product2"};
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1"},
        new Product {Id = 2, Name = "Product2"},
        new Product {Id = 3, Name = "Product3"},
        new Product {Id = 4, Name = "Product4"},
        new Product {Id = 5, Name = "Product5"}
      });
      var controller = new AdminController(mock.Object);
      controller.Delete(product.Id);

      mock.Verify(m => m.DeleteProduct(product.Id));
    }
  }
}