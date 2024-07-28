namespace ChessCS
{
    public class GameState
    {
        public Player White { get; set; }
        public Player Black { get; set; }
        public Player CurrentPlayer { get; set; }



        public GameState(Player whitePlayer, Player blackPlayer)
        {
            White = whitePlayer;
            Black = blackPlayer;
            CurrentPlayer = whitePlayer; //I'm initialising CurrentPlayer as white because white always starts first

            whitePlayer.Colour = "w";
            blackPlayer.Colour = "b";
        }

        public void SwitchPlayer() // Logic to switch CurrentPlayer after every move
        {
            if (CurrentPlayer == White)
            {
                CurrentPlayer = Black;
            }

            else
            {
                CurrentPlayer = White;
            }
        }


        public class Player
        {
            public string Colour { get; set; }
        }
    }
}
