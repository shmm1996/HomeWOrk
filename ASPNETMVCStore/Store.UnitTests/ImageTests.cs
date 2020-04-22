using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Controllers;

namespace Store.UnitTests
{
  [TestClass]
  public class ImageTests
  {
    [TestMethod]
    public void Can_Retrieve_Image_Data()
    {
      var product = new Product
      {
        Id = 2,
        Name = "Product2",
        ImageData = new byte[] { },
        ImageMimeType = "image/png"
      };
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1"},
        product,
        new Product {Id = 3, Name = "Product3"}
      }.AsQueryable());

      var controller = new ProductController(mock.Object);

      var result = controller.GetImage(2);

      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(FileResult));
      Assert.AreEqual(product.ImageMimeType, result.ContentType);
    }

    [TestMethod]
    public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1"},
        new Product {Id = 2, Name = "Product2"}
      }.AsQueryable());
      var controller = new ProductController(mock.Object);

      var result = controller.GetImage(10);

      Assert.IsNull(result);
    }
  }
}