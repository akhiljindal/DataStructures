using System;
using System.Collections;
using System.Collections.Generic;

namespace TreesDS
{
    public static class BSTExtension
    {
        public static T GetKthSmallestNode<T>(this BinarySearchTree<T> bst, int k)
        {
            if (bst == null)
                return default(T);

            int count = 0;
            T result = default(T);
            GetKthSmallestNode<T>(bst.Root, k, ref count, ref result);
            return result;
        }

        private static void GetKthSmallestNode<T>(BinaryTreeNode<T> root, int k, ref int count, ref T result)
        {
            if (root == null)
                return;

            GetKthSmallestNode(root.LeftNode, k, ref count, ref result);


            count++;

            if (count == k)
            {
                result = root.Data;
            }
            GetKthSmallestNode(root.RightNode, k, ref count, ref result);

        }


        public static T GetKthLargestNode<T>(this BinarySearchTree<T> bst, int k)
        {
            if (bst == null)
                return default(T);

            int count = 0;
            T result = default(T);
            GetKthLargestNode<T>(bst.Root, k, ref count, ref result);
            return result;
        }

        private static void GetKthLargestNode<T>(BinaryTreeNode<T> root, int k, ref int count, ref T result)
        {
            if (root == null)
                return;

            GetKthLargestNode(root.RightNode, k, ref count, ref result);


            count++;

            if (count == k)
            {
                result = root.Data;
            }
            GetKthLargestNode(root.LeftNode, k, ref count, ref result);

        }
        public static T GetKthNodeUsingStack<T>(this BinarySearchTree<T> bst, int k)
        {
            if (bst == null)
                return default(T);

            int count = 0;
            Stack<BinaryTreeNode<T>> s = new Stack<BinaryTreeNode<T>>();
            var node = bst.Root;

            while (true)
            {
                if (node != null)
                {
                    s.Push(node);
                    node = node.LeftNode;
                }
                else
                {
                    if (s.Count == 0)
                        break;

                    node = s.Pop();
                    count++;

                    Console.Write(node.Data + "->");

                    if (count == k)
                    {
                        Console.WriteLine("Found kth element -> " + node.Data);
                        return node.Data;
                    }

                    node = node.RightNode;
                }
            }

            return default(T);

        }


        public static void PreOrderIter<T>(this BinarySearchTree<T> bst)
        {
            if (bst == null || bst.Root == null)
                return;


            Stack<BinaryTreeNode<T>> s = new Stack<BinaryTreeNode<T>>();
            var node = bst.Root;

            s.Push(node);

            while (s.Count != 0)
            {
                node = s.Pop();
                Console.Write(node.Data + "-");

                if (node.RightNode != null)
                    s.Push(node.RightNode);

                if (node.LeftNode != null)
                    s.Push(node.LeftNode);

            }
        }

        public static void InOrderIter<T>(this BinarySearchTree<T> bst)
        {
            if (bst == null || bst.Root == null)
                return;


            Stack<BinaryTreeNode<T>> s = new Stack<BinaryTreeNode<T>>();
            var node = bst.Root;

            while (node != null)
            {
                s.Push(node);
                node = node.LeftNode;
            }

            while (s.Count != 0)
            {
                node = s.Pop();

                Console.WriteLine(node.Data + "-");

                if (node.RightNode != null)
                {
                    node = node.RightNode;

                    while (node != null)
                    {
                        s.Push(node);
                        node = node.LeftNode;
                    }
                }

            }
        }

        public static int SizeOfTree<T>(this BinarySearchTree<T> bst)
        {
            return SizeCore(bst.Root);
        }

        private static int SizeCore<T>(BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }

            return SizeCore(root.LeftNode) + SizeCore<T>(root.RightNode) + 1;
        }

        public static bool IsBst(this BinarySearchTree<int> bst)
        {
            return IsBstCore(bst.Root, Int32.MinValue, Int32.MaxValue);
        }

        private static bool IsBstCore(BinaryTreeNode<int> root, int minValue, int maxValue)
        {
            if (root == null)
                return true;

            if (root.Data < minValue || root.Data > maxValue)
                return false;

            return IsBstCore(root.LeftNode, minValue, root.Data) && IsBstCore(root.RightNode, root.Data, maxValue);
        }

        public static int LargestBst(this BinarySearchTree<int> bst)
        {
            return LargestBstCore(bst.Root);
        }

        private static int LargestBstCore(BinaryTreeNode<int> root)
        {
            if (IsBstCore(root, Int32.MinValue, Int32.MaxValue))
            {
                return SizeCore(root);
            }
            else
            {
                return Math.Max(LargestBstCore(root.LeftNode), LargestBstCore(root.RightNode));
            }

        }


