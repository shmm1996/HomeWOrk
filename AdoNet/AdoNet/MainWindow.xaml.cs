using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AdoNet.HomeworkTasks;

namespace AdoNet
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      HomeworkTasksManager.Setup(this);
      MenuTask.ItemsSource = new[]
      {
        BuildMenuItem("Task1 AdoNet",0),
        BuildMenuItem("Task2 GOE",1),
        BuildMenuItem("Task3 PC",2)
      }.AsEnumerable();
    }

    public MenuItem BuildMenuItem(string context, int taskIndex)
    {
      var menuItem = new MenuItem {Header = context};
      menuItem.Click += (s, e) => { if (HomeworkTasksManager.LoadTask(taskIndex)) Title = $"MainWindow - {context}"; };
      return menuItem;
    }
  }
}
