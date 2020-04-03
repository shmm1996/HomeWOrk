using NUnit.Framework;

namespace ABLTree
{
    [TestFixture]
    class AVLTreeTest
    {
        [Test]
        public void AddTest()
        {
            var avl0 = new AVLTree<int>
            {
                1,2,4,5
            };
            avl0.Add(3);
            var avl1 = new AVLTree<int>
            {
                5,4,3,2,1
            };
            CollectionAssert.AreEqual(avl0, avl1);
        }

        [Test]
        public void RemoveTest()
        {
            var avl0 = new AVLTree<int>
            {
                1,2,3,4,5
            };
            avl0.Remove(1);
            var avl1 = new AVLTree<int>
            {
                5,4,3,2
            };
            CollectionAssert.AreEqual(avl0, avl1);
        }

        [Test]
        public void FindTestFalse()
        {
            var avl = new AVLTree<int>
            {
                1,2,3,4,5
            };
            Assert.AreEqual(false, avl.Contains(0));
        }

        [Test]
        public void FindTestTrue()
        {
            var avl = new AVLTree<int>
            {
                1,2,3,4,5
            };
            Assert.AreEqual(true, avl.Contains(1));
        }

        [Test]
        public void BalanceRightTest()
        {
            var avl = new AVLTree<int>
            {
                11,10,20,16,14,15
            };
            var actual = new AVLTree<int> { 14 };
            Assert.AreEqual(avl.Head.Value, actual.Head.Value);
        }

        [Test]
        public void BalanceLeftTest()
        {
            var avl = new AVLTree<int>
            {
                6,16,19,11,10
            };
            avl.Remove(11);
            var actual = new AVLTree<int> { 16 };
            Assert.AreEqual(avl.Head.Value, actual.Head.Value);
        }
    }
}
