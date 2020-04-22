using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
  public class Product
  {
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }

    [Display(Name = "Название")]
    //[Required(ErrorMessage = "Пожалуйста, введите название продукта")]
    public string Name { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Описание")]
    //[Required(ErrorMessage = "Пожалуйста, введите описание для продукта")]
    public string Description { get; set; }

    [Display(Name = "Categoryегория")]
    //[Required(ErrorMessage = "Пожалуйста, укажите Categoryегорию для продукта")]
    public string Category { get; set; }

    [Display(Name = "Цена (грн)")]
    //[Required]
    //[Range(0.01, float.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
    public double Price { get; set; }

    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
  }
}