using MMB_ASPNET.Models;

namespace MMB_ASPNET.Services
{
    public static class MasterCheck
    {
        public static void CheckMmr(params Player[] players)
        {
            foreach(var p in players)
            {
                if (p.Mmr >= 2400)
                {
                    p.Master = true;
                }
            }
        }
    }
}
