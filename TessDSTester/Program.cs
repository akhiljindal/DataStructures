using System;
using TreesDS;

namespace TreeDSTester
{
    class Program
    {
        static void Main(string[] args)
        {


            BinarySearchTree<int> bst = new BinarySearchTree<int>(16);

            bst.AddNode(8);
            bst.AddNode(18);

            bst.AddNode(30);
            bst.AddNode(2);

            bst.AddNode2(10);
            bst.AddNode2(21);
            bst.AddNode2(9);

            bst.AddNode2(1);
            bst.AddNode2(3);
            bst.AddNode2(32);
            //bst.AddNode2(9);

            //int p = 0;

            //Console.WriteLine(bst.FindInOrderSuccessorPredecessor(15, ref p));
            //Console.WriteLine(p);

            //Console.WriteLine(bst.GetLevelOfNode(2));
            //Console.WriteLine(bst.GetLevelOfNode(16));
            //Console.WriteLine(bst.GetLevelOfNode(2));
            //Console.WriteLine(bst.GetLevelOfNode(1));
            //Console.WriteLine(bst.GetLevelOfNode(9));
            //Console.WriteLine(bst.GetLevelOfNode(30));
            //Console.WriteLine(bst.GetLevelOfNode(21));

            bst.GetCounsinofKey(32);

            int[] i = { 4, 8, 2, 5, 1, 6, 3, 7 };
            int[] p = { 8, 4, 5, 2, 6, 7, 3, 1 };

            var result = BSTExtension.ConstrucTreefromInorderPostOrder(i, p);

            var listDepthWise = bst.GetNodeListByLevel();
            var listDepthWiseDFS = bst.GetNodeListByLevelUsingDFS();
            Console.ReadLine();
        }
    }
}

