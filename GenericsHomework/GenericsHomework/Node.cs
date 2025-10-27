using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsHomework;

///<summary>
/// A node that stores a homogenous value of type T and references to other nodes.
/// Participates in a circularly linked structure.
/// </summary>
public class Node<T> : ICollection<T>
{
    private T _value;
    private bool _isEmpty;

    /// <summary>
    /// Initializes a single-node circular list; Next points to this node.
    /// _isEmpty will represent an empty collection while keeping the self-loop.
    /// </summary>
    public Node(T value)
    {
        _value = value;
        _isEmpty = false;
        Next = this;
    }

    ///<summary>
    /// The next node in the circular list. If this is the only node, Next points to itself.
    /// Non-nullable per assignment guidelines; initialized in constructor.
    /// </summary>
    public Node<T> Next { get; private set; }

    /// <summary>
    /// Returns the underlying value's ToString() representation, or empty for null/empty collection.
    /// </summary>
    public override string ToString() =>
        _isEmpty ? string.Empty : (_value?.ToString() ?? string.Empty);

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

        if (_isEmpty)
        {
            _value = value;
            _isEmpty = false;
            Next = this;
            return;
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
        while (!object.ReferenceEquals(current, this))
        {
            Node<T> next = current.Next;
            // Break the link to help with garbage collection
            current.Next = current;
            current = next;
        }

        this.Next = this;
        _isEmpty = true;

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
        if (_isEmpty) return false;
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

    /// <summary>
    /// Returns the number of elements in the collection.
    /// </summary>
    public int Count
    {
        get
        {
            if (_isEmpty) return 0;
            int count = 1;
            for (Node<T> node = this.Next; !ReferenceEquals(node, this); node = node.Next) count++;
            return count;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds an item to the collection.
    /// This method is an explicit interface implementation of the ICollection<T>.Add method.
    /// </summary>
    void ICollection<T>.Add(T item) => Append(item);

    /// <summary>
    /// Determines whether the collection contains a specific value.
    /// </summary>
    public bool Contains(T item) => Exists(item);

    /// <summary>
    /// Copies the elements of the collection to the specified array, starting at the specified array index.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null) throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < Count) throw new ArgumentException("The destination array has insufficient space.");
        if (_isEmpty) return;
        array[arrayIndex++] = _value;

        if (ReferenceEquals(this.Next, this)) return;
        int appendedCount = Count - 1;
        int writeIndex = arrayIndex + appendedCount - 1;

        for (Node<T> node = this.Next; !ReferenceEquals(node, this); node = node.Next)
        {
            array[writeIndex--] = node._value;
        }
    }


    /// <summary>
    /// Removes the first occurrence of the specified item from the list.
    /// </summary>
    /// <returns>true if the item was found and removed; otherwise, false.</returns>
    public bool Remove(T item)
    {
        if (_isEmpty) return false;
        // Special case: removing the head node
        if (object.Equals(this._value, item))
        {
            if (ReferenceEquals(this.Next, this))
            {
                // Only one node in the list
                _isEmpty = true;
                return true;
            }

            var removed = this.Next;
            _value = removed._value;
            Next = removed.Next;
            return true;
            //// Copy next node's value into this node and remove next node
            //this._value = this.Next._value;
            //this.Next = this.Next.Next;
          
        }
        Node<T> previous = this;
        Node<T> current = this.Next;
        while (!ReferenceEquals(current, this))
        {
            if (object.Equals(current._value, item))
            {
                previous.Next = current.Next;
                current.Next = current;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;

    }


    /// <summary>
    /// Returns an enumerator that iterates through the elements of the collection.
    /// Enumeration starts from the current node and continues through the collection in sequence,
    /// returning to the starting node. The enumerator reflects the state of the collection at the time GetEnumerator is
    /// called.
    /// </summary>
   
    public IEnumerator<T> GetEnumerator()
    {
        if (_isEmpty) yield break;
        yield return _value;
        for (Node<T> node = this.Next; !ReferenceEquals(node, this); node = node.Next)
        {
            yield return node._value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}