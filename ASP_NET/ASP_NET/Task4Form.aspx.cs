using System;
using System.Web.UI;

namespace ASP_NET
{
  public partial class Task4Form : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Page.IsPostBack) return;
    }

    private bool CheckLogin()
    {
      if (string.IsNullOrWhiteSpace(loginField.Text) || loginField.Text.StartsWith(" ") || loginField.Text.EndsWith(" "))
      {
        Alert($"Login cant start/end with space!");
        return false;
      }

      if (loginField.Text.Length < 4)
      {
        Alert($"Login length must be at least 4 characters!");
        return false;
      }

      return true;
    }

    private bool CheckPassword()
    {
      if (string.IsNullOrWhiteSpace(passwordField.Text) || passwordField.Text.StartsWith(" ") || passwordField.Text.EndsWith(" "))
      {
        Alert($"Password cant start/end with space!");
        return false;
      }

      if (passwordField.Text.Length < 4)
      {
        Alert($"Password length must be at least 4 characters!");
        return false;
      }

      return true;
    }

    private bool CheckGender()
    {
      if (radioBtnMale.Checked == false && radioBtnFemale.Checked == false)
      {
        Alert($"Choose your gender!");
        return false;
      }

      return true;
    }

    private void Alert(string text) => ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", $"alert('{text}')", true);

    protected void regBtn_OnClick(object sender, EventArgs e)
    {
      if (CheckLogin() && CheckPassword() && CheckGender())
        Alert($"Success registration!");
    }
  }
}