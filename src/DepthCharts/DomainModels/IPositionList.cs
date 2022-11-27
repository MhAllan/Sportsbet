using DepthCharts.Models;

namespace DepthCharts.DomainModels;

public interface IPositionList
{
    void Add(Player player, uint? depth);
    void Remove(string playerId);
    IEnumerable<Player> GetPlayersUnder(string playerId);
    IEnumerable<Player> GetAllPlayers();
}
