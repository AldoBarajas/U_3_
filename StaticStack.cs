namespace Unidad3.Estructuras;

/// <summary>Pila LIFO de capacidad fija, implementada exclusivamente mediante T[].</summary>
public sealed class StaticStack<T>
{
    private readonly T[] datos;
    private int top = -1;

    public StaticStack(int capacidad)
    {
        if (capacidad <= 0) throw new ArgumentOutOfRangeException(nameof(capacidad));
        datos = new T[capacidad];
    }

    public int Capacity => datos.Length;
    public int Count => top + 1;
    public int TopIndex => top;
    public bool IsEmpty() => top == -1;
    public bool IsFull() => top == datos.Length - 1;

    // Alias con la grafía literal indicada en la rúbrica.
    public bool isEmpty() => IsEmpty();
    public bool isFull() => IsFull();

    /// <summary>Inserta al tope en O(1). Lanza OverflowException si está llena.</summary>
    public void Push(T valor)
    {
        if (IsFull()) throw new OverflowException("La pila está llena.");
        datos[++top] = valor;
    }

    /// <summary>Retira el último elemento en O(1).</summary>
    public T Pop()
    {
        if (IsEmpty()) throw new InvalidOperationException("La pila está vacía.");
        T valor = datos[top];
        datos[top--] = default!; // libera referencia si T es un objeto
        return valor;
    }

    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("La pila está vacía.");
        return datos[top];
    }

    /// <summary>Instantánea desde la base hasta el tope, solo para visualización.</summary>
    public T[] Snapshot()
    {
        var resultado = new T[Count];
        for (int i = 0; i < Count; i++) resultado[i] = datos[i];
        return resultado;
    }
}
