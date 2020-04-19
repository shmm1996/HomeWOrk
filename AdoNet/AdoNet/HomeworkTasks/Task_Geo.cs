using System.Data.SqlClient;

namespace AdoNet.HomeworkTasks
{
  public class Task_Geo : HomeworkTask
  {
    public Task_Geo(MainWindow window) : base(window) { }
    protected override SqlConnection SqlConnection => null;
  }
}