using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Mangers
{
    public class Thief : Player
    {
        public Thief(double height, double width) : base(width, height)
        {
            Img.Source = new BitmapImage(new Uri("Image/thief.png", UriKind.Relative));

        }
    }
}
