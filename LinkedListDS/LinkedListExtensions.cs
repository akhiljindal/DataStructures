using System;
using SC = System.Collections.Generic;
namespace LinkedListDS
{
    public static class LinkedListExtensions
    {
        private static int iteration = 0;
        public static SC.LinkedListNode<T> FindKthFromLastRecursive<T>(this SC.LinkedList<T> list, int k)
        {
            iteration = 0;
            return FindKthFromLastRecursive<T>(list.First, ref k);
        }

        private static SC.LinkedListNode<T> FindKthFromLastRecursive<T>(SC.LinkedListNode<T> node, ref int k)
        {
            // string s = string.Format("Iteration :{0} :: Node:{1}", iteration, node ?? node.Value);
            Console.WriteLine();
            if (node == (SC.LinkedListNode<T>)null)
                return null;

            var result = FindKthFromLastRecursive(node.Next, ref k);
            k--;

            if (k == 0)
            {
                return node;
            }

            return result;
        }
    }
}
