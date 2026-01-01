using System.Windows.Forms;
using System.Drawing;

namespace ProjetClasse
{
    class Nourriture:Panel
    {
        int rayon;
        public int x;
        public int y;
        Brush brosse = Brushes.White;
        public Nourriture(int rayon,int x,int y)
        {
            this.rayon = rayon;
            this.x = x;
            this.y = y;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            dessinerNourriture(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void dessinerNourriture(Graphics g)
        {
            
            g.FillEllipse(brosse, 16, 16, rayon, rayon);
            g.Dispose();
        }
    }
}
