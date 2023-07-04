namespace PeanutToTheDungeon
{
    public class Blub
    {
        //Image and hitbox
        public Image nextFrame;
        public Rectangle hitbox;

        //Hitbox Coordinates
        public int X;
        public int Y;

        //Movement Point Coordinates
        public int point1X;
        public int point1Y;

        public int point2X;
        public int point2Y;

        //Animation Ticks
        public int ticks;

        //Jumpscare Flags
        public bool disappear;
        public bool hidden;

        public Blub(int aPoint1X, int aPoint1Y, int aPoint2X, int aPoint2Y, bool aHidden)
        {
            //Sets variables
            X = aPoint1X;
            Y = aPoint1Y;
            point1X = aPoint1X;
            point1Y = aPoint1Y;
            point2X = aPoint2X;
            point2Y = aPoint2Y;
            hidden = aHidden;

            //Creates the blub
            hitbox.Width = 160;
            hitbox.Height = 160;
            X = point1X;
            Y = point1Y;
            hitbox.Location = new Point(point1X, point1Y);
            ticks = 0;
            nextFrame = Properties.Resources.blubRight1;
        }
    }
}