        /// <summary>
        /// http://www.geeksforgeeks.org/inorder-predecessor-successor-given-key-bst/
        /// // This function finds predecessor and successor of key in BST.
            // It sets pre and suc as predecessor and successor respectively    
        /// </summary>
        /// <param name="bst"></param>
        public static int FindInOrderSuccessorPredecessor(this BinarySearchTree<int> bst, int key, ref int pred)
        {
            int sucessor = Int32.MaxValue;

            FindInOrderSuccessorPredecessorCore(bst.Root, key, ref sucessor, ref pred);
            return sucessor;
        }

        private static void FindInOrderSuccessorPredecessorCore(BinaryTreeNode<int> root, int key, ref int sucessor, ref int pred)
        {
            if (root == null)
                return;

            //As soon as the key is found, we need to find inorder successor and predecessor
            if (root.Data == key)
            {
                //Succssor i.e the next node that will come after key in the inorder travesal
                //In order gives us the nodes in sorted order
                // So successor will be the smallest node in the right subtree of root
                if (root.RightNode != null)
                {
                    var tmp = root.RightNode;

                    while (tmp.LeftNode != null)
                        tmp = tmp.LeftNode;

                    sucessor = tmp.Data;
                }

                //Similary pred is the largest in the left side
                //Largest is on the rightmost side of root.left
                if (root.LeftNode != null)
                {
                    var tmp = root.LeftNode;

                    while (tmp.RightNode != null)
                        tmp = tmp.RightNode;

                    pred = tmp.Data;
                }

                return;
            }

            //If we reached here there has been no match found yet. So let's go in the correct direction
            if (root.Data > key) //look left
            {
                sucessor = root.Data;
                FindInOrderSuccessorPredecessorCore(root.LeftNode, key, ref sucessor, ref pred);
            }
            else //look right
            {
                pred = root.Data;
                FindInOrderSuccessorPredecessorCore(root.RightNode, key, ref sucessor, ref pred);
            }
        }

        public static int GetLevelOfNode<T>(this BinarySearchTree<T> bst, T key)
        {
            if (bst == null)
                return -1;

            Queue<BinaryTreeNode<T>> q = new Queue<BinaryTreeNode<T>>();
            q.Enqueue(bst.Root);
            int level = 1;
            q.Enqueue(null);

            while (q.Count != 0)
            {
                var n = q.Dequeue();

                if (n != null && n.Data.Equals(key))
                {
                    return level;
                }

                if (n != null && n.LeftNode != null)
                {
                    q.Enqueue(n.LeftNode);
                }

                if (n != null && n.RightNode != null)
                {
                    q.Enqueue(n.RightNode);
                }

                if (n == null && q.Count > 0)
                {
                    level++;
                    q.Enqueue(null);
                }
            }

            return -1;
        }

        /// <summary>
        /// http://www.geeksforgeeks.org/print-cousins-of-a-given-node-in-binary-tree/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bst"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void GetCounsinofKey<T>(this BinarySearchTree<T> bst, T key)
        {
            if (bst == null)
                return;

            int levelOfNode = bst.GetLevelOfNode(key);

            if (levelOfNode == -1 || levelOfNode == 1)
                return;


            Queue<BinaryTreeNode<T>> q = new Queue<BinaryTreeNode<T>>();
            q.Enqueue(bst.Root);
            int level = 1;
            q.Enqueue(null);


            while (q.Count != 0)
            {
                var n = q.Dequeue();


                if (level == levelOfNode - 1)
                {
                    if (n != null && ((n.LeftNode != null && n.LeftNode.Data.Equals(key)) || (n.RightNode != null && n.RightNode.Data.Equals(key))))
                    {
                        //Skip
                        continue;
                    }
                    else
                    {
                        if (n != null && n.RightNode != null)
                        {
                            Console.WriteLine(n.RightNode.Data);
                        }

                        if (n != null && n.LeftNode != null)
                        {
                            Console.WriteLine(n.LeftNode.Data);
                        }
                    }
                }
                else
                {
                    if (n != null && n.LeftNode != null)
                    {
                        q.Enqueue(n.LeftNode);
                    }

                    if (n != null && n.RightNode != null)
                    {
                        q.Enqueue(n.RightNode);
                    }
                }

                if (n == null && q.Count > 0 && levelOfNode != level)
                {
                    level++;
                    q.Enqueue(null);
                    continue;
                }
            }


        }

        public static BinaryTreeNode<int> ConstrucTreefromInorderPostOrder(int[] inOrder, int[] post)
        {
            if (inOrder.Length != post.Length)
                return null;


            int len = post.Length;
            int pIndex = len - 1;

            return ConstrucTreefromInorderPostOrderCore(inOrder, post, 0, len - 1, ref pIndex);
        }

