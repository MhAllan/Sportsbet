namespace DepthCharts.Tests.DomainModels;

public class DepthChartTest
{
    Player _anyPlayer => It.IsAny<Player>();
    uint? _anyDepth => It.IsAny<uint?>();
    string _anyStr => It.IsAny<string>();

    [Fact]
    public void Add_Success()
    {
        var listMock = new Mock<IPositionList>();
        listMock.Setup(x => x.Add(_anyPlayer, _anyDepth));

        var factoryMock = new Mock<IPositionListFactory>();
        factoryMock.Setup(x => x.CreateList(_anyStr)).Returns(listMock.Object);

        var depthChart = new DepthChart<AflPosition>(factoryMock.Object);

        var p1 = new Player("1", "Bob");
        var p2 = new Player("2", "Alice");
        var p3 = new Player("3", "Foo");
        var p4 = new Player("4", "Bar");

        //3 on A
        depthChart.Add(p1, AflPosition.A);
        depthChart.Add(p2, AflPosition.A, 0);
        depthChart.Add(p3, AflPosition.A);
        //1 on B
        depthChart.Add(p4, AflPosition.B);

        factoryMock.Verify(x => x.CreateList("A"), Times.Once());
        factoryMock.Verify(x => x.CreateList("B"), Times.Once());

        listMock.Verify(x => x.Add(p1, null), Times.Once());
        listMock.Verify(x => x.Add(p2, 0), Times.Once());
        listMock.Verify(x => x.Add(p3, null), Times.Once());
        listMock.Verify(x => x.Add(p4, null), Times.Once());
        listMock.Verify(x => x.Add(_anyPlayer, _anyDepth), Times.Exactly(4));
    }

    /*
     * 
     * And so on, I will not test for everything
     
     */
}
