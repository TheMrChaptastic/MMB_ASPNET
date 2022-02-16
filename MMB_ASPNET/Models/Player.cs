namespace MMB_ASPNET.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mmr { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public bool Master { get; set; }

    }
}
