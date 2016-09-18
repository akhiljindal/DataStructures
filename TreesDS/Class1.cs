namespace TreesDS
{
    public class BinarySearchTree<T>
    {
        public int Count { get; private set; }

        public BinaryTreeNode<T> Root { get; private set; }

        public BinarySearchTree(T data)
        {
            Root = new BinaryTreeNode<T>(data);
            Count++;
        }


        public bool AddNode(T data)
        {
            BinaryTreeNode<T> node = Root;

            while (true)
            {
                if (BinaryTreeNode<T>.Compare(data, node.Data) > 0)
                {
                    if (node.RightNode != null)
                    {
                        node = node.RightNode;
                    }
                    else
                    {
                        node.RightNode = new BinaryTreeNode<T>(data);
                        break;
                    }
                    //Go right

                }
                else
                {
                    if (node.LeftNode != null)
                    {
                        node = node.LeftNode;
                    }
                    else
                    {
                        node.LeftNode = new BinaryTreeNode<T>(data);
                        break;
                    }
                }
            }

            Count++;
            return true;
        }

        public BinaryTreeNode<T> AddNode2(T data)
        {
            var nodetobeInserted = new BinaryTreeNode<T>(data);

            var parentNode = Root;
            var current = Root;

            //Find the position where the node has to be inserted
            while (current != null)
            {
                parentNode = current;
                if (BinaryTreeNode<T>.Compare(data, current.Data) > 0)
                {

                    current = current.RightNode;
                }
                else
                {
                    current = current.LeftNode;
                }
            }

            if (BinaryTreeNode<T>.Compare(data, parentNode.Data) > 0)
            {
                parentNode.RightNode = nodetobeInserted;
            }
            else
            {
                parentNode.LeftNode = nodetobeInserted;
            }

            Count++;
            return Root;
        }
    }
}
