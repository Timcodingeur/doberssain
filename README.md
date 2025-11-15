# MICRO-JEU DOBERSSAIN LE NUL™

## Description
Un jeu satirique et interactif développé en WPF C# qui suit les aventures de "Doberssain" à travers différentes scènes humoristiques.

## Fonctionnalités

### 🎮 Scènes du jeu :
1. **Scène d'intro** - Présentation du jeu avec effet de fade-in
2. **Choix du destin** - 3 options qui mènent à des expériences différentes
3. **"Rester une merde"** - Animation de billets qui tombent avec texte sarcastique
4. **"Sortir de la sphère"** - Mini-jeu de déplacement au clavier
5. **"Version premium"** - Scène finale avec easter egg Rickroll
6. **Écran de fin** - Conclusion philosophique sur la médiocrité

### 🎨 Éléments visuels :
- Interface avec police Comic Sans MS pour l'effet humoristique
- Transitions fluides avec animations de fondu
- Animations d'objets (billets qui tombent, effets de clignotement)
- Éléments interactifs (boutons, ordinateur cliquable)

### 🔊 Système audio :
- Effets sonores pour les clics
- Musique d'ambiance pour certaines scènes
- Support pour fichiers .wav

### 🎲 Easter Egg :
- 1 chance sur 20 que le jeu affiche "Doberssain a gagné. Vous non." et se ferme

## Installation

1. Clonez le repository
2. Ouvrez le projet dans Visual Studio ou VS Code
3. Compilez avec `dotnet build`
4. Lancez avec `dotnet run` ou exécutez directement l'exe généré

## Commandes

### Scène "Sortir de la sphère" :
- **Flèches directionnelles** : Déplacer le joueur rouge
- **Objectif** : Sortir de la zone grise pour débloquer le casino

### Navigation générale :
- **Clic souris** : Naviguer entre les scènes via les boutons
- **Clic sur ordinateur** (Scène 4) : Ouvre une surprise dans le navigateur

## Structure du projet

```
doberssainLeNul/
├── MainWindow.xaml          # Interface utilisateur
├── MainWindow.xaml.cs       # Logique du jeu
├── Assets/                  # Images du jeu
│   ├── stickman.png
│   ├── billets.png
│   ├── ordi.png
│   └── ...
├── Sounds/                  # Effets sonores
│   ├── click.wav
│   ├── moneyfall.wav
│   └── casinoBGM.wav
└── ...
```

## Personnalisation

### Ajouter des assets :
1. Placez vos images PNG dans le dossier `Assets/`
2. Placez vos fichiers audio WAV dans le dossier `Sounds/`
3. Modifiez le XAML pour référencer vos nouveaux assets

### Modifier les textes :
Les textes humoristiques sont directement dans le fichier `MainWindow.xaml` et peuvent être modifiés selon vos préférences.

### Ajuster les animations :
Les animations sont définies dans les resources XAML et peuvent être personnalisées (durée, effets, répétitions).

## Technologies utilisées
- **.NET 8** - Framework principal
- **WPF (Windows Presentation Foundation)** - Interface utilisateur
- **XAML** - Markup pour l'UI
- **C#** - Logique applicative
- **Storyboard animations** - Effets visuels

## Notes de développement
- Le jeu gère gracieusement les erreurs audio si les fichiers sons sont manquants
- Les placeholder assets permettent de tester le jeu même sans vraies images
- Le code utilise des techniques WPF modernes pour les animations et transitions

## Licence
Projet éducatif / humoristique - Utilisez et modifiez librement !

---
*"Une expérience scientifique sur la nullité humaine"* 🎮