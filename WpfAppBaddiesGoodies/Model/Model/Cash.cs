using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mangers
{
    public class Cash : Player
    {
        public Cash(double width, double height) : base(width, height)
        {
            Img.Source = new BitmapImage(new Uri("/Image/Cash.png", UriKind.Relative));
        }
    }
}
