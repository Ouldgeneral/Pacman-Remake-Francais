

using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Pacman:Element
    {
        SoundPlayer pacmanMangeFruit = new SoundPlayer(Voix.pacman_eatfruit);
        SoundPlayer pacmanMangePhantome = new SoundPlayer(Voix.pacman_eatghost);
        SoundPlayer pacmanMort = new SoundPlayer(Voix.pacman_death);
        Brush brosse = Brushes.Yellow;
        int angle;

        public Pacman(int posX,int posY, char symbol,char direction,bool estVulnerable): base(posX, posY, symbol,direction,estVulnerable)
        {
            
        }
       
        public void mangeFruit()
        {
            pacmanMangeFruit.Play();
        }
        public void mangePhantome()
        {
            pacmanMangePhantome.Play();
        }
        public void mourir()
        {
            pacmanMort.Play();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            dessineElement(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        protected override void dessineElement(Graphics g)
        {
            
            switch (direction)
            {
                case 'h': angle = 300; break;//haut
                case 'g': angle = 210; break;//gauche
                case 'b':angle = 120;break;//bas
                default:angle = 30;break;//droite
            }
            g.FillPie(brosse, 0, 0, 32, 32, angle, 300);
            g.Dispose();
        }
    }
}
