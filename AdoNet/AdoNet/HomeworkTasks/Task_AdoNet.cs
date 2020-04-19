using System.Data.SqlClient;
using System.Windows.Input;

namespace AdoNet.HomeworkTasks
{
  public class Task_AdoNet : HomeworkTask
  {
    protected override SqlConnection SqlConnection => new SqlConnection { ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDB;Integrated Security=true;" };

    public Task_AdoNet(MainWindow window) : base(window) { }

    protected override void FKeysHandler(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.Enter: Command(); return;
        case Key.F1: Window.InputField.Text = "select * from users"; return;
        case Key.F2: Window.InputField.Text = "insert into users (FirstName, Age) values ('', )"; return;
        case Key.F3: Window.InputField.Text = "delete from users where"; return;
      }
    }
  }
}