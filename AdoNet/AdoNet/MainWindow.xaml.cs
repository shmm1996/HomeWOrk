using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace AdoNet
{
  public partial class MainWindow : Window
  {
    private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDB;Integrated Security=true;";
    private readonly SqlConnection _conn;

    public MainWindow()
    {
      InitializeComponent();
      InputField.KeyDown += (s, e) =>
      {
        switch (e.Key)
        {
          case Key.Enter: Command(); return;
          case Key.F1: InputField.Text = "select * from users"; return;
          case Key.F2: InputField.Text = "insert into users (FirstName, Age) values ('', )"; return;
          case Key.F3: InputField.Text = "delete from users where"; return;
        }
      };
      Btn.Click += (s, e) => Command();
      _conn = new SqlConnection {ConnectionString = ConnectionString};
    }

    private void Command()
    {
      SqlDataReader reader = null;
      try
      {
        var comm = new SqlCommand {CommandText = InputField.Text, Connection = _conn};
        _conn.Open();
        var table = new DataTable();
        reader = comm.ExecuteReader();
        int line = 0;
        do
        {
          while (reader.Read())
          {
            if (line == 0)
            {
              for (int i = 0; i < reader.FieldCount; i++)
                table.Columns.Add(reader.GetName(i));
              line++;
            }
            var row = table.NewRow();
            for (int i = 0; i < reader.FieldCount; i++)
              row[i] = reader[i];

            table.Rows.Add(row);
          }
        } while (reader.NextResult());
        DataView.ItemsSource = table.DefaultView;
      }
      catch (Exception)
      {
        MessageBox.Show("Probably wrong request syntax");
      }
      finally
      {
        _conn?.Close();
        reader?.Close();
      }
    }
  }
}
