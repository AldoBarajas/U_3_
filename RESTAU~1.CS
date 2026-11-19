using System.Collections;
using System.Collections.Generic;

namespace Unidad3.Estructuras;

/// <summary>Reserva primero; dentro del mismo nivel se atiende por orden de llegada.</summary>
public enum TipoTurno { Reserva = 0, SinReserva = 1 }

/// <summary>Turno de un grupo en la sala de espera.</summary>
public sealed class ClienteTurno
{
    public int Folio { get; }
    public string Nombre { get; }
    public int Personas { get; }
    public TipoTurno Tipo { get; }
    public DateTime Llegada { get; }

    public ClienteTurno(int folio, string nombre, int personas, TipoTurno tipo, DateTime llegada)
    {
        if (folio <= 0) throw new ArgumentOutOfRangeException(nameof(folio));
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre obligatorio.", nameof(nombre));
        if (personas <= 0) throw new ArgumentOutOfRangeException(nameof(personas));
        if (!Enum.IsDefined(tipo)) throw new ArgumentOutOfRangeException(nameof(tipo));
        Folio = folio;
        Nombre = nombre.Trim();
        Personas = personas;
        Tipo = tipo;
        Llegada = llegada;
    }

    public override string ToString() =>
        $"#{Folio:D3} | {Nombre,-18} | {Personas,2} personas | {Tipo} | {Llegada:HH:mm:ss}";
}

/// <summary>
/// Cola de prioridad dinámica por lista enlazada propia, sin PriorityQueue nativa.
/// Enqueue O(n), Dequeue y Peek O(1), memoria O(n).
/// Estable: mantiene FIFO entre clientes de igual tipo.
/// </summary>
public sealed class RestaurantPriorityQueue : IEnumerable<ClienteTurno>
{
    private sealed class Node
    {
        public ClienteTurno Value;
        public Node? Next;
        public Node(ClienteTurno value) { Value = value; }
    }

    private Node? head;
    public int Count { get; private set; }
    public bool IsEmpty => head is null;

    public void Enqueue(ClienteTurno turno)
    {
        if (turno is null) throw new ArgumentNullException(nameof(turno));
        var nuevo = new Node(turno);
        if (head is null || (int)turno.Tipo < (int)head.Value.Tipo)
        {
            nuevo.Next = head;
            head = nuevo;
        }
        else
        {
            Node actual = head;
            while (actual.Next is not null && (int)actual.Next.Value.Tipo <= (int)turno.Tipo)
                actual = actual.Next;
            nuevo.Next = actual.Next;
            actual.Next = nuevo;
        }
        Count++;
    }

    public ClienteTurno Dequeue()
    {
        if (head is null) throw new InvalidOperationException("No hay clientes en espera.");
        ClienteTurno turno = head.Value;
        head = head.Next;
        Count--;
        return turno;
    }

    public ClienteTurno Peek()
    {
        if (head is null) throw new InvalidOperationException("No hay clientes en espera.");
        return head.Value;
    }

    public IEnumerator<ClienteTurno> GetEnumerator()
    {
        for (Node? actual = head; actual is not null; actual = actual.Next)
            yield return actual.Value;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
