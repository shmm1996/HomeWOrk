using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree.Base
{
  public class Tree<T> : IEnumerable<T> where T : IComparable<T>
  {
    public TreeNode<T> Root { get; private set; }
    public int ElementsCount { get; private set; }

    public void Add(T value)
    {
      if (Root == null) Root = new TreeNode<T>(value);
      else AddTo(Root, value);
      ElementsCount++;
    }

    private void AddTo(TreeNode<T> node, T value)
    {
      if (value.CompareTo(node.Value) < 0)
      {
        if (node.Left == null) node.Left = new TreeNode<T>(value);
        else AddTo(node.Left, value);
      }
      else
      {
        if (node.Right == null) node.Right = new TreeNode<T>(value);
        else AddTo(node.Right, value);
      }
    }

    public IEnumerator<T> GetEnumerator() => LeftOrderEnumerable().Select(node => node.Value).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<TreeNode<T>> LeftOrderEnumerable() => LeftOrderEnumerable(Root);

    public IEnumerable<TreeNode<T>> LeftOrderEnumerable(TreeNode<T> root)
    {
      if (root == null) yield break;
      var queue = new Queue<TreeNode<T>>();
      queue.Enqueue(root);
      while (queue.Count != 0)
      {
        var temp = queue.Dequeue();
        yield return temp;
        if (temp.Left != null) queue.Enqueue(temp.Left);
        if (temp.Right != null) queue.Enqueue(temp.Right);
      }
    }

    public IEnumerable<TreeNode<T>> RightOrderEnumerable() => RightOrderEnumerable(Root);

    public IEnumerable<TreeNode<T>> RightOrderEnumerable(TreeNode<T> root)
    {
      if (root == null) yield break;
      var queue = new Queue<TreeNode<T>>();
      queue.Enqueue(root);
      while (queue.Count != 0)
      {
        var temp = queue.Dequeue();
        yield return temp;
        if (temp.Right != null) queue.Enqueue(temp.Right);
        if (temp.Left != null) queue.Enqueue(temp.Left);
      }
    }

    public IEnumerable<IEnumerable<TreeNode<T>>> GetByLevelsEnumerable()
    {
      var result = new List<IEnumerable<TreeNode<T>>>();
      if (Root != null)
        result.Add(new List<TreeNode<T>> {Root}.AsEnumerable());
      while (true)
      {
        var level = new List<TreeNode<T>>();
        foreach (var node in result[result.Count - 1])
        {
          if (node.Left != null) level.Add(node.Left);
          if (node.Right != null) level.Add(node.Right);
        }
        if (level.Count == 0)
          break;
        result.Add(level.AsEnumerable());
      }
      return result.AsEnumerable();
    }
  }
}