namespace LandMines.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int CurrentPosition { get; set; }
        public int LivesCount { get; set; }
        public int MovesCount { get; set; }
        public string PlayerIcon { get; set; }
        public bool ReachedDestination { get; internal set; }
    }
}
