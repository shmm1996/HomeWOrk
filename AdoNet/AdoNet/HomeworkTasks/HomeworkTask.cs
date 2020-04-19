using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace AdoNet.HomeworkTasks
{
  public abstract class HomeworkTask
  {
    protected readonly MainWindow Window;
    protected HomeworkTask(MainWindow window) => Window = window;
    protected abstract SqlConnection SqlConnection { get; }

    public void Startup()
    {
      Window.InputField.KeyDown += FKeysHandler;
      Window.Btn.Click += ClickHandler;
    }

    protected virtual void FKeysHandler(object sender, KeyEventArgs e) {}
    protected virtual void ClickHandler(object sender, EventArgs e) => Command();
    protected void Command()
    {
      SqlDataReader reader = null;
      try
      {
        var comm = new SqlCommand { CommandText = Window.InputField.Text, Connection = SqlConnection };
        SqlConnection.Open();
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

        Window.DataView.ItemsSource = table.DefaultView;
      }
      catch (Exception)
      {
        MessageBox.Show("Probably wrong request syntax");
      }
      finally
      {
        SqlConnection?.Close();
        reader?.Close();
      }
    }

    public void Abort()
    {
      Window.InputField.KeyDown += FKeysHandler;
      Window.Btn.Click += ClickHandler;
    }
  }
}