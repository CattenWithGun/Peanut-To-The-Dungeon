namespace PeanutToTheDungeon
{
    public class DungeonCollision
    {
        public Rectangle[] collidables;
        public Point[] offsets;
        public int xOffset;
        public int yOffset;

        public DungeonCollision(Rectangle[] aCollidables, Point[] aOffsets, int aXOffset, int aYOffset)
        {
            collidables = aCollidables;
            offsets = aOffsets;
            xOffset = aXOffset;
            yOffset = aYOffset;
        }
        public void ScrollCollision(int scrollX, int scrollY)
        {
            for(int i = 0; i < collidables.Length; i++)
            {
                collidables[i].Location = new Point(offsets[i].X + scrollX + xOffset, offsets[i].Y + scrollY + yOffset);
            }
        }
    }
}
