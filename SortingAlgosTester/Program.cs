using SortingAlgos;

namespace SortingAlgosTester
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = { 11, 19, 0, -1, 5, 6, 16, -3, 6, 0, 14, 18, 7, 21, 18, -6, -8 };
            QuickSort.sort(A, 0, A.Length - 1);

        }
    }
}
