using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP_NET
{
  public partial class Task3Form : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Page.IsPostBack) return;
      for (var i = 0; i < 10; i++)
        storageList.Items.Add(new ListItem($"Item {i}"));
    }

    protected void OnClick_AddAll(object sender, EventArgs e) => SwapAll(storageList, cartList);
    protected void OnClick_Add(object sender, EventArgs e) => SwapSelected(storageList, cartList);
    protected void OnClick_Remove(object sender, EventArgs e) => SwapSelected(cartList, storageList);
    protected void OnClick_RemoveAll(object sender, EventArgs e) => SwapAll(cartList, storageList);

    private void SwapAll(ListBox from, ListBox to)
    {
      for (var i = 0; i < from.Items.Count; i++)
        to.Items.Add(from.Items[i]);
      from.Items.Clear();
    }

    private void SwapSelected(ListBox from, ListBox to)
    {
      var indices = from.GetSelectedIndices();
      for (var i = indices.Length - 1; i >= 0; i--)
      {
        to.Items.Add(from.Items[indices[i]]);
        from.Items.RemoveAt(indices[i]);
      }
    }
  }
}