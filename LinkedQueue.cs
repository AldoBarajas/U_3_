namespace Unidad3.Estructuras;

/// <summary>Cola FIFO dinámica: agregar al final y extraer por el inicio, ambas O(1).</summary>
public sealed class LinkedQueue<T>
{
    private readonly SinglyLinkedList<T> lista = new();
    public int Count => lista.Count;
    public bool IsEmpty() => lista.IsEmpty;
    public void Enqueue(T value) => lista.AddLast(value);
    public T Dequeue() => lista.RemoveFirst();
    public T Peek() => lista.First();
}
