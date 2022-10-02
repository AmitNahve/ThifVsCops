using System;
using System.Windows.Media.Imaging;

namespace Mangers
{

    public class Cops : Player
    {
        public Cops(double height, double width) : base(width, height)
        {
            Img.Source = new BitmapImage(new Uri("/Image/police.png", UriKind.Relative));

        }
    }
}
