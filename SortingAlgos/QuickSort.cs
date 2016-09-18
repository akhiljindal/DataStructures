namespace SortingAlgos
{
    public class QuickSort
    {

        public static void sort(int[] A, int low, int high)
        {
            if (low >= high)
            {
                return;
            }
            int pos = split1(A, low, high);
            sort(A, low, pos - 1);
            sort(A, pos + 1, high);
        }

        private static int split1(int[] A, int low, int high)
        {

            int pivot = low;
            int i = low + 1;
            int j = high;
            while (i <= j)
            {

                if (A[i] <= A[pivot])
                {
                    i++;
                    continue;
                }
                if (A[j] > A[pivot])
                {
                    j--;
                    continue;
                }

                swap(A, i++, j--);
            }
            if (A[pivot] > A[j])
            {
                swap(A, pivot, j);
                return j;
            }
            return pivot;

        }


        private static void swap(int[] A, int i, int j)
        {
            int temp = A[i];
            A[i] = A[j];
            A[j] = temp;
        }

    }
}
