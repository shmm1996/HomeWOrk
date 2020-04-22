using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
  public class ProductController : Controller
  {
    private readonly IProductsRepository _repository;
    public int PageSize = 4;

    public ProductController(IProductsRepository repository) => _repository = repository;

    public ViewResult List(string category, int page = 1)
    {
      var model = new ProductsListViewModel
      {
        Products = _repository.Products
          .Where(p => category == null || p.Category == category)
          .OrderBy(p => p.Id)
          .Skip((page - 1) * PageSize)
          .Take(PageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = PageSize,
          TotalItems = category == null
            ? _repository.Products.Count()
            : _repository.Products.Count(p => p.Category == category)
        },
        CurrentCategory = category
      };
      return View(model);
    }

    public FileContentResult GetImage(int Id)
    {
      var product = _repository.Products.FirstOrDefault(p => p.Id == Id);
      return product != null ? File(product.ImageData, product.ImageMimeType) : null;
    }
  }
}