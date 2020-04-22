using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.WebUI.Controllers;
using Store.WebUI.Infrastructure.Abstract;
using Store.WebUI.Models;

namespace Store.UnitTests
{
  [TestClass]
  public class AdminSecurityTests
  {
    [TestMethod]
    public void Can_Login_With_Valid_Credentials()
    {
      var mock = new Mock<IAuthProvider>();
      mock.Setup(m => m.Authenticate("admin", "12345")).Returns(true);
      var model = new LoginViewModel
      {
        UserName = "admin",
        Password = "12345"
      };
      var target = new AccountController(mock.Object);

      var result = target.Login(model, "/MyURL");

      Assert.IsInstanceOfType(result, typeof(RedirectResult));
      Assert.AreEqual("/MyURL", ((RedirectResult) result).Url);
    }

    [TestMethod]
    public void Cannot_Login_With_Invalid_Credentials()
    {
      var mock = new Mock<IAuthProvider>();
      mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
      var model = new LoginViewModel
      {
        UserName = "badUser",
        Password = "badPass"
      };
      var target = new AccountController(mock.Object);

      var result = target.Login(model, "/MyURL");

      Assert.IsInstanceOfType(result, typeof(ViewResult));
      Assert.IsFalse(((ViewResult) result).ViewData.ModelState.IsValid);
    }
  }
}