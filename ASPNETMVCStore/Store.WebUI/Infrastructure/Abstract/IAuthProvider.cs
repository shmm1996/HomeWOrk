namespace Store.WebUI.Infrastructure.Abstract
{
  public interface IAuthProvider
  {
    bool Authenticate(string username, string password);
  }
}