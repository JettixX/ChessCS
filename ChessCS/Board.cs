
namespace ChessCS
{
    public class Board
    {
        public Pieces[,] Squares { get; set; } //2d array for holding all of our pieces

        public Board() //constructor to intialize board and put pieces in the starting positions I showed in figure 22 in the logbook
        {
            Squares = new Pieces[8, 8];
            InitializeBoard();
        }

        // this is the method to intialize the board in the STARTING position
        private void InitializeBoard()
        {
            Squares[7, 0] = new Rook("w");
            Squares[7, 1] = new Knight("w");
            Squares[7, 2] = new Bishop("w");
            Squares[7, 3] = new Queen("w");
            Squares[7, 4] = new King("w");
            Squares[7, 5] = new Bishop("w");
            Squares[7, 6] = new Knight("w");
            Squares[7, 7] = new Rook("w");

            // initializing black pieces, so the ones that are closer to the coordinate 8th rank


            Squares[0, 0] = new Rook("b");
            Squares[0, 1] = new Knight("b");
            Squares[0, 2] = new Bishop("b");
            Squares[0, 3] = new Queen("b");
            Squares[0, 4] = new King("b");
            Squares[0, 5] = new Bishop("b");
            Squares[0, 6] = new Knight("b");
            Squares[0, 7] = new Rook("b");

            // using a simple for loop to intialize pawns
            for (int v = 0; v < 8; v++)
            {
                Squares[6, v] = new Pawn("w");
                Squares[1, v] = new Pawn("b");
            }


        }

        // Method to move a piece from one square to another
        public void MovePiece((int x, int y) from, (int x, int y) to)
        {
            Pieces piece = Squares[from.x, from.y];
            Squares[to.x, to.y] = piece;
            Squares[from.x, from.y] = null;

            
        }
    }
}










