using MMB_ASPNET.Models;
using System;

namespace MMB_ASPNET.Services
{
    public static class MmrCalc
    {
        public static int MmrChangeWinner(Player w, Player l)
        {
            var factK = DetermineKFactor(w);
            var winDiff = 1 / (1 + Math.Pow(10, ((double)l.Mmr - (double)w.Mmr) / 400));
            return (int)(factK * (1 - winDiff));
        }
        public static int MmrChangeLoser(Player w, Player l)
        {
            var factK = DetermineKFactor(l);
            var lossDiff = 1 / (1 + Math.Pow(10, ((double)w.Mmr - (double)l.Mmr) / 400));
            return (int)(factK * (0 - lossDiff));
        }
        private static int DetermineKFactor(Player p)
        {
            if (p.Master == true && p.Wins + p.Losses > 30)
            {
                return 10;
            }
            else if (p.Wins + p.Losses < 30)
            {
                return 40;
            }
            else
            {
                return 20;
            }
        }
    }
}
