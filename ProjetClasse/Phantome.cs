using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Phantome:Element
    {
        Brush brosse;
        System.DateTime vulnerableJusque;
        public char mode;
        public bool estMort;
        public char modeInitial;
        
        //Corps                       //0,0---------------------------0,32 taille pixel
        Point tete = new Point(16, 0);//|              .tete.         |
        Point p0 = new Point(1, 5);   //|        ..              ..   |     
        Point p1 = new Point(1, 32);  //|      p0                  p8 |
        Point p2 = new Point(6, 27);  //|      |                    | |
        Point p3 = new Point(11, 32); //|      |                    | |
        Point p4 = new Point(16, 27); //|      |                    | |
        Point p5 = new Point(21, 32); //|      |   p2    p4     p6  | |
        Point p6 = new Point(26, 27); //|      |  /  \  /  \   /  \ | |
        Point p7 = new Point(31, 32); //|      p1     p3    p5     p7 |
        Point p8 = new Point(31, 5);  //|32,0------------------------32,32
        Point[] points;
        public Phantome(Brush brosse,char mode,bool estVulnerable,int posX,int posY, char symbol, char direction) :base(posX,posY,symbol,direction,estVulnerable)
        {
            this.brosse = brosse;
            this.mode = mode;
            estMort = false;
            modeInitial = mode;
            points = new Point[10] { tete, p0, p1, p2, p3, p4, p5, p6, p7, p8 };
            

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            dessineElement(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }

        protected override void dessineElement(Graphics g)
        {
            if (estVulnerable)
            {
                if (System.DateTime.Now.Subtract(vulnerableJusque).TotalSeconds > 30 && !estMort)
                {
                    estVulnerable = false;
                    mode = modeInitial;
                }
            }
            
            estMort = (mode == 'm');
            //corps
            if(!estMort)g.FillPolygon((estVulnerable) ? Brushes.White : brosse, points);
            //Yeux
            g.FillPie(Brushes.Black, 9, 7, 7, 7, 0, 360);
            g.FillPie(Brushes.Black, 20, 7, 7, 7, 0, 360);
            //Popieres
            g.FillPie(Brushes.White, 10, 8, 4, 4, 0, 360);
            g.FillPie(Brushes.White, 21, 8, 4, 4, 0, 360);
            g.Dispose();
        }
        public void rendVulnerable()
        {
            if (!estMort)
            {
                estVulnerable = true;
                mode = 'e';
                vulnerableJusque = System.DateTime.Now;
            }
            
        }
        public void revivre()
        {
            estMort = false;
            mode = modeInitial;
            direction = directionInitial;
            estVulnerable = false;
        }
    }
}
