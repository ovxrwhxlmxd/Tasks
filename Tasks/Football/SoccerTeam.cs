namespace Tasks.Football
{
    public class SoccerTeam
    {
        public SoccerPlayer[] Players { get; set; }
        public SoccerPlayer this[int index]
        {
            get
            {
                if (Players == null) return null;
                return index > 0 && index < Players.Length ? Players[index] : throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (index < 0) throw new ArgumentOutOfRangeException();
                if (Players == null)
                    if (index == 0) Players = new SoccerPlayer[] { value };
                    else
                    {
                        Players = new SoccerPlayer[] { null };
                        for (var i = 1; i < index; i++)
                        {
                            Players[i] = null;
                        }
                        Players[index] = value;
                    }
                else if (Players.Length > index) Players[index] = value;
                else
                {
                    var newPlayers = Players;
                    Array.Resize(ref newPlayers, index * 2);
                    Players = newPlayers;
                    Players[index] = value;
                }
            }
        }
    }

    public class SoccerPlayer
    {
        public string Name { get; set; }
        public uint Number { get; set; }
    }
}
