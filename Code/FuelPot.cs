namespace PeanutToTheDungeon
{
    public class FuelPot
    {
        //Other management variables
        public bool hidden = false;
        public int X = 0;
        public int Y = 0;
        public int ticks = 0;

        //Collision
        public Rectangle hitbox = new(0, 0, 120, 140);
        public Rectangle fuelbox = new(0, 0, 160, 160);

        //Default image
        public Image nextFuelPot = Properties.Resources.fuelPot1;

        public FuelPot(int aX, int aY, bool aHidden)
        {
            X = aX;
            Y = aY;
            hitbox.Location = new Point(aX + 20, aY + 10);
            fuelbox.Location = new Point(aX, aY);
            hidden = aHidden;
        }
    }
}
