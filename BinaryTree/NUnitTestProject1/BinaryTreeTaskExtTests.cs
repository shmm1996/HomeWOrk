using NUnit.Framework;
using BinaryTree.Ext;
using BinaryTree.Base;
using System.Linq;

namespace BinaryTreeTests
{
  public class BinaryTreeTaskExtTests
  {
    private Tree<int> _treeInt;

    [SetUp]
    public void SetUp()
    {
      _treeInt = new Tree<int>();
      var valuesTree = new[] {15, 27, 10, 7, 17, 11, 3, 9, 30, 28, 20, 1, 31};
      foreach (var item in valuesTree)
        _treeInt.Add(item);
    }

    [Test]
    public void Sum_int_Test()
    {
      var trueSum = _treeInt.Sum();
      var sum = _treeInt.Task1_Sum();
      Assert.AreEqual(trueSum, sum);
    }

    [Test]
    public void DifferentChildCount_Test()
    {
      var list = _treeInt.Task2_DifferentChildCount().ToArray();
      Assert.IsTrue(list.Contains(10));
      Assert.IsTrue(list.Contains(17));
      Assert.IsTrue(list.Contains(7));
      Assert.IsTrue(list.Contains(27));
      Assert.IsTrue(list.Contains(3));
    }

    [Test]
    public void DifferentSubtreeHeight_Test()
    {
      var list = _treeInt.Task3_DifferentSubtreeHeight().ToArray();
      Assert.IsTrue(list.Contains(10));
      Assert.IsTrue(list.Contains(15));
      Assert.IsTrue(list.Contains(17));
      Assert.IsTrue(list.Contains(7));
      Assert.IsTrue(list.Contains(3));
    }

    [Test]
    public void ElementCount_Test()
    {
      _treeInt.Add(15);
      Assert.AreEqual(_treeInt.Task4_ElementCount(15), 2);
      Assert.AreEqual(_treeInt.Task4_ElementCount(30), 1);
      Assert.AreEqual(_treeInt.Task4_ElementCount(2), 0);
    }

    [Test]
    public void MaxElement_Test()
    {
      Assert.AreEqual(_treeInt.Task5_MaxElement(out var count), 31);
      Assert.AreEqual(count, 1);
      _treeInt.Add(31);
      Assert.AreEqual(_treeInt.Task5_MaxElement(out count), 31);
      Assert.AreEqual(count, 2);
      _treeInt.Add(32);
      Assert.AreEqual(_treeInt.Task5_MaxElement(out count), 32);
      Assert.AreEqual(count, 1);
    }

    [Test]
    public void Singleton_Test()
    {
      Assert.IsFalse(_treeInt.Task6_Singleton());
      _treeInt.Add(15);
      Assert.IsTrue(_treeInt.Task6_Singleton());
    }

    [Test]
    public void MaxCount_Test()
    {
      Assert.AreEqual(_treeInt.Task7_MaxCount(out var count), 15);
      Assert.AreEqual(count, 1);
      _treeInt.Add(30);
      Assert.AreEqual(_treeInt.Task7_MaxCount(out count), 30);
      Assert.AreEqual(count, 2);
    }

    [Test]
    public void IsTreeSimetric_Test()
    {
      Assert.IsFalse(_treeInt.Task8_IsTreeSimetric());
      _treeInt = new Tree<int>();
      var valuesTree = new [] {15, 10, 20, 3, 30, 7, 25};
      foreach (var item in valuesTree)
        _treeInt.Add(item);
      Assert.IsTrue(_treeInt.Task8_IsTreeSimetric());
      _treeInt.Add(30);
      Assert.IsFalse(_treeInt.Task8_IsTreeSimetric());
    }

    [Test]
    public void MaxElementsByLevels_Test()
    {
      int[] values = _treeInt.Task9_MaxElementsByLevels().ToArray();
      Assert.AreEqual(values[0], 15);
      Assert.AreEqual(values[1], 27);
      Assert.AreEqual(values[2], 30);
      Assert.AreEqual(values[3], 31);
      Assert.AreEqual(values[4], 1);
      _treeInt.Add(25);
      values = _treeInt.Task9_MaxElementsByLevels().ToArray();
      Assert.AreEqual(values[4], 25);
    }

    [Test]
    public void CountNodsAndSheetsByLevels_Test()
    {
      var expected = new[]
      {
        new TreeTasksExt.Task10LevelResult {CountNods = 1, CountSheets = 0},
        new TreeTasksExt.Task10LevelResult {CountNods = 2, CountSheets = 0},
        new TreeTasksExt.Task10LevelResult {CountNods = 3, CountSheets = 1},
        new TreeTasksExt.Task10LevelResult {CountNods = 1, CountSheets = 4},
        new TreeTasksExt.Task10LevelResult {CountNods = 0, CountSheets = 1},
      }.AsEnumerable();
      var result = _treeInt.Task10_CountNodsAndSheetsByLevels();
      Assert.AreEqual(expected, result);
    }

    [Test]
    public void EvenSum_Test()
    {
      Assert.AreEqual(_treeInt.Task11_EvenSum(), 81);
    }
  }
}