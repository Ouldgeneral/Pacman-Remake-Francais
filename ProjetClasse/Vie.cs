

using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Vie:Panel
    {
        Brush brosse;
        Point p1;
        Point p2;
        Point p3;
        Point[] points;
        public Vie()
        {
            brosse = Brushes.Red;
            p1 = new Point(0, 2);
            p2 = new Point(8, 16);
            p3 = new Point(16, 2);
            points = new Point[3] { p1,p2,p3};

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            dessinerVie(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void dessinerVie(Graphics g)
        {
            
            g.FillPie(brosse, 0, 0, 8, 5, 180, 180);
            g.FillPie(brosse, 8, 0, 8, 5, 180, 180);
            g.FillPolygon(brosse, points);
            g.Dispose();
        }
    }
}
