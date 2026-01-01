
using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class TexteJeu:Panel
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            dessinerTexteJeu(e.Graphics);
            base.OnPaint(e);
        }
        private void dessinerTexteJeu(Graphics g)
        {
            TextRenderer.DrawText(g, "Clicker sur espace pour jouer", this.Font, new Point(0, 0), Color.White, Color.Transparent);
            g.Dispose();
        }
    }
}
