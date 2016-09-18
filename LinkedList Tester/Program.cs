using LinkedListDS;

namespace LinkedList_Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Collections.Generic.LinkedList<int> list = new System.Collections.Generic.LinkedList<int>();
            list.AddFirst(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            var result = list.FindKthFromLastRecursive(2);

        }
    }
}
