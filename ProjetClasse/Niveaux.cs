
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Niveaux:Panel
    {
        public int niveau;
        List<char[][]> niveaux;
        char[][] niveau1 =
         {

            "XXXXXXXXXXXXXXXXXXXXX".ToCharArray(),
            "XVvvvvvvvvXvvvvvvvvVX".ToCharArray(),
            
            "XvXXXvXXXvXvXXXvXXXvX".ToCharArray(),
            "XvvvvvvvvvVvvvvvvvvvX".ToCharArray(),
            "XvXXXvXvXXXXXvXvXXXvX".ToCharArray(),
            "XVvvvvXvvvXvvvXvvvvVX".ToCharArray(),
            "XXXXXvXXXvXvXXXvXXXXX".ToCharArray(),
            "LLLLXvXvvvvvvvXvXLLLL".ToCharArray(),
            "LLLLXvXvXXoXXvXvXLLLL".ToCharArray(),
            "XXXXXvvvXrbpXvvvXXXXX".ToCharArray(),
            "vvvvvvXvXXXXXvXvvvvvv".ToCharArray(),
            "XXXXXvXvvvVvvvXvXXXXX".ToCharArray(),
            "XVvvvvvvXXvXXvvvvvvVX".ToCharArray(),
            "XvXXXvvvvXvXvvvvXXXvX".ToCharArray(),
            "XvvXXvvvvvPvvvvvXXvvX".ToCharArray(),
            "XXvvXvXvXXXXXvXvXvvXX".ToCharArray(),
            "XvvvvvXvvvXvvvXvvvvvX".ToCharArray(),
            "XvXXXXXXXvXvXXXXXXXvX".ToCharArray(),
            "XVvvvvvvvvvvvvvvvvvVX".ToCharArray(),
            "XXXXXXXXXXXXXXXXXXXXX".ToCharArray(),
            

        };
        char[][] niveau2=
         {

            "XXXXXXXXXXXvXXXXXXXXX".ToCharArray(),
            "XXVvvvvvvvvvvvvvvvvVX".ToCharArray(),
            "XXvXXXXXvXXvXXXXXvXvX".ToCharArray(),
            "vvvvvvvvvXXvvvvvXvXvv".ToCharArray(),
            "XXvXXXXXXXXvXXXvXvXvX".ToCharArray(),
            "XXvvvvvvvvvvXXXvXvXvX".ToCharArray(),
            "lXvXXXvXXvXvXXXvXvXVX".ToCharArray(),
            "lXvvvXvvvrbpovvvXvXXX".ToCharArray(),
            "lXvXXXvXvvvvvXXvXvXXX".ToCharArray(),
            "lXvvvvvXXXXXXXXvvvvvX".ToCharArray(),
            "lXvXXvXXXXXvvvvvXvXvX".ToCharArray(),
            "lXvXXvvvPvvvXvXvXvXvX".ToCharArray(),
            "lXvvvvXXvXXXXvXvXvXvX".ToCharArray(),
            "lXVXXvvvvvvvXvXvXvXvX".ToCharArray(),
            "XXvXXvXXXXXvXvXvXvXvX".ToCharArray(),
            "vvvvvvvvvvvvvvvvvvvvv".ToCharArray(),
            "XXvXXvXXXXXXXvXvXXXXX".ToCharArray(),
            "XvvvvvvvvvvvvvXvvvvVX".ToCharArray(),
            "XXXXXXXXXXXVXXXXXXXXX".ToCharArray()
        };
        public Niveaux()
        {
            
            niveaux = new List<char[][]>();
            niveaux.Add(niveau1);
            niveaux.Add(niveau2);
        }
        public char[][] chargerNiveau(int niveau)
        {
            this.niveau = niveau;
            if (niveau <=1 || niveau -1> niveaux.Count - 1) return niveau1;
            return niveaux[niveau - 1];
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            dessinerNiveau(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void dessinerNiveau(Graphics g)
        {
            if (niveau <= 1 || niveau - 1 > niveaux.Count - 1)niveau=1;
            TextRenderer.DrawText(g,"Niveau:" + niveau, this.Font,new Point(0,0), Color.White,Color.Transparent);
            g.Dispose();
        }
    }
}
