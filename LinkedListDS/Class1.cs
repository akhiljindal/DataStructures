namespace LinkedListDS
{
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; set; }

        public LinkedList(LinkedListNode<T> node)
        {
            Head = node;
        }

        public LinkedListNode<T> AddNodeAtLast(LinkedListNode<T> node)
        {
            var current = Head;

            while (current.Next != null)
                current = current.Next;

            current.Next = node;

            return Head;
        }
    }

    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }

    }

}
