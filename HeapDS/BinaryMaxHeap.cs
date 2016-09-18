using System;
using System.Collections.Generic;

namespace HeapDS
{
    public class BinaryMaxHeap<T>
    {
        private List<HeapNode<T>> allNodes = new List<HeapNode<T>>();

        public void AddNode(int w, T data)
        {
            HeapNode<T> node = new HeapNode<T>();
            node.Weight = w;
            node.Data = data;

            allNodes.Add(node);

            int size = allNodes.Count;

            int currentIndex = size - 1;

            int parentIndex = (currentIndex - 1) / 2;

            while (parentIndex >= 0)
            {
                HeapNode<T> parentNode = allNodes[parentIndex];
                HeapNode<T> currentNode = allNodes[currentIndex];

                if (parentNode.Weight < currentNode.Weight)
                {
                    Swap(parentIndex, currentIndex);
                    currentIndex = parentIndex;
                    parentIndex = (currentIndex - 1) / 2;
                }
                else
                {
                    break;
                }
            }

        }

        public HeapNode<T> MaxNode()
        {
            return allNodes[0];
        }

        public bool IsEmpty()
        {
            return allNodes.Count == 0;
        }

        public HeapNode<T> extractMax()
        {
            if (allNodes.Count == 0)
                return null;

            int li = allNodes.Count - 1;

            var maxNode = allNodes[0];

            //Copy last node data to index = 0 and remove last node
            allNodes[0] = allNodes[li];
            allNodes.RemoveAt(li);

            int ci = 0;

            int size = allNodes.Count;

            while (true)
            {
                int leftIndex = 2 * ci + 1;
                int rightIndex = 2 * ci + 2;

                if (leftIndex > size - 1)
                {
                    break;
                }

                if (rightIndex > size - 1)
                {
                    rightIndex = leftIndex;
                }

                int largerIndex = allNodes[leftIndex].Weight >= allNodes[rightIndex].Weight ? leftIndex : rightIndex;

                //Check if weight at Current Index is less than the larger Index
                if (allNodes[ci].Weight < allNodes[largerIndex].Weight)
                {
                    Swap(largerIndex, ci);
                    ci = largerIndex;
                }
                else
                {
                    break;
                }

            }

            return maxNode;


        }

        public void PrintHeap()
        {
            int c = 0;
            foreach (var n in allNodes)
            {
                Console.WriteLine(string.Format("Node at Position : {0} is : {1} with weight:{2}", c, n.Data.ToString(), n.Weight));
                c++;
            }
        }

        private void Swap(int parentIndex, int currentIndex)
        {
            var tempNode = allNodes[parentIndex];

            allNodes[parentIndex] = allNodes[currentIndex];
            allNodes[currentIndex] = tempNode;
        }
    }


}
