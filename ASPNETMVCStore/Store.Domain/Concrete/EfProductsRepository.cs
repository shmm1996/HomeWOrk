using System.Collections.Generic;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.Domain.Concrete
{
  public class EFProductsRepository : IProductsRepository
  {
    private readonly EFDbContext _context;

    public EFProductsRepository() => _context = new EFDbContext();

    public IEnumerable<Product> Products => _context.Products;

    public void SaveProduct(Product product)
    {
      if (product.Id == 0)
        _context.Products.Add(product);
      else
      {
        var dbEntry = _context.Products.Find(product.Id);
        if (dbEntry != null)
        {
          dbEntry.Name = product.Name;
          dbEntry.Description = product.Description;
          dbEntry.Price = product.Price;
          dbEntry.Category = product.Category;
          dbEntry.ImageData = product.ImageData;
          dbEntry.ImageMimeType = product.ImageMimeType;
        }
      }

      _context.SaveChanges();
    }

    public Product DeleteProduct(int productId)
    {
      var dbEntry = _context.Products.Find(productId);
      if (dbEntry == null) return null;
      _context.Products.Remove(dbEntry);
      _context.SaveChanges();
      return dbEntry;
    }
  }
}