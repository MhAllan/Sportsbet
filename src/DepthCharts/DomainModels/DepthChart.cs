using DepthCharts.Models;

namespace DepthCharts.DomainModels;

public class DepthChart<TEnum> where TEnum : struct
{
    readonly IPositionListFactory _factory;
    public DepthChart(IPositionListFactory factory)
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new Exception($"Generic argument {nameof(TEnum)} must be enum");
        }

        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    readonly Dictionary<TEnum, IPositionList> _dic = new Dictionary<TEnum, IPositionList>();

    public void Add(Player player, TEnum position, uint? depth = null)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        if (!_dic.TryGetValue(position, out var list))
        {
            list = _factory.CreateList(position.ToString());
            _dic.Add(position, list);
        }

        list.Add(player, depth);
    }

    public void Remove(string playerId, TEnum position)
    {
        if (!_dic.TryGetValue(position, out var list))
        {
            return; //or throw
        }

        list.Remove(playerId);
    }

    public IEnumerable<Player> GetPlayersUnder(TEnum position, string playerId)
    {
        if (!_dic.TryGetValue(position, out var list))
        {
            return Enumerable.Empty<Player>(); //or throw
        }

        return list.GetPlayersUnder(playerId);
    }

    public Dictionary<string, IEnumerable<Player>> GetFullChart()
    {
        return _dic.Select(x => new
        {
            Position = x.Key.ToString(),
            Players = x.Value.GetAllPlayers()
        }).ToDictionary(x => x.Position, x => x.Players);
    }
}
