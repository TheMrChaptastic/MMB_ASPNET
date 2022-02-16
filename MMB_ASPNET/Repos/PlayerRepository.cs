using Dapper;
using System.Collections.Generic;
using System.Data;

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

        public Player GetPlayer(int id)
        {
            return _conn.QuerySingle<Player>("Select * FROM players WHERE id = @Id;", new { Id = id });
        }

        public void UpdatePlayerName(Player player)
        {
            _conn.Execute("UPDATE players SET name = @name WHERE id = @Id", new { name = player.Name, player.Id });
        }

        public void UpdatePlayerStats(Player player)
        {
            _conn.Execute("UPDATE players SET Mmr = @Mmr, Wins = @Wins, Losses = @Losses, Master = @Master WHERE id = @Id", 
                new { player.Mmr, player.Wins, player.Losses, player.Master, player.Id});
        }

        public void InsertPlayer(Player playerInsert)
        {
            _conn.Execute("INSERT INTO players (Name, Mmr, wins, Losses) VALUES (@name, 1600, 0, 0);",
                new { name = playerInsert.Name });
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