        private static BinaryTreeNode<int> ConstrucTreefromInorderPostOrderCore(int[] inOrder, int[] post, int iStart, int iEnd, ref int pIndex)
        {
            if (iStart > iEnd || pIndex < 0)
                return null;

            // The last node in Post Order has the root
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(post[pIndex]);
            pIndex--;

            // There are no more left and right to be explored.. So return the node
            if (iStart == iEnd)
                return node;


            // We need to search the value @pIndex of post in the inorder arrray. That will be the split point
            int iIndex = SearchinInorder(inOrder, iStart, iEnd, node.Data);

            //Once the index is found, split the inOrder list into left and right. Start from right first
            node.RightNode = ConstrucTreefromInorderPostOrderCore(inOrder, post, iIndex + 1, iEnd, ref pIndex);
            node.LeftNode = ConstrucTreefromInorderPostOrderCore(inOrder, post, iStart, iIndex - 1, ref pIndex);

            return node;

        }

        private static int SearchinInorder(int[] inOrder, int iStart, int iEnd, int data)
        {
            int i;
            for (i = iStart; i <= iEnd; i++)
            {
                if (inOrder[i] == data)
                    break;
            }
            return i;
        }


        public static List<List<BinaryTreeNode<T>>> GetNodeListByLevel<T>(this BinarySearchTree<T> bst)
        {
            if (bst == null || bst.Root == null)
                return null;

            List<List<BinaryTreeNode<T>>> result = new List<List<BinaryTreeNode<T>>>();
            List<BinaryTreeNode<T>> list = new List<BinaryTreeNode<T>>();
            Queue<BinaryTreeNode<T>> q = new Queue<BinaryTreeNode<T>>();
            q.Enqueue(bst.Root);
            q.Enqueue(null);

            while (q.Count != 0)
            {
                var current = q.Dequeue();
                if (current == null)
                {
                    result.Add(list);
                    list = new List<BinaryTreeNode<T>>();
                    if (q.Count > 0)
                        q.Enqueue(null);

                    continue;
                }

                list.Add(current);

                if (current != null && current.LeftNode != null)
                    q.Enqueue(current.LeftNode);

                if (current != null && current.RightNode != null)
                    q.Enqueue(current.RightNode);



            }

            return result;
        }

        public static ArrayList GetNodeListByLevelUsingDFS<T>(this BinarySearchTree<T> bst)
        {
            if (bst == null || bst.Root == null)
                return null;

            int level = 1;
            ArrayList result = new ArrayList();
            GetNodeListByLevelUsingDFSCore(bst.Root, level, result);
            return result;


        }

        private static void GetNodeListByLevelUsingDFSCore<T>(BinaryTreeNode<T> root, int level, ArrayList result)
        {
            if (root == null)
                return;

            List<BinaryTreeNode<T>> list = null;
            if (result.Count < level)
            {
                list = new List<BinaryTreeNode<T>>();
                result.Add(list);
            }
            else
            {
                list = result[level - 1] as List<BinaryTreeNode<T>>;
            }

            if (list != null) list.Add(root);

            GetNodeListByLevelUsingDFSCore(root.LeftNode, level + 1, result);
            GetNodeListByLevelUsingDFSCore(root.RightNode, level + 1, result);
        }

        public static void CreateMirrorOfTree<T>(this BinarySearchTree<T> bst)
        {
            CreateMirrorCore(bst.Root);
        }

        private static void CreateMirrorCore<T>(BinaryTreeNode<T> root)
        {
            if (root == null)
                return;

            CreateMirrorCore<T>(root.LeftNode);
            CreateMirrorCore<T>(root.RightNode);

            var temp = root.LeftNode;
            root.LeftNode = root.RightNode;
            root.RightNode = temp;
        }

        public static BinaryTreeNode<T> GetLCA<T>(this BinarySearchTree<T> bst, T n1,
            T n2)
        {
            if (bst == null || n1 == null || n2 == null || bst.Root == null)
                return null;

            return GetLCACore(bst.Root, n1, n2);
        }

        private static BinaryTreeNode<T> GetLCACore<T>(BinaryTreeNode<T> root, T n1, T n2)
        {
            if (root == null)
                return null;

            if (root.Data.Equals(n1) || root.Data.Equals(n2))
            {
                return root;
            }

            var left = GetLCACore(root.LeftNode, n1, n2);
            var right = GetLCACore(root.RightNode, n1, n2);

            if (left != null && right != null)
            {
                return root;
            }

            return left ?? right;
        }
    }//class
}