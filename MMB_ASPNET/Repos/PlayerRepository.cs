using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MMB_ASPNET.Models
{
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        private readonly IDbConnection _conn;
        public IEnumerable<Player> GetAllPlayers() => _conn.Query<Player>("SELECT * FROM players ORDER BY mmr DESC;");
        public IEnumerable<Match> GetPlayersMatches(int pId) => _conn.Query<Match>("SELECT m.MId, m.Winner, w.Name AS WinnerName, m.WMmrChange, m.Loser, l.Name AS LoserName, m.LMmrChange, m.Date FROM matchlog AS m " +
            "LEFT JOIN players AS w ON w.Id = m.Winner " +
            $"LEFT JOIN players AS l ON l.Id = m.Loser WHERE m.Winner = {pId} OR m.Loser = {pId} ORDER BY MId DESC;");

        public Player GetPlayer(int id)
        {
            return _conn.QuerySingle<Player>("Select * FROM players WHERE id = @Id;", new { Id = id });
        }

        public void UpdatePlayerName(Player player)
        {
            if (PlayerNamesCheck(player))
            {
                _conn.Execute("UPDATE players SET name = @name WHERE id = @Id", new { name = player.Name, player.Id });
            }        
        }

        public void UpdatePlayerStats(Player player)
        {
            _conn.Execute("UPDATE players SET Mmr = @Mmr, Wins = @Wins, Losses = @Losses, Master = @Master WHERE id = @Id", 
                new { player.Mmr, player.Wins, player.Losses, player.Master, player.Id});
        }

        public void InsertPlayer(Player playerInsert)
        {
            if (PlayerNamesCheck(playerInsert))
            {
                _conn.Execute("INSERT INTO players (Name) VALUES (@name);",
                    new { name = playerInsert.Name });
            }
        }

        public bool PlayerNamesCheck(Player player)
        {
            var pList = _conn.Query<Player>("SELECT * FROM players;");

            if (pList.Where(x => x.Name == player.Name).Count() > 0)
            {
                return false;
            }
            return true;
        }

        public void DeletePlayer(Player player)
        {
            _conn.Execute("DELETE FROM players WHERE id = @Id;",
                new { player.Id });
        }

        public void PlayerMatch(Player w, Player l)
        {
            UpdatePlayerStats(w);
            UpdatePlayerStats(l);
        }
    }
}
