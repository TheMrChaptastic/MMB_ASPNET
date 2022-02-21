using System.Collections.Generic;

namespace MMB_ASPNET.Models
{
    public interface IPlayerRepository
    {
        public IEnumerable<Player> GetAllPlayers();
        public IEnumerable<Match> GetPlayersMatches(int pId);
        public Player GetPlayer(int id);
        public void UpdatePlayerName(Player player);
        public void UpdatePlayerStats(Player player);
        public void InsertPlayer(Player playerInsert);
        public void DeletePlayer(Player player);
        public void PlayerMatch(Player w, Player l);
        public bool PlayerNamesCheck(Player player);
    }
}
