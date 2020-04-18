using System;
using BinaryTree.Base;

namespace BinaryTree.Ext
{
  public static class TreeNodeTasksExt
  {
    public static int RightHeight<T>(this TreeNode<T> node) where T : IComparable<T> => MaxChildHeight<T>(node.Right);
    public static int LeftHeight<T>(this TreeNode<T> node) where T : IComparable<T> => MaxChildHeight<T>(node.Left);
    private static int MaxChildHeight<T>(TreeNode<T> node) where T : IComparable<T> =>
      node == null ? 0 : 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
  }
}