using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.WebUI.HtmlHelpers;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Controllers;
using Store.WebUI.Models;

namespace Store.UnitTests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Can_Paginate()
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
      var controller = new ProductController(mock.Object) {PageSize = 3};

      var result = (ProductsListViewModel) controller.List(null, 2).Model;
      var products = result.Products.ToList();

      Assert.IsTrue(products.Count == 2);
      Assert.AreEqual(products[0].Name, "Product4");
      Assert.AreEqual(products[1].Name, "Product5");
    }

    [TestMethod]
    public void Can_Generate_Page_Links()
    {
      var pagingInfo = new PagingInfo
      {
        CurrentPage = 2,
        TotalItems = 28,
        ItemsPerPage = 10
      };

      var result = ((HtmlHelper) null).PageLinks(pagingInfo, PageUrlDelegate);

      Assert.AreEqual(
        @"<a class=""btn btn-default"" href=""Page1"">1</a><a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a><a class=""btn btn-default"" href=""Page3"">3</a>",
        result.ToString());

      string PageUrlDelegate(int i) => "Page" + i;
    }

    [TestMethod]
    public void Can_Send_Pagination_View_Model()
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
      var controller = new ProductController(mock.Object) {PageSize = 3};

      var result = (ProductsListViewModel) controller.List(null, 2).Model;
      var pageInfo = result.PagingInfo;

      Assert.AreEqual(pageInfo.CurrentPage, 2);
      Assert.AreEqual(pageInfo.ItemsPerPage, 3);
      Assert.AreEqual(pageInfo.TotalItems, 5);
      Assert.AreEqual(pageInfo.TotalPages, 2);
    }

    [TestMethod]
    public void Can_Filter_Products()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
        new Product {Id = 2, Name = "Product2", Category = "Category2"},
        new Product {Id = 3, Name = "Product3", Category = "Category1"},
        new Product {Id = 4, Name = "Product4", Category = "Category2"},
        new Product {Id = 5, Name = "Product5", Category = "Category3"}
      });
      var controller = new ProductController(mock.Object) {PageSize = 3};

      var result = ((ProductsListViewModel) controller.List("Category2", 1).Model).Products.ToList();

      Assert.AreEqual(result.Count, 2);
      Assert.IsTrue(result[0].Name == "Product2" && result[0].Category == "Category2");
      Assert.IsTrue(result[1].Name == "Product4" && result[1].Category == "Category2");
    }

    [TestMethod]
    public void Can_Create_Categories()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
        new Product {Id = 2, Name = "Product2", Category = "Category1"},
        new Product {Id = 3, Name = "Product3", Category = "Category2"},
        new Product {Id = 4, Name = "Product4", Category = "Category3"},
      });
      var target = new NavController(mock.Object);

      var results = ((IEnumerable<string>) target.Menu().Model).ToList();

      Assert.AreEqual(results.Count(), 3);
      Assert.AreEqual(results[0], "Category1");
      Assert.AreEqual(results[1], "Category2");
      Assert.AreEqual(results[2], "Category3");
    }

    [TestMethod]
    public void Indicates_Selected_Category()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new Product[]
      {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
        new Product {Id = 2, Name = "Product2", Category = "Category1"}
      });
      var target = new NavController(mock.Object);
      var categoryToSelect = "Category2";

      var result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

      Assert.AreEqual(categoryToSelect, result);
    }

    [TestMethod]
    public void Generate_Category_Specific_Product_Count()
    {
      var mock = new Mock<IProductsRepository>();
      mock.Setup(m => m.Products).Returns(new List<Product>
      {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
        new Product {Id = 2, Name = "Product2", Category = "Category2"},
        new Product {Id = 3, Name = "Product3", Category = "Category1"},
        new Product {Id = 4, Name = "Product4", Category = "Category2"},
        new Product {Id = 5, Name = "Product5", Category = "Category3"}
      });
      var controller = new ProductController(mock.Object) {PageSize = 3};
      var res1 = ((ProductsListViewModel) controller.List("Category1").Model).PagingInfo.TotalItems;
      var res2 = ((ProductsListViewModel) controller.List("Category2").Model).PagingInfo.TotalItems;
      var res3 = ((ProductsListViewModel) controller.List("Category3").Model).PagingInfo.TotalItems;
      var resAll = ((ProductsListViewModel) controller.List(null).Model).PagingInfo.TotalItems;

      Assert.AreEqual(res1, 2);
      Assert.AreEqual(res2, 2);
      Assert.AreEqual(res3, 1);
      Assert.AreEqual(resAll, 5);
    }
  }
}