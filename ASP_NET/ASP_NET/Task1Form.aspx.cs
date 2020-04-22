using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP_NET
{
  public partial class Task1Form : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Page.IsPostBack) return;
      Func<string, bool, ListItem> item = (text, selected) => new ListItem(text)
      {
        Selected = selected,
        Enabled = false
      };
      checkboxlist.Items.AddRange(new[]
      {
        item("ПН", false),
        item("ВТ", false),
        item("СР", false),
        item("ЧТ", false),
        item("ПТ", false),
        item("СБ", true),
        item("ВС", true)
      });
    }
  }
}