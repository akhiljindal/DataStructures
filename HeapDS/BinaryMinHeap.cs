using System;
using System.Collections.Generic;

namespace HeapDS
{
    public class BinaryMinHeap<T>
    {
        private List<HeapNode<T>> allNodes = new List<HeapNode<T>>();
        private Dictionary<T, int> nodeMap = new Dictionary<T, int>();

        public void AddNode(int w, T data)
        {
            HeapNode<T> node = new HeapNode<T>();
            node.Weight = w;
            node.Data = data;

            allNodes.Add(node);

            int size = allNodes.Count;

            int currentIndex = size - 1;

            nodeMap.Add(node.Data, currentIndex);

            HeapifyNode(currentIndex);
        }

        private void HeapifyNode(int currentIndex)
        {
            int parentIndex = (currentIndex - 1) / 2;

            while (parentIndex >= 0)
            {
                HeapNode<T> parentNode = allNodes[parentIndex];
                HeapNode<T> currentNode = allNodes[currentIndex];

                if (parentNode.Weight > currentNode.Weight)
                {
                    SwapandUpdate(parentIndex, currentIndex);
                    currentIndex = parentIndex;
                    parentIndex = (currentIndex - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }

        public HeapNode<T> MinNode()
        {
            return allNodes[0];
        }

        public bool IsEmpty()
        {
            return allNodes.Count == 0;
        }

        public HeapNode<T> extractMin()
        {
            if (allNodes.Count == 0)
                return null;

            int li = allNodes.Count - 1;

            var minNode = allNodes[0];

            //Copy last node data to index = 0 and remove last node
            allNodes[0] = allNodes[li];
            allNodes.RemoveAt(li);

            //Remove extracted Min Node and update last node as 0
            nodeMap.Remove(minNode.Data);
            if (allNodes.Count > 0)
                nodeMap[allNodes[0].Data] = 0;

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

                int smallestIndex = allNodes[leftIndex].Weight <= allNodes[rightIndex].Weight ? leftIndex : rightIndex;

                //Check if weight at Current Index is less than the larger Index
                if (allNodes[ci].Weight > allNodes[smallestIndex].Weight)
                {
                    SwapandUpdate(smallestIndex, ci);
                    ci = smallestIndex;
                }
                else
                {
                    break;
                }

            }

            return minNode;


        }

        public bool ContainsData(T key)
        {
            return nodeMap.ContainsKey(key);
        }

        public int GetWeight(T key)
        {
            return allNodes[nodeMap[key]].Weight;
        }

        public void Decrease(T data, int newWeight)
        {
            int currentPosition = nodeMap[data];
            if (allNodes[currentPosition].Weight <= newWeight)
                return;

            allNodes[currentPosition].Weight = newWeight;


            //Since the weight has been updated we need to heapify so the node lands in the correct positon and also update heap
            HeapifyNode(currentPosition);
        }

        public void PrintHeap()
        {
            int c = 0;
            foreach (var n in allNodes)
            {
                Console.WriteLine(string.Format("Node at Position : {0} is : {1} with weight:{2}", c, n.Data.ToString(), n.Weight));
                c++;
            }

            Console.WriteLine("Priniting Map....\n");

            foreach (var n in nodeMap)
            {
                Console.WriteLine(n.Key + ":" + n.Value);
            }
        }

        private void SwapandUpdate(int parentIndex, int currentIndex)
        {
            var tempNode = allNodes[parentIndex];

            allNodes[parentIndex] = allNodes[currentIndex];
            allNodes[currentIndex] = tempNode;

            nodeMap[allNodes[parentIndex].Data] = parentIndex;
            nodeMap[allNodes[currentIndex].Data] = currentIndex;

        }
    }
}
