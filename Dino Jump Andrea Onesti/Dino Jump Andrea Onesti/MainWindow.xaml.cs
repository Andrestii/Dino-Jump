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
using System.Windows.Threading;
using System.Media;
// Andrea Onesti
namespace Dino_Jump_Andrea_Onesti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        double dinoMove = 15, openingOp = 1, mainMenuOp = 1, playerMenuOp = 0, leaderboardMenuOp = 0;
        bool openMainMenu = false, openPlayMenu = false, openLeaderboardMenu = false;
        bool up = true; // True == up, False == down
        string username = "Player";
        public Leaderboard leaderboard = new Leaderboard();
        MediaPlayer gameMusic = new MediaPlayer();
        GameWindow gwBuffer = new GameWindow("test", "dino");

        public MainWindow()
        {
            InitializeComponent();

            gameMusic.Open(new Uri(@"./../../../sounds/dino_music.wav", UriKind.Relative));
            gameMusic.Volume = 0.2;
            gameMusic.Play();

            background1.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/menu_background.png", UriKind.Relative)));
            background2.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/menu_background.png", UriKind.Relative)));
            background3.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/menu_background.png", UriKind.Relative)));
            title.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/logo_dino_jump.png", UriKind.Relative)));
            dino.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/menu_dino.png", UriKind.Relative)));

            leaderboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            leaderboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(9, GridUnitType.Star) });
            for (int i = 0; i < 10; i++)
                leaderboardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (openingOp >= 0.01)
                opening.Opacity = (openingOp -= 0.01);
            else
                opening.Visibility = Visibility.Hidden;

            Canvas.SetTop(dino, Canvas.GetTop(dino) - dinoMove);
            if (up && dinoMove > -8)
                dinoMove -= 0.5;
            else
            {
                dinoMove += 0.5;
                up = false;
                if (dinoMove == 8)
                    up = true;
            }

            Canvas.SetLeft(background1, Canvas.GetLeft(background1) - 1);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 1);
            Canvas.SetLeft(background3, Canvas.GetLeft(background3) - 1);
            if (Canvas.GetLeft(background1) < -1691)
                Canvas.SetLeft(background1, Canvas.GetLeft(background3) + background3.Width);
            if (Canvas.GetLeft(background2) < -1691)
                Canvas.SetLeft(background2, Canvas.GetLeft(background1) + background1.Width);
            if (Canvas.GetLeft(background3) < -1691)
                Canvas.SetLeft(background3, Canvas.GetLeft(background2) + background2.Width);

            // Open PlayerMenu
            if (openPlayMenu)
            {
                if (mainMenuOp >= 0.1)
                    MainMenu.Opacity = (mainMenuOp -= 0.1);
                else
                    MainMenu.Visibility = Visibility.Hidden;

                PlayerMenu.Visibility = Visibility.Visible;
                if (playerMenuOp <= 1)
                    PlayerMenu.Opacity = (playerMenuOp += 0.1);
                else
                    openPlayMenu = false;
            }
            else
                openPlayMenu = false;

            // Open LeaderboardMenu
            if (openLeaderboardMenu)
            {
                if (mainMenuOp >= 0.1)
                    MainMenu.Opacity = (mainMenuOp -= 0.1);
                else
                    MainMenu.Visibility = Visibility.Hidden;

                LeaderboardMenu.Visibility = Visibility.Visible;
                if (leaderboardMenuOp <= 1)
                    LeaderboardMenu.Opacity = (leaderboardMenuOp += 0.1);
                else
                    openLeaderboardMenu = false;
            }

            // Open MainMenu
            if (openMainMenu)
            {
                if (playerMenuOp >= 0.1)
                    PlayerMenu.Opacity = (playerMenuOp -= 0.1);
                else
                    PlayerMenu.Visibility = Visibility.Hidden;

                if (leaderboardMenuOp >= 0.1)
                    LeaderboardMenu.Opacity = (leaderboardMenuOp -= 0.1);
                else
                    LeaderboardMenu.Visibility = Visibility.Hidden;

                MainMenu.Visibility = Visibility.Visible;
                if (mainMenuOp <= 1)
                    MainMenu.Opacity = (mainMenuOp += 0.1);
                else
                    openMainMenu = false;

                username = "Player";
            }

            // Music Manager
            if (!gwBuffer.IsActive)
            {
                gameMusic.Play();
            }
            gameMusic.MediaEnded += GameMusicEnded;
        }

        private void KeyboardInput(object sender, KeyEventArgs e)
        {
            /* switch(e.Key)
            {
                case Key.A:
                    GameWindow gw = new GameWindow(username, "mario");
                    gw.Show();
                    break;
                case Key.Escape:
                    this.Close();
                    break;
            }*/
            //if (e.Key == Key.Escape)
            //    this.Close();
        }

        private void playBTN_Click(object sender, RoutedEventArgs e)
        {
            openPlayMenu = true;
            Canvas.SetLeft(usernameTB, (MainCanvas.ActualWidth - usernameTB.ActualWidth) / 2);
            Canvas.SetLeft(instructions, (MainCanvas.ActualWidth - instructions.ActualWidth) / 2);
            dinoSelectBTN.Background = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/dino_select.png", UriKind.Relative)));
            Canvas.SetLeft(dinoSelectBTN, (MainCanvas.ActualWidth) / 3 - (dinoSelectBTN.ActualWidth / 2));
            marioSelectBTN.Background = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/mario_select.png", UriKind.Relative)));
            Canvas.SetLeft(marioSelectBTN, (MainCanvas.ActualWidth - marioSelectBTN.ActualWidth) / 2);
            minecraftSelectBTN.Background = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/minecraft_select.png", UriKind.Relative)));
            Canvas.SetLeft(minecraftSelectBTN, (MainCanvas.ActualWidth * 2) / 3 - (minecraftSelectBTN.ActualWidth / 2));
        }

        private void leaderboardBTN_Click(object sender, RoutedEventArgs e)
        {
            leaderboard.userData = ManageLeaderboardXML.LeaderboardReader();

            Canvas.SetLeft(leaderboardTitle, (MainCanvas.ActualWidth - leaderboardTitle.ActualWidth) / 2);
            Canvas.SetLeft(leaderboardTitle2, (MainCanvas.ActualWidth - leaderboardTitle2.ActualWidth) / 2);
            Canvas.SetLeft(leaderboardGrid, (MainCanvas.ActualWidth - leaderboardGrid.ActualWidth) / 2);

            int k = 0;
            var bc = new BrushConverter();
            foreach (UserData ud in leaderboard.userData)
            {
                if (k < 10)
                    for (int j = 0; j < 2; j++)
                    {
                        Label l = new Label();
                        l.Foreground = (Brush)bc.ConvertFrom("#f5a462");
                        l.Background = (Brush)bc.ConvertFrom("#690B95");
                        l.FontSize = 70;
                        l.FontFamily = new FontFamily("Bebas Neue");
                        //l.VerticalContentAlignment = VerticalAlignment.Center;

                        if (j == 0)
                            l.Content = ud.Points;
                        else
                            l.Content = ud.Username;
                        leaderboardGrid.Children.Add(l);
                        Grid.SetColumn(l, j);
                        Grid.SetRow(l, k);
                    }
                k++;
            }

            openLeaderboardMenu = true;
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e)
        {
            gameMusic.Stop();
            Application.Current.Shutdown();
        }

        private void DinoSelect(object sender, RoutedEventArgs e)
        {
            openMainMenu = true;
            if (usernameTB.Text != "")
                username = usernameTB.Text;

            GameWindow gw = new GameWindow(username, "dino");
            gwBuffer = gw;
            gw.Show();
        }

        private void MarioSelect(object sender, RoutedEventArgs e)
        {
            openMainMenu = true;
            if (usernameTB.Text != "")
                username = usernameTB.Text;

            gameMusic.Stop(); // Stops default music

            GameWindow gw = new GameWindow(username, "mario");
            gwBuffer = gw;
            gw.Show();
        }

        private void MinecraftSelect(object sender, RoutedEventArgs e)
        {
            openMainMenu = true;
            if (usernameTB.Text != "")
                username = usernameTB.Text;
            
            gameMusic.Stop(); // Stops default music

            GameWindow gw = new GameWindow(username, "minecraft");
            gwBuffer = gw; 
            gw.Show();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            openMainMenu = true;
        }

        private void GameMusicEnded(object sender, EventArgs e)
        {
            gameMusic.Stop();
            gameMusic.Position = System.TimeSpan.FromSeconds(0);
            gameMusic.Play();
        }
    }
}
// Andrea Onesti