using System;

namespace BinaryTree.Base
{
  public class TreeNode<T> : IComparable<T> where T : IComparable<T>
  {
    public TreeNode(T value) => Value = value;
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }
    public T Value { get; }
    public int CompareTo(T other) => Value.CompareTo(other);
  }
}