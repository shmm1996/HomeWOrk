using System.Web.Mvc;
using System.Linq;
using System.Web;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.WebUI.Controllers
{
  [Authorize]
  public class AdminController : Controller
  {
    private readonly IProductsRepository _repository;
    public AdminController(IProductsRepository repository) => _repository = repository;

    public ViewResult Index() => View(_repository.Products);
    public ViewResult Edit(int Id) => View(_repository.Products.FirstOrDefault(p => p.Id == Id));

    [HttpPost]
    public ActionResult Edit(Product product, HttpPostedFileBase image = null)
    {
      if (!ModelState.IsValid) return View(product);
      if (image != null)
      {
        product.ImageMimeType = image.ContentType;
        product.ImageData = new byte[image.ContentLength];
        image.InputStream.Read(product.ImageData, 0, image.ContentLength);
      }
      _repository.SaveProduct(product);
      TempData["message"] = string.Format("Изменения в продукте \"{0}\" были сохранены", product.Name);
      return RedirectToAction("Index");
    }

    public ViewResult Create() => View("Edit", new Product());

    [HttpPost]
    public ActionResult Delete(int Id)
    {
      var deletedProduct = _repository.DeleteProduct(Id);
      if (deletedProduct != null)
        TempData["message"] = $"Продукт \"{deletedProduct.Name}\" был удален";
      return RedirectToAction("Index");
    }
  }
}