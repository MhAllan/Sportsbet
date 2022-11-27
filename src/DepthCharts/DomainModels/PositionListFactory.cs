namespace DepthCharts.DomainModels;

public class PositionListFactory : IPositionListFactory
{
    public IPositionList CreateList(string position) => new PositionList(position);
}
