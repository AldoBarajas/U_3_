using Unidad3.Estructuras;

internal static class Program
{
    private static int ejecutadas;
    private static int fallidas;
    private static readonly DateTime Fecha = new(2026, 11, 8, 18, 0, 0);

    private static void Run(string nombre, Action prueba)
    {
        ejecutadas++;
        try { prueba(); Console.WriteLine($"OK   {ejecutadas:D2} | {nombre}"); }
        catch (Exception ex)
        {
            fallidas++;
            Console.WriteLine($"FAIL {ejecutadas:D2} | {nombre} | {ex.GetType().Name}: {ex.Message}");
        }
    }

    private static void Equal<T>(T esperado, T real)
    {
        if (!EqualityComparer<T>.Default.Equals(esperado, real))
            throw new Exception($"Se esperaba '{esperado}', recibido '{real}'.");
    }

    private static void True(bool condicion)
    {
        if (!condicion) throw new Exception("Se esperaba verdadero.");
    }

    private static void Throws<TError>(Action accion) where TError : Exception
    {
        try { accion(); }
        catch (TError) { return; }
        throw new Exception($"Se esperaba {typeof(TError).Name}.");
    }

    private static ClienteTurno Cliente(int folio, TipoTurno tipo) =>
        new(folio, "Cliente " + folio, 2, tipo, Fecha.AddMinutes(folio));

    private static int Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("PRUEBAS AUTOMÁTICAS | UNIDAD 3 ESTRUCTURAS LINEALES");

        Run("Pila: vacía al inicializar", () => True(new StaticStack<int>(2).IsEmpty()));
        Run("Pila: Push/Peek", () => { var s = new StaticStack<int>(2); s.Push(1); s.Push(2); Equal(2, s.Peek()); });
        Run("Pila: LIFO", () => { var s = new StaticStack<int>(2); s.Push(1); s.Push(2); Equal(2, s.Pop()); Equal(1, s.Pop()); });
        Run("Pila: llena", () => { var s = new StaticStack<int>(1); s.Push(5); True(s.IsFull()); });
        Run("Pila: desbordamiento", () => { var s = new StaticStack<int>(1); s.Push(5); Throws<OverflowException>(() => s.Push(6)); });
        Run("Pila: subdesbordamiento", () => Throws<InvalidOperationException>(() => new StaticStack<int>(1).Pop()));
        Run("Pila: capacidad inválida", () => Throws<ArgumentOutOfRangeException>(() => new StaticStack<int>(0)));
        Run("Pila: índice top", () => { var s = new StaticStack<int>(2); Equal(-1, s.TopIndex); s.Push(4); Equal(0, s.TopIndex); });

        Run("Cola simple: vacía", () => True(new LinearQueue<int>(2).IsEmpty()));
        Run("Cola simple: FIFO", () => { var q = new LinearQueue<int>(2); q.Enqueue(1); q.Enqueue(2); Equal(1, q.Dequeue()); Equal(2, q.Dequeue()); });
        Run("Cola simple: Peek", () => { var q = new LinearQueue<int>(2); q.Enqueue(9); Equal(9, q.Peek()); });
        Run("Cola simple: no recicla frontal ocupada", () => { var q = new LinearQueue<int>(2); q.Enqueue(1); q.Enqueue(2); q.Dequeue(); True(q.IsFull()); Throws<OverflowException>(() => q.Enqueue(3)); });
        Run("Cola simple: reinicia al vaciar", () => { var q = new LinearQueue<int>(1); q.Enqueue(1); q.Dequeue(); q.Enqueue(2); Equal(2, q.Dequeue()); });
        Run("Cola simple: vacía -> error", () => Throws<InvalidOperationException>(() => new LinearQueue<int>(2).Dequeue()));
        Run("Cola simple: índices", () => { var q = new LinearQueue<int>(3); q.Enqueue(1); q.Enqueue(2); q.Dequeue(); Equal(1, q.FrontIndex); Equal(1, q.RearIndex); });

        Run("Circular: FIFO", () => { var q = new CircularQueue<int>(3); q.Enqueue(1); q.Enqueue(2); Equal(1, q.Dequeue()); Equal(2, q.Dequeue()); });
        Run("Circular: reutiliza celda inicial", () => { var q = new CircularQueue<int>(3); q.Enqueue(1); q.Enqueue(2); q.Enqueue(3); q.Dequeue(); q.Enqueue(4); Equal("2,3,4", string.Join(",", q.Snapshot())); });
        Run("Circular: llena", () => { var q = new CircularQueue<int>(1); q.Enqueue(1); True(q.IsFull()); });
        Run("Circular: desbordamiento", () => { var q = new CircularQueue<int>(1); q.Enqueue(1); Throws<OverflowException>(() => q.Enqueue(2)); });
        Run("Circular: vacía -> error", () => Throws<InvalidOperationException>(() => new CircularQueue<int>(1).Peek()));
        Run("Circular: reinicio y reutilización", () => { var q = new CircularQueue<int>(1); q.Enqueue(5); q.Dequeue(); Equal(-1, q.RearIndex); q.Enqueue(8); Equal(8, q.Peek()); });

