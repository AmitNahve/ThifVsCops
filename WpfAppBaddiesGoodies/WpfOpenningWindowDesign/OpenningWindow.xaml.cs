using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfOpenningWindowDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CanvasBackground();
        }
        private void CanvasBackground()
        {
            openningCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\Amit\source\repos\FirstProjectGoodiesBaddies\WpfAppBaddiesGoodies\WpfAppBaddiesGoodies\Image\newBackground.jpg", UriKind.Relative)));
        }

        private void startGameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
        }
    }
}
