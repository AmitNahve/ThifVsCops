using Mangers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GameLogic
{
    public class Maneger
    {
        public Random rnd = new Random();
        public Canvas _cnv;
        public Thief _thief;
        public List<Cops> _cops;
        public List<Cash> _cash;
        public MediaPlayer backgroundMusic = new MediaPlayer();
        public Button muteButton = new Button();
        public int Score = 0;
        public bool soundMute = false;
        private bool goLeft, goRight, goUp, goDown;
        TextBlock txtScore = new TextBlock();
        DispatcherTimer timer = new DispatcherTimer();

        public Maneger(Canvas cnv)
        {
            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += GameTimer;
            timer.Start();
            _cnv = cnv;
            _cops = new List<Cops>();
            _cash = new List<Cash>();
            Start();

        }

        public void Start()//this function start the application UI
        {
            var BackgroundSoundUri = new Uri("../../MediaPlayer/GameMainMusic.wav", UriKind.RelativeOrAbsolute);
            backgroundMusic.Open(BackgroundSoundUri);
            backgroundMusic.Play();

            AddPlayer();
            AddEnemies();
            AddCashs();
            AddTextScore();
            AddButtons();
        }
        private void GameTimer(object sender, EventArgs e) //all the game running here in the timer
        {
            PlayerMovment();
            MoveEnemies();
            CheckCollisionsOfCops();
            CheckThiefDath();
            CheckCashCollisions();

        }
        #region Add UI Items function
        public void AddEnemies()
        {

            if (_cops != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    _cops.Add(new Cops(50, 50));

                    do
                    {
                        Canvas.SetTop(_cops[i].Img, rnd.Next(50, 550)); //position of Y BadGuy
                        Canvas.SetLeft(_cops[i].Img, rnd.Next(50, 920));//position of X Badguy
                    }
                    while (!CheckLocationNoColision(Canvas.GetLeft(_cops[i].Img), Canvas.GetTop(_cops[i].Img)));
                    _cnv.Children.Add(_cops[i].Img);
                }
            }
        }
        private bool CheckLocationNoColision(double x, double y)//chacked that we dont get colision
        {
            if (Math.Abs(Canvas.GetTop(_thief.Img) - y) > 80 && Math.Abs(Canvas.GetLeft(_thief.Img) - x) > 80) 
                return true;
            else
                return false;
        }
        public void AddPlayer()
        {
            _thief = new Thief(45, 45);
            Canvas.SetTop(_thief.Img, rnd.Next(50, 500));
            Canvas.SetLeft(_thief.Img, rnd.Next(50, 900));
            _cnv.Children.Add(_thief.Img);
        }
        public void AddTextScore()
        {
            _cnv.Children.Add(txtScore);
            Canvas.SetLeft(txtScore, 10);
            Canvas.SetTop(txtScore, 10);
            txtScore.Height = 30;
            txtScore.Width = 100;
            txtScore.FontSize = 20;
            txtScore.Text = "score: " + Score.ToString();
        }
        private void AddCashs()
        {
            int cashCount = _cash.Count; // take the cash list count to prevent that i have tow pic on one place in the CashList
            for (int i = cashCount; i < cashCount + 20; i++)
            {
                _cash.Add(new Cash(25, 25));
                Canvas.SetTop(_cash[i].Img, rnd.Next(0, 500));
                Canvas.SetLeft(_cash[i].Img, rnd.Next(0, 900));
                _cnv.Children.Add(_cash[i].Img);
            }

        }
        public Button CreateButtons(string content, double setTopX, double setLeftY, RoutedEventHandler eventHandler)//this function return button for i can Change details from them for example in line 133.
        {
            Button button = new Button();
            button.Content = content;
            Canvas.SetTop(button, setTopX);
            Canvas.SetLeft(button, setLeftY);
            button.Click += eventHandler;
            _cnv.Children.Add(button);
            return button;
        }
        public void AddButtons()
        {
            CreateButtons("EXIT", 570, 900, ExitButton_Click);
            CreateButtons("Pause", 570, 0, PauseButton_Click);
            CreateButtons("Continue", 570, 100, ContinueButton_Click);
            CreateButtons("Restart", 570, 200, RestartButton_Click);
            muteButton = CreateButtons("Mute", 570, 300, MuteButton_Click);
        }
        #endregion

        #region Game Buttons Cliks Logic
        
        public void MuteButton_Click(object sender, RoutedEventArgs e)
        {

            if (soundMute == true)
            {
                backgroundMusic.Play();
                muteButton.Content = "Mute";
            }
            else
            {
                backgroundMusic.Stop();
                muteButton.Content = "UnMute";
            }
            soundMute = !soundMute;
        }
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Player Movment functions
        public void PlayerMovment()
        {
            if (_thief.Img != null)// if the img null we goterror of null
            {
                if (goDown)
                {
                    double PlayerDown = Canvas.GetTop(_thief.Img);
                    if (PlayerDown + 8 < 525)
                        Canvas.SetTop(_thief.Img, PlayerDown + 8);
                }
                if (goRight)
                {
                    double PlayerRight = Canvas.GetLeft(_thief.Img);
                    if (PlayerRight + 8 < 950)
                        Canvas.SetLeft(_thief.Img, PlayerRight + 8);
                }
                if (goUp)
                {
                    double PlayerUp = Canvas.GetTop(_thief.Img);
                    if (PlayerUp - 8 > 0)
                        Canvas.SetTop(_thief.Img, PlayerUp - 8);
                }
                if (goLeft)
                {
                    double PlayerLeft = Canvas.GetLeft(_thief.Img);
                    if (PlayerLeft - 8 > 0)
                        Canvas.SetLeft(_thief.Img, PlayerLeft - 8);
                }
            }
        }
        public void KeysCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                goLeft = true;
            if (e.Key == Key.Right)
                goRight = true;
            if (e.Key == Key.Up)
                goUp = true;
            if (e.Key == Key.Down)
                goDown = true;
        }
        public void MakeKeyFalse(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                goLeft = false;
            if (e.Key == Key.Up)
                goUp = false;
            if (e.Key == Key.Right)
                goRight = false;
            if (e.Key == Key.Down)
                goDown = false;
        }
        public void PlayerJump(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Canvas.SetTop(_thief.Img, rnd.Next(0, 525));
                Canvas.SetLeft(_thief.Img, rnd.Next(0, 900));
            }
        }
        #endregion

        #region Enemies movment function
        public void MoveEnemies()
        {
            for (int i = 0; i < _cops.Count; i++)
            {
                if (Canvas.GetLeft(_thief.Img) > Canvas.GetLeft(_cops[i].Img))
                {
                    Canvas.SetLeft(this._cops[i].Img, Canvas.GetLeft(_cops[i].Img) + 1.5);
                }
                else
                {
                    Canvas.SetLeft(this._cops[i].Img, Canvas.GetLeft(_cops[i].Img) - 1.5);
                }
                if (Canvas.GetTop(_thief.Img) > Canvas.GetTop(_cops[i].Img))
                {
                    Canvas.SetTop(this._cops[i].Img, Canvas.GetTop(_cops[i].Img) + 1.5);
                }
                else
                {
                    Canvas.SetTop(this._cops[i].Img, Canvas.GetTop(_cops[i].Img) - 1.5);
                }
            }
        }
        #endregion

        #region Win or Lose functions
        public void CheckThiefDath()
        {
            double y = Canvas.GetTop(_thief.Img);
            double x = Canvas.GetLeft(_thief.Img);
            foreach (var cop in _cops)
            {
                double getCopsTop = Canvas.GetTop(cop.Img);
                double getCopsLeft = Canvas.GetLeft(cop.Img);
                if (Math.Abs(getCopsTop - y) < 30 && Math.Abs(getCopsLeft - x) < 30)
                {
                    _cnv.Children.Remove(_thief.Img);
                    GameOver();
                    return;
                }
            }
        }
        public void CheckCashCollisions()
        {


            double GetleftGoodGuy = (double)Canvas.GetLeft(_thief.Img);
            double GetTopGoodGuy = (double)Canvas.GetTop(_thief.Img);
            for (int i = 0; i < _cash.Count; i++)
            {
                double GetleftCash = (int)Canvas.GetLeft(_cash[i].Img);
                double GetTopCash = (int)Canvas.GetTop(_cash[i].Img);

                if (Math.Abs(GetleftGoodGuy - GetleftCash) < 50 && Math.Abs(GetTopGoodGuy - GetTopCash) < 50)
                {
                    _cnv.Children.Remove(_cash[i].Img);
                    _cash.Remove(_cash[i]);
                    CashSound();
                    Score++;
                }
                txtScore.Text = "score: " + Score.ToString();
                if (_cash.Count <= 4)
                {
                    AddCashs(); // add more cash to the game
                }
            }
        }
        private static void CashSound()
        {
            MediaPlayer cashPlayer = new MediaPlayer();
            var cashSoundUri = new Uri("../../MediaPlayer/cash2.wav", UriKind.RelativeOrAbsolute);
            cashPlayer.Open(cashSoundUri);
            cashPlayer.Play();
        }
        public void CheckCollisionsOfCops()
        {
            for (int i = 0; i < _cops.Count - 1; i++)
            {
                for (int j = i + 1; j < _cops.Count; j++)
                {
                    if (Math.Abs(Canvas.GetLeft(_cops[j].Img) - Canvas.GetLeft(_cops[i].Img))
                        < 40 && Math.Abs(Canvas.GetTop(_cops[j].Img)) - Canvas.GetTop(_cops[i].Img) < 40)
                    {
                        _cnv.Children.Remove(_cops[j].Img);
                        _cops.Remove(_cops[j]);
                        Score++;
                        txtScore.Text = "score: " + Score.ToString();

                        if (_cops.Count == 1)
                        {
                            PlayerWon();
                        }
                    }
                }
            }
        }
        private void GameOver()
        {

            for (int i = 0; i < _cops.Count; i++)
            {
                _cops.RemoveAt(i);
            }
            MediaPlayer gameOverPlayer = new MediaPlayer();
            var gameOverUri = new Uri("../../MediaPlayer/GameOverSound.wav", UriKind.RelativeOrAbsolute);
            gameOverPlayer.Open(gameOverUri);
            gameOverPlayer.Play();

            if (MessageBox.Show(" your score is: " + Score + " \n Do you want to Play again ?\n press Yes to restart or No to Exit the game??", " You lose! Game over!",
            MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else
            {

                RestartGame();
            }
        }
        private void PlayerWon()
        {
            timer.Stop();
            PlayWinningSound();
            if (MessageBox.Show(" your score is: " + Score + " \n Do you want to Play again?\n press Yes to restart or No to Exit the game.", " You won!!!",
             MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else
            {
                RestartGame();
            }
        }
        public void RestartGame()
        {
            _cnv.Children.Clear();
            _cash.Clear();
            _cops.Clear();
            Score = 0;
            Start();
            timer.Start();
            goLeft = false;
            goRight = false;
            goUp = false;
            goDown = false;
            backgroundMusic.Play();
        }
        public void PlayWinningSound()
        {
            MediaPlayer winningSoundPlayer = new MediaPlayer();
            var soundUri = new Uri("../../MediaPlayer/WinningSound.wav", UriKind.RelativeOrAbsolute);
            winningSoundPlayer.Open(soundUri);
            winningSoundPlayer.Play();
        }

        #endregion
    }
}

