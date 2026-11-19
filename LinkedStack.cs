namespace Unidad3.Estructuras;

/// <summary>Pila LIFO dinámica construida sobre una lista propia de nodos.</summary>
public sealed class LinkedStack<T>
{
    private readonly SinglyLinkedList<T> lista = new();
    public int Count => lista.Count;
    public bool IsEmpty() => lista.IsEmpty;
    public void Push(T value) => lista.AddFirst(value);
    public T Pop() => lista.RemoveFirst();
    public T Peek() => lista.First();
}
