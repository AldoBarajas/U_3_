using System.Collections;
using System.Collections.Generic;

namespace Unidad3.Estructuras;

/// <summary>Lista simplemente enlazada sin usar List&lt;T&gt; para el almacenamiento.</summary>
public sealed class SinglyLinkedList<T> : IEnumerable<T>
{
    private sealed class Node
    {
        public T Value;
        public Node? Next;
        public Node(T value) { Value = value; }
    }

    private Node? head;
    private Node? tail;
    public int Count { get; private set; }
    public bool IsEmpty => head is null;

    public void AddFirst(T value)
    {
        var nuevo = new Node(value) { Next = head };
        head = nuevo;
        if (tail is null) tail = nuevo;
        Count++;
    }

    public void AddLast(T value)
    {
        var nuevo = new Node(value);
        if (tail is null) head = nuevo;
        else tail.Next = nuevo;
        tail = nuevo;
        Count++;
    }

    public T First()
    {
        if (head is null) throw new InvalidOperationException("Lista vacía.");
        return head.Value;
    }

    public T RemoveFirst()
    {
        if (head is null) throw new InvalidOperationException("Lista vacía.");
        T value = head.Value;
        head = head.Next;
        Count--;
        if (head is null) tail = null;
        return value;
    }

    public bool Contains(T value)
    {
        for (Node? actual = head; actual is not null; actual = actual.Next)
            if (EqualityComparer<T>.Default.Equals(actual.Value, value)) return true;
        return false;
    }

    public bool Remove(T value)
    {
        Node? previo = null;
        Node? actual = head;
        while (actual is not null)
        {
            if (EqualityComparer<T>.Default.Equals(actual.Value, value))
            {
                if (previo is null) head = actual.Next;
                else previo.Next = actual.Next;
                if (actual == tail) tail = previo;
                Count--;
                return true;
            }
            previo = actual;
            actual = actual.Next;
        }
        return false;
    }

    public void Clear() { head = null; tail = null; Count = 0; }

    public IEnumerator<T> GetEnumerator()
    {
        for (Node? actual = head; actual is not null; actual = actual.Next)
            yield return actual.Value;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