        Run("Lista: inserciones por ambos extremos", () => { var l = new SinglyLinkedList<int>(); l.AddLast(2); l.AddFirst(1); Equal("1,2", string.Join(",", l)); });
        Run("Lista: eliminar cabeza", () => { var l = new SinglyLinkedList<int>(); l.AddLast(1); l.AddLast(2); Equal(1, l.RemoveFirst()); Equal(2, l.First()); });
        Run("Lista: eliminar cola", () => { var l = new SinglyLinkedList<int>(); l.AddLast(1); l.AddLast(2); True(l.Remove(2)); Equal(1, l.First()); });
        Run("Lista: eliminar único nodo", () => { var l = new SinglyLinkedList<int>(); l.AddLast(1); True(l.Remove(1)); True(l.IsEmpty); l.AddLast(2); Equal(2, l.First()); });
        Run("Lista: Contains y ausentes", () => { var l = new SinglyLinkedList<string>(); l.AddFirst("A"); True(l.Contains("A")); True(!l.Remove("B")); });
        Run("Lista: Clear", () => { var l = new SinglyLinkedList<int>(); l.AddLast(1); l.Clear(); Equal(0, l.Count); });
        Run("Lista: error al vaciar", () => Throws<InvalidOperationException>(() => new SinglyLinkedList<int>().RemoveFirst()));
        Run("Pila dinámica: LIFO", () => { var p = new LinkedStack<int>(); p.Push(1); p.Push(2); Equal(2, p.Pop()); Equal(1, p.Pop()); });
        Run("Cola dinámica: FIFO", () => { var q = new LinkedQueue<int>(); q.Enqueue(1); q.Enqueue(2); Equal(1, q.Dequeue()); Equal(2, q.Dequeue()); });

        Run("Restaurante: reserva antes que sin reserva", () => { var q = new RestaurantPriorityQueue(); q.Enqueue(Cliente(1, TipoTurno.SinReserva)); q.Enqueue(Cliente(2, TipoTurno.Reserva)); Equal(2, q.Dequeue().Folio); });
        Run("Restaurante: FIFO igual prioridad", () => { var q = new RestaurantPriorityQueue(); q.Enqueue(Cliente(1, TipoTurno.Reserva)); q.Enqueue(Cliente(2, TipoTurno.Reserva)); Equal(1, q.Dequeue().Folio); Equal(2, q.Dequeue().Folio); });
        Run("Restaurante: entrelazado y prioridad", () => { var q = new RestaurantPriorityQueue(); q.Enqueue(Cliente(1, TipoTurno.SinReserva)); q.Enqueue(Cliente(2, TipoTurno.Reserva)); q.Enqueue(Cliente(3, TipoTurno.SinReserva)); q.Enqueue(Cliente(4, TipoTurno.Reserva)); Equal("2,4,1,3", string.Join(",", q.Select(c => c.Folio))); });
        Run("Restaurante: número de turnos", () => { var q = new RestaurantPriorityQueue(); q.Enqueue(Cliente(1, TipoTurno.Reserva)); Equal(1, q.Count); q.Dequeue(); Equal(0, q.Count); });
        Run("Restaurante: Peek sin extraer", () => { var q = new RestaurantPriorityQueue(); q.Enqueue(Cliente(1, TipoTurno.Reserva)); Equal(1, q.Peek().Folio); Equal(1, q.Count); });
        Run("Restaurante: atiende hasta vaciar", () => { var q = new RestaurantPriorityQueue(); q.Enqueue(Cliente(1, TipoTurno.SinReserva)); q.Dequeue(); True(q.IsEmpty); });
        Run("Restaurante: vacío -> error", () => Throws<InvalidOperationException>(() => new RestaurantPriorityQueue().Dequeue()));
        Run("Restaurante: datos inválidos", () => Throws<ArgumentException>(() => new ClienteTurno(1, " ", 2, TipoTurno.Reserva, Fecha)));
        Run("Restaurante: persona inválida", () => Throws<ArgumentOutOfRangeException>(() => new ClienteTurno(1, "A", 0, TipoTurno.Reserva, Fecha)));

        Console.WriteLine($"\nRESULTADO: {ejecutadas - fallidas}/{ejecutadas} correctas; {fallidas} fallidas.");
        return fallidas == 0 ? 0 : 1;
    }
}
