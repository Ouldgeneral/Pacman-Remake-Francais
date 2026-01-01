using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Score:Panel
    {
        public int score;
        public int scoreInitial;

        public Score(int score)
        {
            this.score = score;
            this.scoreInitial = score;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            dessineScore(e.Graphics);
            e.Dispose();
        }
        private void dessineScore(Graphics g)
        {

            TextRenderer.DrawText(g, "Score:" + score, this.Font, new Point(0, 0), Color.White, Color.Transparent);
            g.Dispose();
        }
    }
}
