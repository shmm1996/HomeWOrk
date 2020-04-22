using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
  public class CartController : Controller
  {
    private readonly IProductsRepository _repository;
    private readonly IOrderProcessor _orderProcessor;

    public CartController(IProductsRepository repository, IOrderProcessor processor)
    {
      _repository = repository;
      _orderProcessor = processor;
    }

    public ViewResult Checkout() => View(new ShippingDetails());

    [HttpPost]
    public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
    {
      if (!cart.Lines.Any())
        ModelState.AddModelError("", "Извините, ваша корзина пуста!");
      if (!ModelState.IsValid)
        return View(shippingDetails);
      _orderProcessor.ProcessOrder(cart, shippingDetails);
      cart.Clear();
      return View("Completed");
    }

    public ViewResult Index(Cart cart, string returnUrl) =>
      View(new CartIndexViewModel {Cart = cart, ReturnUrl = returnUrl});


    public PartialViewResult Summary(Cart cart)
      => PartialView(cart);

    public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
    {
      var product = _repository.Products.FirstOrDefault(p => p.Id == Id);
      if (product != null)
        cart.AddItem(product, 1);
      return RedirectToAction("Index", new {returnUrl});
    }

    public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
    {
      var product = _repository.Products.FirstOrDefault(p => p.Id == Id);
      if (product != null)
        cart.RemoveLine(product);
      return RedirectToAction("Index", new {returnUrl});
    }
  }
}