using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTree.Base;

namespace BinaryTree.Ext
{
  public static class TreeTasksExt
  {
    public static long Task1_Sum(this Tree<int> tree) => tree.Sum();
    public static long Task1_Sum(this Tree<long> tree) => tree.Sum();
    public static double Task1_Sum(this Tree<float> tree) => tree.Sum();
    public static double Task1_Sum(this Tree<double> tree) => tree.Sum();

    public static IEnumerable<T> Task2_DifferentChildCount<T>(this Tree<T> tree) where T : IComparable<T> =>
      from node in tree.LeftOrderEnumerable()
      where (node.Left == null && node.Right != null) || (node.Left != null && node.Right == null)
      select node.Value;

    public static IEnumerable<T> Task3_DifferentSubtreeHeight<T>(this Tree<T> tree) where T : IComparable<T> =>
      from node in tree.LeftOrderEnumerable()
      where node.LeftHeight() != node.RightHeight()
      select node.Value;

    public static int Task4_ElementCount<T>(this Tree<T> tree, T x) where T : IComparable<T> =>
      tree.Count(value => value.Equals(x));

    public static T Task5_MaxElement<T>(this Tree<T> tree, out int count) where T : IComparable<T>
    {
      var max = tree.Max();
      count = tree.Count(x => x.Equals(max));
      return max;
    }

    public static bool Task6_Singleton<T>(this Tree<T> tree) where T : IComparable<T>
    {
      var query = (from value in tree select value).ToArray();
      for (int i = 0, j; i < query.Length; i++)
      for (j = i + 1; j < query.Length; j++)
        if (query[i].Equals(query[j]))
          return false;
      return true;
    }

    public static T Task7_MaxCount<T>(this Tree<T> tree, out int count) where T : IComparable<T>
    {
      count = 0;
      var dictionary = new Dictionary<T, int>();
      foreach (var value in tree)
      {
        if (dictionary.Keys.Contains(value))
          dictionary[value]++;
        else
          dictionary.Add(value, 1);
      }

      var max = dictionary.Max(x => x.Value);
      count = max;
      return dictionary.FirstOrDefault(x => x.Value == max).Key;
    }

    public static bool Task8_IsTreeSimetric<T>(this Tree<T> tree) where T : IComparable<T>
    {
      var leftSubtree = tree.LeftOrderEnumerable(tree.Root.Left);
      var rightSubtree = tree.RightOrderEnumerable(tree.Root.Right);
      if (leftSubtree.Count() != rightSubtree.Count()) return false;
      var leftSubtreeEnumerator = leftSubtree.GetEnumerator();
      var rightSubtreeEnumerator = rightSubtree.GetEnumerator();
      do
      {
        if (leftSubtreeEnumerator.Current?.Left == null && rightSubtreeEnumerator.Current?.Right != null) return false;
        if (leftSubtreeEnumerator.Current?.Left != null && rightSubtreeEnumerator.Current?.Right == null) return false;
        rightSubtreeEnumerator.MoveNext();
      } while (leftSubtreeEnumerator.MoveNext());

      return true;
    }

    public static IEnumerable<int> Task9_MaxElementsByLevels(this Tree<int> tree) =>
      from level in tree.GetByLevelsEnumerable()
      select level.Sum(x => x.Value);

    public static IEnumerable<long> Task9_MaxElementsByLevels(this Tree<long> tree) =>
      from level in tree.GetByLevelsEnumerable()
      select level.Sum(x => x.Value);

    public static IEnumerable<float> Task9_MaxElementsByLevels(this Tree<float> tree) =>
      from level in tree.GetByLevelsEnumerable()
      select level.Sum(x => x.Value);

    public static IEnumerable<double> Task9_MaxElementsByLevels(this Tree<double> tree) =>
      from level in tree.GetByLevelsEnumerable()
      select level.Sum(x => x.Value);

    public static int Task11_EvenSum(this Tree<int> tree)
    {
      var sum = 0;
      var levels = tree.GetByLevelsEnumerable().ToArray();
      for (var i = 1; i < levels.Length; i += 2)
        sum += levels[i].Sum(x => x.Value);
      return sum;
    }

    public static long Task11_EvenSum(this Tree<long> tree)
    {
      long sum = 0;
      var levels = tree.GetByLevelsEnumerable().ToArray();
      for (var i = 1; i < levels.Length; i += 2)
        sum += levels[i].Sum(x => x.Value);
      return sum;
    }

    public static float Task11_EvenSum(this Tree<float> tree)
    {
      var sum = 0f;
      var levels = tree.GetByLevelsEnumerable().ToArray();
      for (var i = 1; i < levels.Length; i += 2)
        sum += levels[i].Sum(x => x.Value);
      return sum;
    }

    public static double Task11_EvenSum(this Tree<double> tree)
    {
      var sum = .0;
      var levels = tree.GetByLevelsEnumerable().ToArray();
      for (var i = 1; i < levels.Length; i += 2)
        sum += levels[i].Sum(x => x.Value);
      return sum;
    }
  }
}