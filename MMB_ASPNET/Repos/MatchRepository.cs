using Dapper;
using System.Collections.Generic;
using System.Data;
using System;
using MMB_ASPNET.Services;

namespace MMB_ASPNET.Models
{
    public class MatchRepository : IMatchRepository
    {
        public MatchRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        private readonly IDbConnection _conn;
        public IEnumerable<Match> GetAllMatches() => _conn.Query<Match>("SELECT m.MId, m.Winner, w.Name AS WinnerName, m.WMmrChange, m.Loser, l.Name AS LoserName, m.LMmrChange, m.Date FROM mymomsbasement.matchlog AS m " +
            "INNER JOIN mymomsbasement.players AS w ON w.Id = m.Winner " + 
            "INNER JOIN mymomsbasement.players AS l ON l.Id = m.Loser ORDER BY MId DESC;");

        public void NewMatch(Player w, Player l)
        {
            MasterCheck.CheckMmr(w, l);

            var wGain = MmrCalc.MmrChangeWinner(w, l);
            var lLoss = MmrCalc.MmrChangeLoser(w, l);
            w.Mmr += wGain;
            l.Mmr += lLoss;
            w.Wins++;
            l.Losses++;
            InsertMatch(w, l, wGain, lLoss);
        }

        public void InsertMatch(Player w, Player l, int wChange, int lChange)
        {
            _conn.Execute("INSERT INTO matchlog (Winner, WMmrChange, Loser, LMmrChange, Date) VALUES (@Winner, @WChange, @Loser, @LChange, @Date);",
                new { Winner = w.Id, WChange = wChange, Loser = l.Id, LChange = lChange, Date = DateTime.Now });
        }
    }
}
