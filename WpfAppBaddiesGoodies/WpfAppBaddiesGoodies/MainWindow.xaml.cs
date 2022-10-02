using GameLogic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfAppBaddiesGoodies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Maneger maneger;
        public MainWindow()
        {
            InitializeComponent();
            CanvasBackground();
            maneger = new Maneger(myCanvas);

        }
       
        private void CanvasBackground()
        {
            ImageBrush backgroundImage = new ImageBrush();
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/WpfAppBaddiesGoodies;component/Image/gameBackground2.png"));
            myCanvas.Background = backgroundImage;
        }
        #region Key function
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            maneger.KeysCheck(sender, e);
            maneger.PlayerJump(sender, e);
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            maneger.MakeKeyFalse(sender, e);
        }

        #endregion

    }

}
