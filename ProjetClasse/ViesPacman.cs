

using System.Windows.Forms;

namespace ProjetClasse
{
    
    class ViesPacman:Panel
    {
        public int vies;
        public int viesInitial;
        public ViesPacman(int lives)
        {
            this.vies = lives;
            this.viesInitial = lives;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < vies; i++)
            {
                Vie vie = new Vie();
                vie.SetBounds(i * 16, 0, 16, 16);
                Controls.Add(vie);
            }
            base.OnPaint(e);
            e.Dispose();
        }
    }
}
