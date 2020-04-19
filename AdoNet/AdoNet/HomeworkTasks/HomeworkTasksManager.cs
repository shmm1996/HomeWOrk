namespace AdoNet.HomeworkTasks
{
  public static class HomeworkTasksManager
  {

    private static HomeworkTask[] _tasks;
    private static int _currentTaskNumber;

    public static void Setup(MainWindow window)
    {
      _currentTaskNumber = -1;
      _tasks = new HomeworkTask []
      {
        new Task_AdoNet(window),
        new Task_Geo(window) 
      };
    }

    public static bool LoadTask(int taskNumber)
    {
      if (taskNumber == _currentTaskNumber || taskNumber < 0 || taskNumber >= _tasks.Length) return false;
      if (_currentTaskNumber >= 0 && _currentTaskNumber < _tasks.Length) _tasks[_currentTaskNumber].Abort();
      _currentTaskNumber = taskNumber;
      _tasks[_currentTaskNumber].Startup();
      return true;
    }
  }
}