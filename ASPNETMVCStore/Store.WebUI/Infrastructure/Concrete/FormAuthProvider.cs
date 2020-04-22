using System.Web.Security;
using Store.WebUI.Infrastructure.Abstract;

namespace Store.WebUI.Infrastructure.Concrete
{
  public class FormAuthProvider : IAuthProvider
  {
    [System.Obsolete]
    public bool Authenticate(string username, string password)
    {
      var result = FormsAuthentication.Authenticate(username, password);
      if (result) FormsAuthentication.SetAuthCookie(username, false);
      return result;
    }
  }
}