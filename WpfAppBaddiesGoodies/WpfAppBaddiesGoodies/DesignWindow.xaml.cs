using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfAppBaddiesGoodies
{
    /// <summary>
    /// Interaction logic for DesignWindow.xaml
    /// </summary>
    public partial class DesignWindow : Window
    {
        public Button muteBtn = new Button();
        public MediaPlayer gameMusic;
        private Uri gameMainMusic = new Uri("../../MediaPlayer/GameMainMusic.wav", UriKind.RelativeOrAbsolute);
        private bool isMute = false;
        public DesignWindow()
        {
            InitializeComponent();
            CanvasBackground();
            GameMusic();
            AddButtons();
        }
        #region ButtonsUI&&Logic
        public Button CreateButtons(string content, double setTopX, double setLeftY, RoutedEventHandler eventHandler) // return btn for i can use the mute button ant give him more details
        {
            Button button = new Button();
            button.Content = content;
            Canvas.SetTop(button, setTopX);
            Canvas.SetLeft(button, setLeftY);
            button.Click += eventHandler;
            OppeningCanvas.Children.Add(button);
            return button;
        }
        public void AddButtons()
        {
            CreateButtons("EXIT", 383, 685, ExitButton_Click);
            muteBtn = CreateButtons("Mute", 383, 0, MuteButton_Click);
        }
        private void MuteButton_Click(object sender, RoutedEventArgs e)// mute UnMute btn Click function  i used bool to switch between them in every click
        {
           
            if (isMute == false)
            {
                gameMusic.Stop();
                muteBtn.Content = "UnMute";
            }
            else
            {
                gameMusic.Play();
                muteBtn.Content = "Mute";
            }
            isMute = !isMute;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void startPlayBtn_Click(object sender, RoutedEventArgs e)// this function start the game && close The design window That opened first
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            gameMusic.Stop();
            designWindow.Close();
        }
        #endregion
        private void GameMusic()
        {
            gameMusic = new MediaPlayer();
            gameMusic.Open(gameMainMusic);
            gameMusic.Play();
        }
        private void CanvasBackground()
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource  = new BitmapImage(new Uri("pack://application:,,,/WpfAppBaddiesGoodies;component/Image/gameBackground2.png"));
            OppeningCanvas.Background = imageBrush;
        }
    }
}
