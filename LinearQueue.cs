namespace Unidad3.Estructuras;

/// <summary>
/// Cola simple FIFO con arreglo fijo. No recicla posiciones liberadas en el frente
/// mientras queden elementos; se reinician índices al vaciarla por completo.
/// </summary>
public sealed class LinearQueue<T>
{
    private readonly T[] datos;
    private int front = 0;
    private int rear = -1;

    public LinearQueue(int capacidad)
    {
        if (capacidad <= 0) throw new ArgumentOutOfRangeException(nameof(capacidad));
        datos = new T[capacidad];
    }

    public int Capacity => datos.Length;
    public int FrontIndex => IsEmpty() ? -1 : front;
    public int RearIndex => rear;
    public int Count => rear < front ? 0 : rear - front + 1;
    public bool IsEmpty() => rear < front;
    // Está llena cuando rear alcanzó el extremo físico, incluso si ya se retiró del frente.
    public bool IsFull() => rear == datos.Length - 1;

    // Alias con la grafía literal indicada en la rúbrica.
    public bool isEmpty() => IsEmpty();
    public bool isFull() => IsFull();

    public void Enqueue(T valor)
    {
        if (IsFull()) throw new OverflowException("Fin del arreglo; no hay espacio al final.");
        datos[++rear] = valor;
    }

    public T Dequeue()
    {
        if (IsEmpty()) throw new InvalidOperationException("La cola simple está vacía.");
        T valor = datos[front];
        datos[front++] = default!;
        if (front > rear) { front = 0; rear = -1; }
        return valor;
    }

    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("La cola simple está vacía.");
        return datos[front];
    }

    public T[] Snapshot()
    {
        var resultado = new T[Count];
        for (int i = 0; i < resultado.Length; i++) resultado[i] = datos[front + i];
        return resultado;
    }
}
