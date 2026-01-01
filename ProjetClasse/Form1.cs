using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace ProjetClasse
{
    public partial class EspaceJeu : Form 
    {
        
        static int LONGUEUR;//longueur
        static int LARGEUR;//largeur
        static int TAILLE_PIXEL=32;//taille pixel a occuppe( a ne pas changer tout nos calcules de taille sont proportionnels a 32)
        int niveauActuel;//le niveau actuel
        int nombreNourriture;//nombre de nouriture
        Niveaux niveaux;//les niveaux de jeux
        Score score;//score
        Timer timer;//timer pour la mise a jour graphique
        char[][] carte;//carte charge selon le niveau
        Nourriture[,] nourritures;//nouriture pacman
        ViesPacman vies;//nombre de vie du joueur
        Dictionary<char, char> directionsInverse;
        Dictionary<char, Phantome> phantomes;
        Dictionary<char,Brush> brosses;
        SoundPlayer interMission = new SoundPlayer(Voix.pacman_intermission);
        SoundPlayer debutJeu = new SoundPlayer(Voix.pacman_beginning);
        Pacman pacman;
        bool jeuEnCours;
        TexteJeu texte;
        public EspaceJeu()
        {


            /**
             * Pacman Remake de Malick Ould Hamdi et Khene Rafik 
             * Ce remake est presente comme projet de fin de semestre 2 de la specialite DMPF a l'iteem
             * Cette copie de code est la seule copie originale du travail demender 
             * Toute modifiaction dans le code qui entrainent son disfonctionnement n'est pas notre responsabilite
             * Alger le 5/12/2025
             * */
            //initialisation des composantes
            brosses = new Dictionary<char, Brush>();
            brosses.Add('r', Brushes.Red);
            brosses.Add('b', Brushes.Blue);
            brosses.Add('o', Brushes.Orange);
            brosses.Add('p', Brushes.Pink);
            directionsInverse = new Dictionary<char, char>();//ce direction est utilise apres pour eviter le retour en arriere des phantome
            directionsInverse.Add('h', 'b');//haut inverse de bas ainsi de suite
            directionsInverse.Add('d', 'g');
            directionsInverse.Add('b', 'h');
            directionsInverse .Add('g', 'd');
            niveauActuel = 2;
            texte = new TexteJeu();
            niveaux = new Niveaux();//initialisations des niveaux
            vies = new ViesPacman(10);//nombre de vie du joueur
            score = new Score(0);//score par defaut 0
            InitializeComponent();
            construitCarte();//Construction initial de la carte
            this.KeyPreview = true;
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            
        }
        
        private void construitCarte()
        {
            phantomes = new Dictionary<char, Phantome>();
            texte.SetBounds(280, 352, 150, 16);
            Controls.Add(texte);
            jeuEnCours = false;
            debutJeu.Play();
            carte = niveaux.chargerNiveau(niveauActuel);
            niveaux.niveau = niveauActuel;
            LARGEUR = carte[0].Length;//recuperation de la largeur de la carte carte
            LONGUEUR = carte.Length;//recuperation de la longueur de la carte carte
            this.Width = LARGEUR * TAILLE_PIXEL+TAILLE_PIXEL/2;
            this.Height = LONGUEUR * TAILLE_PIXEL+TAILLE_PIXEL*2;
            nombreNourriture = 0;
            nourritures = new Nourriture[LONGUEUR, LARGEUR];//matrices de la nouriture 
            vies.vies = vies.viesInitial;
            score.score = score.scoreInitial;
            vies.SetBounds(TAILLE_PIXEL*(LARGEUR-vies.vies), 0, 16*(vies.vies), 16);
            Controls.Add(vies);
            score.SetBounds(0, 0, 60, 15);
            score.BackColor = Color.Brown;
            Controls.Add(score);
            niveaux.SetBounds(0, 16, 60, 15);
            niveaux.BackColor = Color.Brown;
            Controls.Add(niveaux);
            char place;
            //Emplacement des caracteres principales d'abord (phantomes et Pacman)
            //Emplacement des murs qui permettront de controler le deplacement des caracteres et de la nourriture pour pacman
            for (int i = 0; i < LONGUEUR; i++)
            {
                for (int j = 0; j < LARGEUR; j++)
                {
                    place = carte[i][j];
                    if ("rbpo".ToCharArray().Contains(place))//'r'=rouge 'b'=bleu 'p'=rose 'o'=orange
                    {
                        char mode = (place == 'b' || place == 'p') ? 'c' : 'b';
                        Phantome phantome = new Phantome(brosses[place], mode, false, j, i, place, 'd');
                        phantome.Location = new Point(j * TAILLE_PIXEL, i * TAILLE_PIXEL);
                        phantome.SetBounds(j * TAILLE_PIXEL, i * TAILLE_PIXEL, TAILLE_PIXEL, TAILLE_PIXEL);
                        Controls.Add(phantome);
                        phantomes.Add(place, phantome);
                    }
                    else if (place == 'P')//'P'=pacman
                    {
                        pacman = new Pacman(j, i, 'P', 'g', true);
                        pacman.Location = new Point(j * TAILLE_PIXEL, i * TAILLE_PIXEL);
                        pacman.SetBounds(j * TAILLE_PIXEL, i * TAILLE_PIXEL, TAILLE_PIXEL, TAILLE_PIXEL);
                        Controls.Add(pacman);
                    }
                    else if (place == 'X')//'X'=brique
                    {
                        Brique brique = new Brique();
                        brique.SetBounds(j * TAILLE_PIXEL, i * TAILLE_PIXEL, TAILLE_PIXEL, TAILLE_PIXEL);
                        Controls.Add(brique);
                    }
                    else if (place == 'v' || place =='V')//v=nouriture simple V=nouriture de force
                    {
                        Nourriture nourriture = new Nourriture(place=='v'?5:10,j,i);
                        nourriture.SetBounds(j * TAILLE_PIXEL, i * TAILLE_PIXEL, TAILLE_PIXEL, TAILLE_PIXEL);
                        Controls.Add(nourriture);
                        nourritures[i, j] = nourriture;
                        nombreNourriture++;
                    }

                }
            }
            if(pacman!=null)pacman.BringToFront();
            foreach(Phantome phantome in phantomes.Values)
            {
                phantome.BringToFront();
            }
        }
        private void niveauSuivant()
        {
            jeuEnCours = false;
            interMission.Play();
            timer.Stop();
            DialogResult r = MessageBox.Show("Vous avez gagné votre score est:" + score.score + "\nVoulez vous jouer au niveau suivant",
                "Félicitations",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                score.scoreInitial = score.score;
                niveauActuel++;
                
            }else if (r != DialogResult.No)
            {
                this.Close();
            }
            jeuEnCours = true;
            Controls.Clear();
            construitCarte();
            timer.Start();
            
        }
        private void reInitialiseElement(Element element)
        {
            carte[element.posY][element.posX]='v';
            element.reInitialise();
            element.Location = new Point(element.posX * TAILLE_PIXEL, element.posY * TAILLE_PIXEL);

        }
        private void jeuTermine()
        {
            
            jeuEnCours = false;
            pacman.mourir();
            timer.Stop();
            DialogResult r = MessageBox.Show("Vous avez perdu votre score est:" + score.score + " \nVoulez vous rejouer",
                "Jeu Termine", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                reInitialiseElement(pacman);
                foreach (Phantome phantome in phantomes.Values) reInitialiseElement(phantome);
                foreach (Nourriture nourriture in nourritures)
                {
                    if (nourriture == null) continue;
                    int x = nourriture.x;
                    int y = nourriture.y;
                    carte = niveaux.chargerNiveau(niveauActuel);
                    if (!Controls.Contains(nourriture))
                    {
                        Controls.Add(nourriture);
                    }
                }
                score.score = 0;
                vies.vies = vies.viesInitial;
                timer.Start();
            }
            else
            {
                this.Close();
            }
        }
        private void Timer_Tick(object sender,EventArgs e)
        {
            if (vies.vies <= 0)
            {
                jeuTermine();
            }
            else if (nombreNourriture <= 0) niveauSuivant();
            if (!jeuEnCours)
            {
                texte.SetBounds(280, 352, 150, 15);
                Controls.Add(texte);
                texte.BringToFront();
                timer.Stop();
                return;
            }
            foreach(Phantome phantome in phantomes.Values)
            {
                bouger(phantome, bougePhantome(phantome));
            }
            bouger(pacman, pacman.direction);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (vies.vies <= 0)
            {
                jeuTermine();
            }
            char direction=pacman.direction;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    jeuEnCours = !jeuEnCours;
                    if (jeuEnCours)
                    {
                        
                        Controls.Remove(texte);
                        timer.Start();
                    }
                    else
                    {
                        
                        texte.SetBounds(280, 352, 150, 15);
                        Controls.Add(texte);
                        texte.BringToFront();
                        timer.Stop();
                    }
                    break;
                case Keys.Up:
                    direction = 'h';//haut
                    break;
                case Keys.Down:
                    direction = 'b';//bas
                    break;
                case Keys.Left:
                    direction = 'g';//gauche
                    break;
                default:
                    direction = 'd';//droite
                    break;
            }
            if (jeuEnCours)bouger(pacman, direction);
            
            
        }
        private void rendPhantomeVulnerable()
        {
            pacman.estVulnerable = false;
            foreach(Phantome phantome in phantomes.Values)
            {
                if (phantome != null) phantome.rendVulnerable();
            }
        }
        private void mourir(Element element)
        {
            if (element.symbol != 'P')
            {
                if (!element.estVulnerable) return;
                tuePhatome((Phantome)element);
                return;
            }
            vies.vies--;
            vies.SetBounds(TAILLE_PIXEL * (LARGEUR - vies.vies), 0, 16 * (vies.vies), 16);
            vies.Refresh();
            carte[element.posY][element.posX] = 'v';
            element.posX = element.positionInitial.X;
            element.posY = element.positionInitial.Y;
            element.estVulnerable = false;
            element.direction = element.directionInitial;
            element.Location = new Point(element.posX * TAILLE_PIXEL, element.posY * TAILLE_PIXEL);
        }
        private void tuePhatome(Phantome phantome)
        {
            pacman.mangePhantome();
            score.score += 50;
            phantome.mode = 'm';
            echangerPlaces(phantome, pacman);
            phantome.Location = new Point(phantome.posX * TAILLE_PIXEL, phantome.posY * TAILLE_PIXEL);
            
        }
        private bool bougeElement(Element element, char direction)
        {
            int x = element.posX;
            int y = element.posY;
            switch (direction)
            {
                case 'h':y = v(y - 1);break;
                case 'b':y = v(y + 1);break;
                case 'g':x = h(x - 1);break;
                case 'd':x = h(x + 1);break;
            }
            char place = carte[y][x];
            if (place == 'v' || place == 'V')
            {
                Nourriture nourriture = nourritures[element.posY, element.posX];

                if (element.symbol == 'P')
                {
                    if (Controls.Contains(nourriture))
                    {
                        if (place == 'V')
                        {
                            rendPhantomeVulnerable();
                            score.score += 10;
                            pacman.mangeFruit();
                        }
                        else
                        {
                            score.score++;
                        }
                        nombreNourriture--;
                        score.Refresh();
                        Controls.Remove(nourriture);
                    }
                }
                carte[element.posY][element.posX] = (element.symbol == 'P') ? 'v' : place;
                carte[y][x] = element.symbol;
                element.actualiser(x,y,direction);
            }
            else if (place != 'X')
            {
                //code de detection de collision
                if (element.symbol == 'P')
                {
                    if (phantomes[place].estVulnerable) tuePhatome(phantomes[place]);
                    else
                    {
                        element.estVulnerable = true;
                        mourir(element);
                    }
                }
                else
                {
                    if (element.estVulnerable && place == 'P') mourir(element);
                    else if (!element.estVulnerable && place == 'P') mourir(pacman);
                    else return false;
                }
            }
            else return false;
            return true;


        }
        private void echangerPlaces(Element element1,Element element2)
        {
            carte[element1.posY][element1.posX] = 'v';
            element1.posY = element2.dernierY;
            element1.posX = element2.derinierX;
            carte[element1.posY][element1.posX] = element1.symbol;
        }
        private void bouger(Element element,char? direction)
        {
            if (element == null || direction==null) return;
            bool aBouger=bougeElement(element,direction.Value);
            if (!aBouger) return;
            element.Location = new Point(element.posX * TAILLE_PIXEL, element.posY * TAILLE_PIXEL);
            element.Refresh();
        }
        char? bougePhantome(Phantome phantome)
        {
            //cette methode est responsable du mouvement des phantomes intelligemment selon le mode elle recoit 4 mode b,m,c,e
            //Elle est le 100% du cerveau du phantome
            //le mode b=balader est le mode de promenade normale dans ce mode le phantome va juste se balader dans le jeu 
            //il n'as pas pacman comme enemmi mais si pacman se dirige vers lui en ce mode pacman sera mort
            //le mode e=effraye mode effraye le phantome va essayer de s'eloigner de pacman
            //la logique de cette mode est de calculer toutes les distances qui mene a pacman et choisir la plus longue pour simuler la fuite
            //le mode c=chasse mode chasse le phantome va essayer de se rapprocher de pacman pour le tuer
            //la logique de cette mode est de calculer toutes les distances qui mene a pacman et choisir la plus courte pour simuler la chasse de pacman
            //le mode m=mort c'est le mode mort du phantome il va essayer de revivre
            //la logique de cette mode est de calculer toutes les distances qui mene a la position initial du phantome et choisir la plus courte pour simuler la resurrection
            if (phantome == null || pacman == null) return null;
            int destinationX = (phantome.mode == 'm') ? phantome.positionInitial.X : pacman.posX;
            int destinationY = (phantome.mode == 'm') ? phantome.positionInitial.Y : pacman.posY;
            int x = phantome.posX;
            int y = phantome.posY;
            double? distance;
            char mode = phantome.mode;
            Dictionary<double, char> directionsPosssible = new Dictionary<double, char>();
            SortedDictionary<double, char> directionsTrier;
            char direction = phantome.direction;
            foreach( char direction1 in directionsInverse.Keys)
            {
                //eviter d'enregistrer les directions inverse pour ne pas tomber dans une boucle infini de mouvement entre deux points
                if (directionsInverse[direction1]==direction) continue;
                distance = calculerDistance(x, y, destinationX, destinationY, direction1, mode);
                if (distance != null)
                {
                    if (mode == 'b') return direction1;//mode b
                    directionsPosssible[distance.Value] = direction1;
                }
            }
            if (directionsPosssible.Count == 0)
            {
                return directionsInverse[direction];
            }
            directionsTrier = new SortedDictionary<double, char>(directionsPosssible);
            if (directionsTrier.First().Key < 2 && phantome.mode == 'm')//mode m
            {
                phantome.revivre();
            }
            if (phantome.mode == 'e') return directionsTrier.Last().Value;//mode e
            return directionsTrier.First().Value;//mode m,c
        }
       
        //calcule la distance entre deux points
        private double? calculerDistance(int x1,int y1,int x2,int y2,char direction,char mode)
        {
            switch (direction)
            {
                case 'h':y1 = v(y1 - 1);break;
                case 'b':y1 = v(y1 + 1);break;
                case 'g':x1 = h(x1 - 1);break;
                case 'd':x1 = h(x1 + 1);break;

            }
            if (carte[y1][x1] == 'P' && mode == 'c') mourir(pacman);
            if (carte[y1][x1] != 'v' && carte[y1][x1] != 'V') return null;
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        //Calcule un emplacement verticale
        private int v(int y)
        {
            return (LONGUEUR + y) % LONGUEUR;
        }
        //calcule un emplacement horizontale
        private int h(int x)
        {
            return (LARGEUR + x) % LARGEUR;
        }
    }
}
