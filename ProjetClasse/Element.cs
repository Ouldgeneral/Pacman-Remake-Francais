using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    abstract class Element:Panel
    {
        public int posX;
        public int posY;
        public char direction;
        public char symbol;
        public Point positionInitial;
        public bool estVulnerable;
        public char directionInitial;
        public int derinierX;
        public int dernierY;
        public Element(int posX,int posY, char symbol,char direction,bool estVulnerable)
        {
            this.posX = posX;
            this.posY = posY;
            this.symbol= symbol;
            this.direction = direction;
            this.estVulnerable = estVulnerable;
            this.positionInitial = new Point(posX, posY);
            directionInitial = direction;
            this.derinierX = posX;
            this.dernierY = posY;


        }
        protected abstract void dessineElement(Graphics g);
        public void actualiser(int x,int y,char direction)
        {
            dernierY = posY;
            derinierX = posX;
            posY = y;
            posX = x;
            this.direction = direction;
        }
        public void reInitialise()
        {
            posX = positionInitial.X;
            posY = positionInitial.Y;
            derinierX = posX;
            dernierY = posY;
            direction = directionInitial;
            estVulnerable = false;
        }
    }
    
}
