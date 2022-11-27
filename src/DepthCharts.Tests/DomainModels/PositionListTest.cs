namespace DepthCharts.Tests.DomainModels;

public class PositionListTest
{
    [Fact]
    public void EmptyList_HeadAndTail_Should_Be_Null()
    {
        var list = new PositionList("a");
        Assert.Null(list.Head);
        Assert.Null(list.Tail);
    }

    public static IEnumerable<object[]> Random_Add_Delete_Success_Data()
    {
        var ops = new PlayerOperation[]
        {
            new PlayerOperation { Player = new Player("1", "Bob"), Op = NodeOp.Add },
            new PlayerOperation { Player = new Player("2", "Alice"), Op = NodeOp.Add },
            new PlayerOperation { Player = new Player("3", "Foo"), Op = NodeOp.Add, Depth = 1 },
            new PlayerOperation { Player = new Player("4", "Bar"), Op = NodeOp.Add, Depth = 0 },
            new PlayerOperation { Player = new Player("1", "Bob"), Op = NodeOp.Remove },
        };

        var expected = new Player[]
        {
            new Player("4", "Bar"),
            new Player("3", "Foo"),
            new Player("2", "Alice")
        };

        yield return new object[] { ops, expected };
    }

    [Theory]
    [MemberData(nameof(Random_Add_Delete_Success_Data))]
    public void Random_Add_Delete_Success(IEnumerable<PlayerOperation> operations, IEnumerable<Player> expectedResult)
    {
        var list = new PositionList("a");
        foreach (var item in operations)
        {
            if (item.Op == NodeOp.Add)
            {
                list.Add(item.Player, item.Depth);
            }
            else if (item.Op == NodeOp.Remove)
            {
                list.Remove(item.Player.Id);
            }
            else throw new NotSupportedException(item.Op.ToString());
        }

        var head = list.Head;

        var found = GetPlayers(head);

        expectedResult.Should().BeEquivalentTo(found);
    }

    /*
     * 
     * And so on, I will not test for everything
     
     */

    IEnumerable<Player> GetPlayers(Node node)
    {
        while (node != null)
        {
            yield return node.Player;
            node = node.Next;
        }
    }
}
