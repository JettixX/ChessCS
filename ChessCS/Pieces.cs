
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessCS
{
    public abstract class Pieces
    {
        
        public abstract string Name { get; }
        public string Color { get; set; }
       

        // Property to hold the image source
        public ImageSource ImageSource { get; set; }

        public Pieces(string color)
        {
            ImageSource = LoadImage($"/Assets/{color}{Name}.png"); // Initialize ImageSource using LoadImage
        }

        public static ImageSource LoadImage(string filename)
        {
            var bitmap = new BitmapImage(new Uri(filename, UriKind.Relative));
            RenderOptions.SetBitmapScalingMode(bitmap, BitmapScalingMode.HighQuality);
            return bitmap;
        }
    }

    public class Pawn : Pieces
    {
        public override string Name => "Pawn";

        public Pawn(string color) : base(color) {
            Color = color;

        }
    }

    public class Rook : Pieces
    {
        public override string Name => "Rook";

        public Rook(string color) : base(color) {
            Color = color;
        }
    }

    public class Knight : Pieces
    {
        public override string Name => "Knight";

        public Knight(string color) : base(color) {
            Color = color;
        }
    }

    public class Bishop : Pieces
    {
        public override string Name => "Bishop";

        public Bishop(string color) : base(color) {
            Color = color;
        }
    }

    public class Queen : Pieces
    {
        public override string Name => "Queen";

        public Queen(string color) : base(color) {
            Color = color;
        }
    }

    public class King : Pieces
    {
        public override string Name => "King";

        public King(string color) : base(color) {
            Color = color;
        }
    }
}
