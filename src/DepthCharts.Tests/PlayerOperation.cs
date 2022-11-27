using DepthCharts.Models;

namespace DepthCharts.Tests;

public class PlayerOperation
{
    public Player Player { get; set; }
    public NodeOp Op { get; set; }
    public uint? Depth { get; set; }
}

public enum NodeOp
{
    Add,
    Remove
}
