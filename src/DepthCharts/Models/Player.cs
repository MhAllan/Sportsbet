namespace DepthCharts.Models;

public class Player
{
    public string Id { get; init; }
    public string Name { get; init; }

    public Player(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
