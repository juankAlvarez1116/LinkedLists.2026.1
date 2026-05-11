using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    public bool Contains(T data)
    {
        var current = _head;

        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public void InsertOrdered(T data)
    {
        var newNode = new Node<T>(data);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        var current = _head;

        while (current != null && current.Data!.CompareTo(data) < 0)
        {
            current = current.Next;
        }

        // Insertar al final
        if (current == null)
        {
            _tail!.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }

        // Insertar al inicio
        else if (current == _head)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }

        // Insertar en medio
        else
        {
            newNode.Next = current;
            newNode.Previous = current.Previous;

            current.Previous!.Next = newNode;
            current.Previous = newNode;
        }
    }

    public void Remove(T data)
    {
        var current = _head;

        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                // único nodo
                if (_head == _tail)
                {
                    _head = null;
                    _tail = null;
                }

                // eliminar cabeza
                else if (current == _head)
                {
                    _head = _head!.Next;
                    _head!.Previous = null;
                }

                // eliminar cola
                else if (current == _tail)
                {
                    _tail = _tail!.Previous;
                    _tail!.Next = null;
                }

                // eliminar en medio
                else
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }

                return;
            }

            current = current.Next;
        }
    }

    public void RemoveAll(T data)
    {
        while (Contains(data))
        {
            Remove(data);
        }
    }

    public void Reverse()
    {
        var current = _head;
        Node<T>? temp = null;

        while (current != null)
        {
            temp = current.Previous;
            current.Previous = current.Next;
            current.Next = temp;

            current = current.Previous;
        }

        temp = _head;
        _head = _tail;
        _tail = temp;
    }

    public void Sort()
    {
        Reverse();
    }

    public string GetModes()
    {
        if (_head == null)
        {
            return "List is empty";
        }

        Dictionary<T, int> counts = new Dictionary<T, int>();

        var current = _head;

        while (current != null)
        {
            if (counts.ContainsKey(current.Data!))
            {
                counts[current.Data!]++;
            }
            else
            {
                counts[current.Data!] = 1;
            }

            current = current.Next;
        }

        int max = counts.Values.Max();

        string result = "";

        foreach (var item in counts)
        {
            if (item.Value == max)
            {
                result += $"{item.Key} ";
            }
        }

        return $"Mode(s): {result}";
    }

    public string GetGraph()
    {
        if (_head == null)
        {
            return "List is empty";
        }

        Dictionary<T, int> counts = new Dictionary<T, int>();

        var current = _head;

        while (current != null)
        {
            if (counts.ContainsKey(current.Data!))
            {
                counts[current.Data!]++;
            }
            else
            {
                counts[current.Data!] = 1;
            }

            current = current.Next;
        }

        string result = "";

        foreach (var item in counts)
        {
            result += $"{item.Key} ";

            for (int i = 0; i < item.Value; i++)
            {
                result += "*";
            }

            result += "\n";
        }

        return result;
    }

    override public string ToString()
    {
        var current = _head;
        var result = string.Empty;

        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }

        result += "null";

        return result;
    }

    public string ToStringReverse()
    {
        var current = _tail;
        var result = string.Empty;

        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Previous;
        }

        result += "null";

        return result;
    }
}