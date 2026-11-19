namespace Unidad3.Estructuras;

/// <summary>Cola FIFO circular con T[], front, rear y contador de elementos.</summary>
public sealed class CircularQueue<T>
{
    private readonly T[] datos;
    private int front = 0;
    private int rear = -1;
    private int count = 0;

    public CircularQueue(int capacidad)
    {
        if (capacidad <= 0) throw new ArgumentOutOfRangeException(nameof(capacidad));
        datos = new T[capacidad];
    }

    public int Capacity => datos.Length;
    public int Count => count;
    public int FrontIndex => IsEmpty() ? -1 : front;
    public int RearIndex => rear;
    public bool IsEmpty() => count == 0;
    public bool IsFull() => count == datos.Length;

    // Alias con la grafía literal indicada en la rúbrica.
    public bool isEmpty() => IsEmpty();
    public bool isFull() => IsFull();

    public void Enqueue(T valor)
    {
        if (IsFull()) throw new OverflowException("La cola circular está llena.");
        rear = (rear + 1) % datos.Length;
        datos[rear] = valor;
        count++;
    }

    public T Dequeue()
    {
        if (IsEmpty()) throw new InvalidOperationException("La cola circular está vacía.");
        T valor = datos[front];
        datos[front] = default!;
        front = (front + 1) % datos.Length;
        count--;
        if (count == 0) { front = 0; rear = -1; }
        return valor;
    }

    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("La cola circular está vacía.");
        return datos[front];
    }

    public T[] Snapshot()
    {
        var resultado = new T[count];
        for (int i = 0; i < count; i++) resultado[i] = datos[(front + i) % datos.Length];
        return resultado;
    }
}
