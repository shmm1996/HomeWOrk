using System.Collections.Generic;
using Store.Domain.Entities;

namespace Store.Domain.Abstract
{
  public interface IProductsRepository
  {
    IEnumerable<Product> Products { get; }
    void SaveProduct(Product product);
    Product DeleteProduct(int productId);
  }
}