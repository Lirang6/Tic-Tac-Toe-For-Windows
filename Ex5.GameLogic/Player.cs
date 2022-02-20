namespace Ex5.GameLogic
{
    public enum ePlayer
    {
        Player1,
        Player2,
    }

    public class Player
    {
        public readonly string r_Name;
        public readonly ePlayer r_PlayerNumber;
        internal int m_Wins;

        public Player(string i_Name, ePlayer i_PlayerNumber)
        {
            m_Wins = 0;
            r_Name = i_Name;
            r_PlayerNumber = i_PlayerNumber;
        }

        public int Wins
        {
            get { return m_Wins; } 
        }
    }
}
