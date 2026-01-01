

using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Brique:Panel
    {
        Brush brosse = Brushes.Brown;
        protected override void OnPaint(PaintEventArgs e)
        {
            dessineBrique(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void dessineBrique(Graphics g)
        {
            
            g.FillRectangle(brosse,0,1,32,31);
            g.DrawRectangle(Pens.Black, 0, 11, 32, 10);
            g.DrawLine(Pens.Black, 16, 1, 16, 11);
            g.DrawLine(Pens.Black, 16, 21, 16, 31);
            g.Dispose();

        }
    }
}
