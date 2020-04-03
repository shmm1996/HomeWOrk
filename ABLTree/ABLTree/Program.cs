using System;

namespace ABLTree
{
    class Program
    {
        static void Main()
        {
            var avl = new AVLTree<int>
            {
                30,
                4,
                9,
                8,
                7,
                11,
                12,
                14
            };

            avl.Remove(12);

            foreach (var item in avl)
                Console.WriteLine(item);
        }
    }
}
