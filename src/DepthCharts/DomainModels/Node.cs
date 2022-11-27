using DepthCharts.Models;

namespace DepthCharts.DomainModels;

public class Node
{
    public Player Player { get; init; }
    public Node Next { get; set; }
    public Node Prev { get; set; }

    public Node(Player player)
    {
        Player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public override string ToString()
    {
#if DEBUG
        return Player.Id;
#else
        return base.ToString();
#endif
    }
}
