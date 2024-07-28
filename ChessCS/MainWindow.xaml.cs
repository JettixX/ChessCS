using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private GameState _gameState;
        private Board _board;
        private GameState.Player _whitePlayer;
        private GameState.Player _blackPlayer;

        // Constructor to initialize players and game state
        public MainWindow()
        {
            
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            Directory.SetCurrentDirectory(projectRoot); //Code to allow relative file paths (@"\Assets\..." instead of ("C:\Users\..\source\repos\ChessCS\ChessCS\Assets\capture.wav")

            InitializeComponent();
            _whitePlayer = new GameState.Player(); // Initialize player
            _blackPlayer = new GameState.Player(); // Initialize player
            _gameState = new GameState(_whitePlayer, _blackPlayer);



            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _board = new Board();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button square = new()
                    {
                        Margin = new Thickness(0),
                        Background = new SolidColorBrush(Colors.Transparent), // Making the buttons transparent to avoid obscuring the chequered colour pattern
                        BorderThickness = new Thickness(0),
                    };

                    Pieces pieces = _board.Squares[row, col];
                    Pieces piece = pieces;

                    if (piece != null)
                    {
                        square.Content = new Image
                        {
                            Source = piece.ImageSource,
                        }; // Set button content to piece image
                    }

                    square.Click += OnSquareClick; // Attaching the event handler for when the user clicks any button

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);
                    Board.Children.Add(square);
                }
            }
        }

        // Making a field to track the selected square
        private (int, int)? _selectedSquare = null;

        // The actual event handler for when the user clicks any button
        private void OnSquareClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);


            if (_selectedSquare == null)
            {
                if (_board.Squares[row, col] != null)
                {
                    _selectedSquare = (row, col);
                    clickedButton.Background = Brushes.LightYellow; // Highlighting the clicked square with a light yellow color
                }



            }
            else
            {
                var (fromRow, fromCol) = _selectedSquare.Value;


                Pieces selectedPiece = _board.Squares[fromRow, fromCol];

                // Player check

                if (selectedPiece != null && selectedPiece.Color == _gameState.CurrentPlayer.Colour)
                {
                    Console.WriteLine("Valid");
                }

                else
                {
                    MessageBox.Show($"It's {_gameState.CurrentPlayer.Colour}'s turn.");

                    Button originalButton = Board.Children
                                    .OfType<Button>()
                                    .FirstOrDefault(btn => Grid.GetRow(btn) == fromRow && Grid.GetColumn(btn) == fromCol);
                    if (originalButton != null)
                    {
                        originalButton.Background = Brushes.Transparent; // Remove highlight
                    }
                    _selectedSquare = null; // Reset selection
                    return;
                }

                if (fromRow == row && fromCol == col) // Check to see if the original square is the same as the destination square
                {
                    RejectMove(fromRow, fromCol);
                }
                else
                {


                    // Check to make sure piece on the destination square isn't the same colour
                    if (_selectedSquare != null)
                    {
                        Pieces Destination = _board.Squares[row, col];
                        if (Destination != null && selectedPiece.Color == Destination.Color)
                        {
                            // Pieces are the same color, so the move is invalid

                            Button originalButton = Board.Children
                                .OfType<Button>()
                                .FirstOrDefault(btn => Grid.GetRow(btn) == fromRow && Grid.GetColumn(btn) == fromCol);
                            if (originalButton != null)
                            {
                                originalButton.Background = Brushes.Transparent; // Remove highlight
                            }
                            _selectedSquare = null; // Reset selection
                            return;
                        }
                    }

                    // Move the piece

                    _board.MovePiece((fromRow, fromCol), (row, col));

                    // Update the UI

                    Button fromButton = Board.Children
                        .OfType<Button>()
                        .First(btn => Grid.GetRow(btn) == fromRow && Grid.GetColumn(btn) == fromCol);
                    if (clickedButton.Content != null)
                    {
                        MoveSound(@"Assets\capture.wav");
                    }
                    else
                    {
                        MoveSound(@"Assets\move-self.wav");
                    }
                    fromButton.Content = null; // Remove the content of the original square because the piece is moving to a different square
                    clickedButton.Content = new Image
                    {
                        Source = selectedPiece.ImageSource,
                    }; // Update the content of the destination square with the new piece
                       // Reset
                    fromButton.Background = Brushes.Transparent; // Remove highlight
                    _selectedSquare = null; // Reset selection

                    // Switch the current player after the move
                    _gameState.SwitchPlayer();
                }
            }
        }

        public void RejectMove(int fromRow, int fromCol)
        {
            Button originalButton = Board.Children
                                .OfType<Button>()
                                .FirstOrDefault(btn => Grid.GetRow(btn) == fromRow && Grid.GetColumn(btn) == fromCol);
            if (originalButton != null)
            {
                originalButton.Background = Brushes.Transparent; // Remove highlight
            }
            _selectedSquare = null; // Reset selection
            return;
        }

        public void MoveSound(string filepath)
        {
            SoundPlayer player = new();
            player.SoundLocation = filepath;
            player.Play();
        }

    }
}