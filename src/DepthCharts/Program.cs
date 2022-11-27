using DepthCharts.DomainModels;
using DepthCharts.Models;

namespace CodingTest.DepthCharts;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var factory = new PositionListFactory();
            var chart = new DepthChart<AflPosition>(factory);
            var p = new Player("1", "Bob");
            chart.Add(p, AflPosition.A);
            var p1 = new Player("2", "Alice");
            chart.Add(p1, AflPosition.A);

            var p2 = new Player("3", "Foo");
            chart.Add(p2, AflPosition.A, 1);

            var p3 = new Player("4", "Bar");
            chart.Add(p3, AflPosition.A, 1);
            chart.Remove(p3.Id, AflPosition.A);

            var underBob = chart.GetPlayersUnder(AflPosition.A, p.Id);

            WritePlayers(underBob);

            chart.Add(p3, AflPosition.B, 0);

            var result = chart.GetFullChart();

            WriteFullChart(result);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void WriteFullChart(Dictionary<string, IEnumerable<Player>> result)
    {
        foreach (var item in result)
        {
            var str = $"{item.Key}: [{string.Join(", ", item.Value.Select(p => p.Id))}]";
            Console.WriteLine(str);
        }
    }

    static void WritePlayers(IEnumerable<Player> players)
    {
        var str = $"[{string.Join(", ", players.Select(p => p.Id))}]";
        Console.WriteLine(str);
    }
}

enum AflPosition
{
    A, B, C
}
