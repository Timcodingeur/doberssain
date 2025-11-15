// Test simple des fichiers audio - à ajouter temporairement dans MainWindow.xaml.cs
// Ajoutez ce code dans le constructeur MainWindow() pour diagnostiquer les problèmes audio

private void DiagnosticAudio()
{
    try
    {
        // Vérifier le répertoire de l'application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string soundsDir = System.IO.Path.Combine(baseDir, "Sounds");
        
        MessageBox.Show($"Répertoire de l'app: {baseDir}\nRépertoire sons: {soundsDir}", "Diagnostic Audio");
        
        // Vérifier si le dossier Sounds existe
        if (!Directory.Exists(soundsDir))
        {
            MessageBox.Show("Le dossier Sounds n'existe pas!", "Erreur");
            return;
        }
        
        // Lister tous les fichiers dans Sounds
        var files = Directory.GetFiles(soundsDir);
        string fileList = "Fichiers trouvés:\n" + string.Join("\n", files.Select(f => Path.GetFileName(f)));
        MessageBox.Show(fileList, "Fichiers audio");
        
        // Test simple d'un fichier MP3
        string testFile = System.IO.Path.Combine(soundsDir, "sneaky_business.mp3");
        if (File.Exists(testFile))
        {
            try
            {
                // Créer un MediaPlayer simple pour test
                var player = new MediaPlayer();
                player.Open(new Uri(testFile, UriKind.Absolute));
                player.Play();
                MessageBox.Show($"Test de lecture: {testFile}", "Test Audio");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de lecture: {ex.Message}", "Erreur Audio");
            }
        }
        else
        {
            MessageBox.Show($"Fichier non trouvé: {testFile}", "Erreur");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur générale: {ex.Message}", "Erreur Diagnostic");
    }
}

// INSTRUCTIONS:
// 1. Ajoutez "DiagnosticAudio();" dans le constructeur MainWindow()
// 2. Ajoutez "using System.Linq;" en haut du fichier si pas déjà présent
// 3. Compilez et lancez pour voir les messages de diagnostic
// 4. Supprimez ce code une fois le problème identifié