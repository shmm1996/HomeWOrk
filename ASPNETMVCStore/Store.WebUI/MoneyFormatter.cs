using System.Globalization;

namespace Store.WebUI
{
  public static class MoneyFormatter
  {
    public static string FormatToMoneyUAH(this double value) =>
      value.ToString("0.00₴", new NumberFormatInfo {CurrencyDecimalSeparator = "."});
  }
}