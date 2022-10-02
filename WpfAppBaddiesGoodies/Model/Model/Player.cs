using System.Windows.Controls;

namespace Mangers
{
    public abstract class Player
    {
        private Image _img;
        public Image Img
        {
            get { return _img; }
            set { _img = value; }
        }
        public double Width { get => _img.Width; }
        public double Hight { get => _img.Height; }
        public Player(double width, double height)
        {
            _img = new Image();
            _img.Width = width;
            _img.Height = height;
        }
    }

}
