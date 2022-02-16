using System;

namespace MMB_ASPNET.Models
{
    public class Match
    {
        public int MId { get; set; }
        public int Winner { get; set; }
        public string WinnerName { get; set; }
        public int WMmrChange { get; set; }
        public int Loser { get; set; }
        public string LoserName { get; set; }
        public int LMmrChange { get; set; }
        public DateTime Date{ get; set; }
    }
}
