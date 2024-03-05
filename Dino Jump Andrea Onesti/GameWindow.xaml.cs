using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Media;
// Andrea Onesti
namespace Dino_Jump_Andrea_Onesti
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        int scoreNum, gameSpeedMarker, powerupMarker, animationMarker, powerupType;
        double playerMove, playerMoveConst, jumpSpeed, gameSpeed;
        bool gameActive, jumpable, obstacle1Start, obstacle2Start, powerupActive;
        Rect playerHitbox, floorHitbox, obstacle1Hitbox, obstacle2Hitbox, powerupHitbox;
        MediaPlayer gameMusic = new MediaPlayer();
        MediaPlayer jumpSound = new MediaPlayer();
        MediaPlayer deathSound = new MediaPlayer();
        MediaPlayer defaultMusic = new MediaPlayer();


        string username, skin;
        public Leaderboard leaderboard = new Leaderboard();

        public GameWindow(string iUsername, string iSkin)
        {
            InitializeComponent();

            leaderboard.userData = ManageLeaderboardXML.LeaderboardReader();

            username = iUsername;
            skin = iSkin;

            var bc = new BrushConverter();
            gameMusic.Stop();
            switch (skin)
            {
                case "dino":
                    backgroundSky1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_sky.png", UriKind.Relative)));
                    backgroundSky2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_sky.png", UriKind.Relative)));
                    backgroundLandscape1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_landscape.png", UriKind.Relative)));
                    backgroundLandscape2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_landscape.png", UriKind.Relative)));
                    player.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino1.png", UriKind.Relative)));
                    obstacle1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_obstacle.png", UriKind.Relative)));
                    obstacle2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_obstacle.png", UriKind.Relative)));
                    powerup.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_star.png", UriKind.Relative)));
                    floor.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_floor.png", UriKind.Relative)));
                    floor2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/dino_floor.png", UriKind.Relative)));
                    score.Foreground = (Brush)bc.ConvertFrom("#E78036");
                    powerupTimer.Foreground = (Brush)bc.ConvertFrom("#E78036");
                    startText.Foreground = (Brush)bc.ConvertFrom("#E78036");
                    gameOverText.Foreground = (Brush)bc.ConvertFrom("#E78036");
                    gameOverText2.Foreground = (Brush)bc.ConvertFrom("#E78036");

                    jumpSound.Open(new Uri(@"./../../../sounds/jump.wav", UriKind.Relative));
                    break;
                case "mario":
                    backgroundSky1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_sky.png", UriKind.Relative)));
                    backgroundSky2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_sky.png", UriKind.Relative)));
                    backgroundLandscape1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_landscape.png", UriKind.Relative)));
                    backgroundLandscape2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_landscape.png", UriKind.Relative)));
                    player.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario1.png", UriKind.Relative)));
                    obstacle1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_obstacle.png", UriKind.Relative)));
                    obstacle2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_obstacle.png", UriKind.Relative)));
                    powerup.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_star.png", UriKind.Relative)));
                    floor.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_floor.png", UriKind.Relative)));
                    floor2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/mario_floor.png", UriKind.Relative)));
                    score.Foreground = (Brush)bc.ConvertFrom("#F83800");
                    powerupTimer.Foreground = (Brush)bc.ConvertFrom("#F83800");
                    startText.Foreground = (Brush)bc.ConvertFrom("#F83800");
                    gameOverText.Foreground = (Brush)bc.ConvertFrom("#F83800");
                    gameOverText2.Foreground = (Brush)bc.ConvertFrom("#F83800");

                    gameMusic.Open(new Uri(@"./../../../sounds/mario_music.wav", UriKind.Relative));
                    jumpSound.Open(new Uri(@"./../../../sounds/mario_jump.wav", UriKind.Relative));
                    deathSound.Open(new Uri(@"./../../../sounds/mario_death.wav", UriKind.Relative));
                    break;
                case "minecraft":
                    backgroundSky1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_sky.png", UriKind.Relative)));
                    backgroundSky2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_sky.png", UriKind.Relative)));
                    backgroundLandscape1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_landscape.png", UriKind.Relative)));
                    backgroundLandscape2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_landscape.png", UriKind.Relative)));
                    player.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft1.png", UriKind.Relative)));
                    obstacle1.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_obstacle.png", UriKind.Relative)));
                    obstacle2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_obstacle.png", UriKind.Relative)));
                    powerup.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_star.png", UriKind.Relative)));
                    floor.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_floor.png", UriKind.Relative)));
                    floor2.Fill = new ImageBrush(new BitmapImage(new Uri("./../../../images/minecraft_floor.png", UriKind.Relative)));
                    score.Foreground = (Brush)bc.ConvertFrom("#ba865e");
                    powerupTimer.Foreground = (Brush)bc.ConvertFrom("#ba865e");
                    startText.Foreground = (Brush)bc.ConvertFrom("#ba865e");
                    gameOverText.Foreground = (Brush)bc.ConvertFrom("#ba865e");
                    gameOverText2.Foreground = (Brush)bc.ConvertFrom("#ba865e");

                    gameMusic.Open(new Uri(@"./../../../sounds/minecraft_music.wav", UriKind.Relative));
                    gameMusic.Play();
                    jumpSound.Open(new Uri(@"./../../../sounds/jump.wav", UriKind.Relative));
                    deathSound.Open(new Uri(@"./../../../sounds/minecraft_death.wav", UriKind.Relative));
                    break;
            }
            gameMusic.Volume = 0.2;
            jumpSound.Volume = 0.2;
            deathSound.Volume = 0.2;

            playerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            floorHitbox = new Rect(Canvas.GetLeft(floor), Canvas.GetTop(floor), floor.Width, floor.Height);

            gameTimer.Interval = TimeSpan.FromMilliseconds(15);
            gameTimer.Tick += GameRunning;
        }

        public void Start()
        {
            // Variables
            scoreNum = 0; gameSpeedMarker = 0; powerupMarker = 0; powerupType = 0;
            playerMove = 0; playerMoveConst = 25; jumpSpeed = 1; gameSpeed = 0;
            jumpable = true; obstacle1Start = false; obstacle2Start = false; powerupActive = false;

            // Components
            Canvas.SetLeft(backgroundSky1, 0);
            Canvas.SetLeft(backgroundSky2, 2000);
            Canvas.SetLeft(backgroundLandscape1, 0);
            Canvas.SetLeft(backgroundLandscape2, 2000);

            Canvas.SetTop(obstacle1, 850);
            Canvas.SetTop(obstacle2, 850);
            Canvas.SetLeft(obstacle1, 2000);
            Canvas.SetLeft(obstacle2, 4000);
            Canvas.SetTop(powerup, 800);
            Canvas.SetLeft(powerup, 2000);

            Canvas.SetTop(player, 830);
            Canvas.SetLeft(player, 300);
            Canvas.SetLeft(floor, 0);
            Canvas.SetLeft(floor2, 1920);

            startText.Visibility = Visibility.Hidden;
            gameOverText.Visibility = Visibility.Hidden;
            gameOverText2.Visibility = Visibility.Hidden;

            // Music
            if (skin == "mario")
                gameMusic.Play();
            deathSound.Stop();

            // Start
            gameActive = true;
            gameTimer.Start();
        }

        public void GameRunning(object sender, EventArgs e)
        {
            Random r = new Random();

            // Score
            score.Content = "Score: " + scoreNum++;

            // Jump
            Canvas.SetTop(player, Canvas.GetTop(player) - playerMove);
            playerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 40, player.Height);

            if (playerMove >= -playerMoveConst)
                playerMove -= jumpSpeed;

            if (playerHitbox.IntersectsWith(floorHitbox))
            {
                playerMove = 0;
                jumpable = true;
                jumpSpeed = 1;
                Canvas.SetTop(player, 830); 

                jumpSound.Stop();

                // Running animation
                animationMarker++;
                if (animationMarker < 10)
                {
                    if (skin == "dino")
                        player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/dino2.png", UriKind.Relative)));
                    else if (skin == "mario")
                        player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/mario2.png", UriKind.Relative)));
                    else if (skin == "minecraft")
                        player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/minecraft2.png", UriKind.Relative)));
                }
                else if (animationMarker < 20)
                {
                    if (skin == "dino")
                        player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/dino1.png", UriKind.Relative)));
                    else if (skin == "mario")
                        player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/mario1.png", UriKind.Relative)));
                    else if (skin == "minecraft")
                        player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/minecraft1.png", UriKind.Relative)));
                }
                else animationMarker = 0;
            }
            else
            {
                if (skin == "dino")
                    player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/dino2.png", UriKind.Relative)));
                else if (skin == "mario")
                    player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/mario2.png", UriKind.Relative)));
                else if (skin == "minecraft")
                    player.Fill = new ImageBrush(new BitmapImage(new Uri(@"./../../../images/minecraft1.png", UriKind.Relative)));
            }

            // Powerups
            powerupMarker++;
            if (powerupMarker >= 700 && !powerupActive)
            {
                Canvas.SetLeft(powerup, Canvas.GetLeft(powerup) - 10 - gameSpeed);
                powerupHitbox = new Rect(Canvas.GetLeft(powerup), Canvas.GetTop(powerup), powerup.Width - 50, powerup.Height - 5);
            }
            if (playerHitbox.IntersectsWith(powerupHitbox))
            {
                Canvas.SetLeft(powerup, 2000);
                Canvas.SetTop(powerup, r.Next(350) + 500);
                powerupHitbox = new Rect(Canvas.GetLeft(powerup), Canvas.GetTop(powerup), powerup.Width, powerup.Height);
                powerupMarker = 0;
                powerupActive = true;
                powerupType = r.Next(2) + 1;
            }
            if ((Canvas.GetLeft(powerup) + powerup.Width) < 0)
            {
                Canvas.SetLeft(powerup, 2000);
                Canvas.SetTop(powerup, r.Next(350) + 500);
                powerupHitbox = new Rect(Canvas.GetLeft(powerup), Canvas.GetTop(powerup), powerup.Width, powerup.Height);
                powerupMarker = 0;
            }
            powerupTimer.Content = "";
            if (powerupActive && powerupMarker <= 300)
                switch (powerupType)
                {
                    case 1: // Jump boost
                        powerupTimer.Content = "Jump boost time: " + (300 - powerupMarker);
                        playerMoveConst = 40;
                        break;
                    case 2: // Invincibility
                        powerupTimer.Content = "Invincibility time: " + (300 - powerupMarker);
                        playerHitbox = new Rect(0, 0, 0, 0);
                        break;
                }
            else
            {
                playerMoveConst = 25;
                powerupActive = false;
            }

            // Obstacles
            if (Canvas.GetLeft(obstacle1) <= 700 && Canvas.GetLeft(obstacle1) > 650)
            {
                obstacle2Start = true;
                ObstacleGenerator();
            }
            if (Canvas.GetLeft(obstacle2) <= 700 && Canvas.GetLeft(obstacle2) > 650)
            {
                obstacle1Start = true;
                ObstacleGenerator();
            }

            Canvas.SetLeft(obstacle1, Canvas.GetLeft(obstacle1) - 10 - gameSpeed);
            Canvas.SetLeft(obstacle2, Canvas.GetLeft(obstacle2) - 10 - gameSpeed);
            obstacle1Hitbox = new Rect(Canvas.GetLeft(obstacle1), Canvas.GetTop(obstacle1), obstacle1.Width - 70, obstacle1.Height - 20);
            obstacle2Hitbox = new Rect(Canvas.GetLeft(obstacle2), Canvas.GetTop(obstacle2), obstacle2.Width - 70, obstacle2.Height - 20);

            if (playerHitbox.IntersectsWith(obstacle1Hitbox) || playerHitbox.IntersectsWith(obstacle2Hitbox))
                GameOver();

            // Game Speed
            gameSpeedMarker++;
            if (gameSpeed <= 20)
                if (gameSpeedMarker == 500)
                {
                    gameSpeed += 1;
                    gameSpeedMarker = 0;
                }

            // Backgrounds and floor
            Canvas.SetLeft(backgroundSky1, Canvas.GetLeft(backgroundSky1) - 1 - gameSpeed);
            Canvas.SetLeft(backgroundSky2, Canvas.GetLeft(backgroundSky2) - 1 - gameSpeed);
            Canvas.SetLeft(backgroundLandscape1, Canvas.GetLeft(backgroundLandscape1) - 3 - gameSpeed);
            Canvas.SetLeft(backgroundLandscape2, Canvas.GetLeft(backgroundLandscape2) - 3 - gameSpeed);
            Canvas.SetLeft(floor, Canvas.GetLeft(floor) - 10 - gameSpeed);
            Canvas.SetLeft(floor2, Canvas.GetLeft(floor2) - 10 - gameSpeed);

            if (Canvas.GetLeft(backgroundSky1) < -2000)
                Canvas.SetLeft(backgroundSky1, Canvas.GetLeft(backgroundSky2) + backgroundSky2.Width);
            if (Canvas.GetLeft(backgroundSky2) < -2000)
                Canvas.SetLeft(backgroundSky2, Canvas.GetLeft(backgroundSky1) + backgroundSky1.Width);
            if (Canvas.GetLeft(backgroundLandscape1) < -2000)
                Canvas.SetLeft(backgroundLandscape1, Canvas.GetLeft(backgroundLandscape2) + backgroundLandscape2.Width);
            if (Canvas.GetLeft(backgroundLandscape2) < -2000)
                Canvas.SetLeft(backgroundLandscape2, Canvas.GetLeft(backgroundLandscape1) + backgroundLandscape1.Width);
            if (Canvas.GetLeft(floor) < -1920)
                Canvas.SetLeft(floor, Canvas.GetLeft(floor2) + floor2.Width);
            if (Canvas.GetLeft(floor2) < -1920)
                Canvas.SetLeft(floor2, Canvas.GetLeft(floor) + floor.Width);

            gameMusic.MediaEnded += GameMusicEnded;
        }

        public void KeyboardInput(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.Space:
                    if (jumpable)
                    {
                        playerMove = playerMoveConst;
                        jumpable = false;
                        jumpSound.Play();
                    }
                    break;
                case Key.Down:
                    jumpSpeed *= 10;
                    break;
                case Key.Enter:
                    if (!gameActive)
                        Start();
                    break;
                case Key.Escape:
                    if (!gameActive)
                    {
                        ManageLeaderboardXML.LeaderboardWriter(leaderboard.userData);
                        gameMusic.Stop();
                        this.Close();
                    }
                    break;
            }
        }

        public void ObstacleGenerator()
        {
            Random r = new Random();
            if (obstacle2Start)
            {
                Canvas.SetTop(obstacle2, r.Next(150) + 750);
                Canvas.SetLeft(obstacle2, 2000);
                obstacle2Start = false;
            }
            if (obstacle1Start)
            {
                Canvas.SetTop(obstacle1, r.Next(150) + 750);
                Canvas.SetLeft(obstacle1, 2000);
                obstacle1Start = false;
            }
        }

        public void GameOver()
        {
            gameTimer.Stop();
            Canvas.SetLeft(gameOverText, (GameCanvas.ActualWidth - gameOverText.ActualWidth) / 2);
            Canvas.SetTop(gameOverText, (GameCanvas.ActualHeight - gameOverText.ActualHeight) / 2);
            Canvas.SetLeft(gameOverText2, (GameCanvas.ActualWidth - gameOverText2.ActualWidth) / 2);
            Canvas.SetTop(gameOverText2, ((GameCanvas.ActualHeight - gameOverText2.ActualHeight) / 2) + gameOverText.ActualHeight / 2);
            gameOverText.Visibility = Visibility.Visible;
            gameOverText2.Visibility = Visibility.Visible;
            gameActive = false;
            
            leaderboard.InsertData(username, scoreNum);

            if (skin == "mario")
            {
                gameMusic.Stop();
                deathSound.Play();
            }
            if (skin == "minecraft")
            {
                deathSound.Play();
            }
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