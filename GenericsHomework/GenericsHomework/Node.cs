namespace GenericsHomework
{

    ///<summary>
    /// A node that stores a homogenous value of type T and references to other nodes.
    /// Participates in a circularly linked structure.
    /// </summary>
    public class Node<T>
    {
        private readonly T _value;

        /// <summary>
        /// Initializes a single-node circular list; Next points to this node.
        /// </summary>
        public Node(T value)
        {
            _value = value;
            Next = this;
        }

        ///<summary>
        /// The next node in the circular list. If this is the only node, Next points to itself.
        /// Non-nullable per assignment guidelines; initialized in constructor.
        /// </summary>
        public Node<T> Next { get; private set; }


        /// <summary>
        /// Returns the underlying value's ToString() representation.
        /// </summary>
        public override string ToString() 
            => _value?.ToString() ?? string.Empty;


        /// Stubs for methods to be implemented.
        public void Append(T value)
        {
            Node<T> newNode = new Node<T>(value);
            newNode.Next = this.Next;
            this.Next = newNode;
        }



        public bool Exists(T value) => throw new NotImplementedException();
        public void Clear() => throw new NotImplementedException();


    }
}
