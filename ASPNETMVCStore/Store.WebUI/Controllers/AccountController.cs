using System.Web.Mvc;
using Store.WebUI.Infrastructure.Abstract;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
  public class AccountController : Controller
  {
    private readonly IAuthProvider _authProvider;

    public AccountController(IAuthProvider authProvider) => _authProvider = authProvider;

    public ViewResult Login() => View();

    [HttpPost]
    public ActionResult Login(LoginViewModel model, string returnUrl)
    {
      if (!ModelState.IsValid) return View();
      if (_authProvider.Authenticate(model.UserName, model.Password))
        return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
      ModelState.AddModelError("", "Неправильный логин или пароль");
      return View();
    }
  }
}