using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstract;

namespace Store.WebUI.Controllers
{
  public class NavController : Controller
  {
    private readonly IProductsRepository _repository;

    public NavController(IProductsRepository repository) => _repository = repository;

    public PartialViewResult Menu(string category = null)
    {
      ViewBag.SelectedCategory = category;
      IEnumerable<string> categories = _repository.Products
        .Select(p => p.Category)
        .Distinct()
        .OrderBy(x => x);
      return PartialView("FlexMenu", categories);
    }
  }
}