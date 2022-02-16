using System.Collections.Generic;

namespace MMB_ASPNET.Models
{
    public interface IMatchRepository
    {
        public IEnumerable<Match> GetAllMatches();
        public void InsertMatch(Player w, Player l, int wChange, int lChange);
        public void NewMatch(Player w, Player l);
    }
}
