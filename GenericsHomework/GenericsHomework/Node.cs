namespace GenericsHomework;

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

    /// <summary>
    /// Appends a new node containing the specified value immediately after this node,
    /// rejecting duplicates.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a node with the same value already exists in the list.
    /// </exception>
    public void Append(T value)
    {
        if (this.Exists(value))
        {
            throw new InvalidOperationException("Duplicate value not allowed in this circular list.");
        }

        Node<T> newNode = new Node<T>(value)
        {
            Next = this.Next
        };
        this.Next = newNode;
    }

    /// <summary>
    /// Removes all items from the collection except the current node.
    /// </summary>
    public void Clear()
    {
        Node<T> current = this.Next;
        if (object.ReferenceEquals(current, this))
        {
            return;
        }

        while (!object.ReferenceEquals(current, this))
        {
            (current.Next, current) = (current, current.Next);
        }

        this.Next = this;

        // Garbage Collector note:
        // If nothing outside the linked list still references the removed nodes,
        // they’ll be collected even if they point to each other.
        // Using a self-loop ensures a removed node can’t point back into the list
    }

    /// <summary>
    /// Tests whether a value exists in the circular list.
    /// </summary>
    public bool Exists(T value)
    {
        if (object.Equals(this._value, value))
        {
            return true;
        }

        for (Node<T> node = this.Next; !object.ReferenceEquals(node, this); node = node.Next)
        {
            if (object.Equals(node._value, value))
            {
                return true;
            }
        }

        return false;
    }

}

