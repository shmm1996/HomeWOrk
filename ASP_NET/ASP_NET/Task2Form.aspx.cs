using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP_NET
{
  public partial class Task2Form : Page
  {

    private bool TextBoxIsNotEmpty => !(string.IsNullOrEmpty(textBox.Text) || string.IsNullOrWhiteSpace(textBox.Text));

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void OnClick_New(object sender, EventArgs e)
    {
      if (TextBoxIsNotEmpty)
        listBox.Items.Add(new ListItem(textBox.Text));
      textBox.Text = default;
    }

    protected void OnClick_Edit(object sender, EventArgs e)
    {
      var item = listBox.SelectedItem;
      if (TextBoxIsNotEmpty && item != null)
        item.Text = textBox.Text;
      textBox.Text = default;
    }

    protected void OnClick_Remove(object sender, EventArgs e) => listBox.Items.Remove(listBox.SelectedItem);
  }
}