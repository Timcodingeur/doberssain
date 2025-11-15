using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace doberssainLeNul
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private bool isOutOfSphere = false;
        private string chosenPath = ""; // "merde", "casino", ou "premium"

        public MainWindow()
        {
            InitializeComponent();
            
            // Test des fichiers audio au démarrage
            TestAudioFiles();
            
            // Easter egg: 1 chance sur 20 que le jeu crashe
            if (random.Next(1, 21) == 1)
            {
                ShowEasterEgg();
            }
            
            // Démarrer la musique de fond
            StartBackgroundMusic();
            
            // Commencer avec un fade-in sur la scène intro
            Scene0.Opacity = 0;
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(Scene0);
        }

        private void TestAudioFiles()
        {
            string soundsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");
            Debug.WriteLine($"Répertoire des sons: {soundsDir}");
            
            string[] expectedFiles = {
                "sneaky_business.mp3", "anime_wow.mp3", "bruh.mp3", 
                "coins_falling.mp3", "why_you_bully_me.mp3", "slot_machine.mp3", "click.mp3"
            };
            
            foreach (string file in expectedFiles)
            {
                string fullPath = System.IO.Path.Combine(soundsDir, file);
                bool exists = File.Exists(fullPath);
                Debug.WriteLine($"Fichier {file}: {(exists ? "✅ Trouvé" : "❌ Manquant")} - {fullPath}");
            }
        }

        private void ShowEasterEgg()
        {
            HideAllScenes();
            EasterEggScene.Visibility = Visibility.Visible;
            
            // Fermer l'application après 3 secondes
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, e) => {
                timer.Stop();
                Application.Current.Shutdown();
            };
            timer.Start();
        }

        private void HideAllScenes()
        {
            Scene0.Visibility = Visibility.Collapsed;
            Scene1.Visibility = Visibility.Collapsed;
            Scene2.Visibility = Visibility.Collapsed;
            Scene3.Visibility = Visibility.Collapsed;
            Scene4.Visibility = Visibility.Collapsed;
            SceneFinal.Visibility = Visibility.Collapsed;
        }

        private void TransitionToScene(Grid targetScene)
        {
            PlaySound("click");
            HideAllScenes();
            targetScene.Visibility = Visibility.Visible;
            targetScene.Opacity = 0;
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(targetScene);
        }

        private void StartBackgroundMusic()
        {
            try
            {
                string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "sneaky_business.mp3");
                Debug.WriteLine($"Tentative de lecture: {musicPath}");
                
                if (File.Exists(musicPath))
                {
                    BackgroundMusic.Source = new Uri(musicPath, UriKind.Absolute);
                    BackgroundMusic.LoadedBehavior = MediaState.Manual;
                    BackgroundMusic.Play();
                    Debug.WriteLine("Musique de fond démarrée");
                }
                else
                {
                    Debug.WriteLine($"Fichier audio non trouvé: {musicPath}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur musique de fond: {ex.Message}");
                MessageBox.Show($"Erreur audio: {ex.Message}", "Debug Audio");
            }
        }
        
        private void BackgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Relancer la musique en boucle
            BackgroundMusic.Position = TimeSpan.Zero;
            BackgroundMusic.Play();
        }

        private void PlaySound(string soundName)
        {
            try
            {
                string soundPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", $"{soundName}.mp3");
                Debug.WriteLine($"Tentative de lecture son: {soundPath}");
                
                if (File.Exists(soundPath))
                {
                    SoundPlayer.Stop(); // Arrêter le son précédent
                    SoundPlayer.Source = new Uri(soundPath, UriKind.Absolute);
                    SoundPlayer.LoadedBehavior = MediaState.Manual;
                    SoundPlayer.Play();
                    Debug.WriteLine($"Son joué: {soundName}");
                }
                else
                {
                    Debug.WriteLine($"Fichier son non trouvé: {soundPath}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur de son: {ex.Message}");
                // Afficher l'erreur pour debug
                // MessageBox.Show($"Erreur son {soundName}: {ex.Message}", "Debug Audio");
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            TransitionToScene(Scene1);
        }

        private void Choice3Btn_Click(object sender, RoutedEventArgs e)
        {
            chosenPath = "premium";
            TransitionToScene(Scene4);
        }

        private void Choice1Btn_Click(object sender, RoutedEventArgs e)
        {
            chosenPath = "merde";
            TransitionToScene(Scene2);
            
            // Démarrer l'animation des billets qui tombent
            var moneyAnimation = (Storyboard)Resources["MoneyFallStoryboard"];
            moneyAnimation.Begin();
            
            PlaySound("coins_falling");
        }

        private void Choice2Btn_Click(object sender, RoutedEventArgs e)
        {
            chosenPath = "casino";
            TransitionToScene(Scene3);
            Focus(); // Pour capturer les événements clavier
        }

        private void ContinueFromScene2Btn_Click(object sender, RoutedEventArgs e)
        {
            // Aller à la scène 4 mais adapter pour la voie "merde"
            TransitionToScene(Scene4);
            ShowEndingForPath();
        }

        private void ContinueFromScene3Btn_Click(object sender, RoutedEventArgs e)
        {
            // Montrer le texte de fin casino directement dans la scène 3
            CasinoEndingText.Visibility = Visibility.Visible;
            PlaySound("slot_machine");
            
            // Changer le bouton pour aller à la fin
            ContinueFromScene3Btn.Content = "Accepter la ruine";
            ContinueFromScene3Btn.Click -= ContinueFromScene3Btn_Click;
            ContinueFromScene3Btn.Click += (s, args) => TransitionToScene(SceneFinal);
        }

        private void FinalBtn_Click(object sender, RoutedEventArgs e)
        {
            TransitionToScene(SceneFinal);
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Ordi_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'ouvrir le lien. Doberssain a encore gagné.", "Erreur", 
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Scene3.Visibility != Visibility.Visible) return;

            double left = Canvas.GetLeft(Player);
            double top = Canvas.GetTop(Player);
            
            // Valeurs par défaut si pas encore définies
            if (double.IsNaN(left)) left = 390;
            if (double.IsNaN(top)) top = 290;

            // Déplacement du joueur
            switch (e.Key)
            {
                case Key.Up:
                    top = Math.Max(0, top - 10);
                    break;
                case Key.Down:
                    top = Math.Min(580, top + 10);
                    break;
                case Key.Left:
                    left = Math.Max(0, left - 10);
                    break;
                case Key.Right:
                    left = Math.Min(780, left + 10);
                    break;
            }

            Canvas.SetLeft(Player, left);
            Canvas.SetTop(Player, top);

            // Vérifier si le joueur est sorti de la sphère
            CheckIfOutOfSphere(left, top);
        }

        private void ShowEndingForPath()
        {
            // Adapter l'affichage de la scène 4 selon la voie choisie
            if (chosenPath == "merde")
            {
                PremiumText.Visibility = Visibility.Collapsed;
                MerdeEndingText.Visibility = Visibility.Visible;
                PlaySound("why_you_bully_me");
            }
            else if (chosenPath == "premium")
            {
                PremiumText.Visibility = Visibility.Visible;
                MerdeEndingText.Visibility = Visibility.Collapsed;
                PlaySound("bruh");
            }
        }

        private void CheckIfOutOfSphere(double playerLeft, double playerTop)
        {
            // Coordonnées du centre de la sphère (200 + 200 = 400, 100 + 200 = 300)
            double sphereCenterX = 400;
            double sphereCenterY = 300;
            double sphereRadius = 200;

            // Centre du joueur
            double playerCenterX = playerLeft + 10;
            double playerCenterY = playerTop + 10;

            // Calculer la distance du centre du joueur au centre de la sphère
            double distance = Math.Sqrt(Math.Pow(playerCenterX - sphereCenterX, 2) + 
                                      Math.Pow(playerCenterY - sphereCenterY, 2));

            if (distance > sphereRadius && !isOutOfSphere)
            {
                isOutOfSphere = true;
                
                // Afficher "CASINO UNLOCKED!"
                CasinoUnlockedText.Visibility = Visibility.Visible;
                ContinueFromScene3Btn.Visibility = Visibility.Visible;
                
                // Démarrer l'animation de clignotement
                var blinkAnimation = (Storyboard)Resources["CasinoBlinkStoryboard"];
                blinkAnimation.Begin();
                
                PlaySound("anime_wow");
            }
        }
    }
}