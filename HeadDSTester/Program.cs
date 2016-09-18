using HeapDS;
using System;

namespace HeapDSTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestBinaryMaxHeap();

            TestBinaryMinHeap();
        }

        private static void TestBinaryMaxHeap()
        {
            BinaryMaxHeap<String> heap = new BinaryMaxHeap<String>();
            heap.AddNode(3, "Tushar");
            heap.AddNode(4, "Ani");
            heap.AddNode(8, "Vijay");
            heap.AddNode(10, "Pramila");
            heap.AddNode(5, "Roy");
            heap.AddNode(6, "NTF");
            heap.PrintHeap();



            var node = heap.extractMax();
            while (node != null)
            {
                Console.WriteLine("Max Node extracted is :" + node.Data + " " + node.Weight);
                heap.PrintHeap();

                node = heap.extractMax();
            }
        }

        private static void TestBinaryMinHeap()
        {
            BinaryMinHeap<String> heap = new BinaryMinHeap<String>();
            heap.AddNode(3, "Tushar");
            heap.AddNode(4, "Ani");
            heap.AddNode(8, "Vijay");
            heap.AddNode(10, "Pramila");
            heap.AddNode(5, "Roy");
            heap.AddNode(6, "NTF");
            heap.PrintHeap();

            heap.Decrease("Pramila", 1);


            heap.Decrease("Vijay", 1);
            heap.Decrease("Ani", 11);
            heap.Decrease("NTF", 4);

            heap.PrintHeap();

            var node = heap.extractMin();
            while (node != null)
            {
                Console.WriteLine("Min Node extracted is :" + node.Data + " " + node.Weight);
                heap.PrintHeap();

                node = heap.extractMin();
            }
        }
    }
}
