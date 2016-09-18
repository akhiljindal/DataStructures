using System.Collections.Generic;

namespace TreesDS
{
    public class BinaryTreeNode<T> 
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> LeftNode { get; set; }
        public BinaryTreeNode<T> RightNode { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
            LeftNode = null;
            RightNode = null;
        }

            
        public static int Compare(T x, T y)
        {
            return Comparer<T>.Default.Compare(x, y);

        }

    }
}