using DepthCharts.Models;

namespace DepthCharts.DomainModels;

public class PositionList
{
    public string Position { get; init; }

    //This is for finding corresponding node without iterating
    readonly Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

    //We can use LinkedList<T> but I want to implement my own here
    public Node Head { get; private set; }
    public Node Tail { get; private set; }

    public PositionList(string position)
    {
        Position = position;
    }

    public void Add(Player player, uint? depth)
    {
        if (depth.HasValue && depth.Value > _nodes.Count)
            throw new ArgumentException($"Invalid depth value {depth}");

        var id = player.Id;
        if (_nodes.ContainsKey(id))
            throw new Exception($"Player {id} is alreaded added for position {Position}");

        var node = new Node(player);
        _nodes.Add(id, node);

        if (Head == null)
        {
            Head = Tail = node;
            return;
        }

        if (!depth.HasValue)
        {
            AddAfter(Tail, node);
            return;
        }

        var target = FindByDepth(depth.Value);
        AddBefore(target, node);
    }

    public void Remove(string playerId)
    {
        if (!_nodes.TryGetValue(playerId, out var node))
        {
            return;
        }

        var prev = node.Prev;
        var next = node.Next;
        if (prev != null)
            prev.Next = next;
        if (next != null)
            next.Prev = prev;

        if (node == Head)
        {
            Head = next;
        }
        if (node == Tail)
        {
            Tail = prev;
        }

        _nodes.Remove(playerId);
    }

    public IEnumerable<Player> GetPlayersUnder(string playerId)
    {
        if (!_nodes.TryGetValue(playerId, out var node))
        {
            yield break;
        }

        node = node.Next;
        while (node != null)
        {
            yield return node.Player;
            node = node.Next;
        }
    }

    public IEnumerable<Player> GetChartPlayers()
    {
        var node = Head;
        while (node != null)
        {
            yield return node.Player;
            node = node.Next;
        }
    }

    Node FindByDepth(uint depth)
    {
        var node = Head;
        for (int i = 0; i < depth; i++)
        {
            node = node.Next;
        }
        return node;
    }

    void AddBefore(Node target, Node node)
    {
        var tPrev = target.Prev;

        target.Prev = node;
        node.Prev = tPrev;

        if (tPrev != null)
        {
            tPrev.Next = node;
        }
        node.Next = target;

        if (target == Head)
        {
            Head = node;
        }
    }

    void AddAfter(Node target, Node node)
    {
        var tNext = target.Next;
        target.Next = node;
        node.Next = tNext;

        if (tNext != null)
        {
            tNext.Prev = node;
        }
        node.Prev = target;

        if (target == Tail)
        {
            Tail = node;
        }
    }
}
