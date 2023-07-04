namespace PeanutToTheDungeon
{
    public class Dungeon
    {
        //Declares the variables
        public int levelNumber = 0;
        public int pillowX = 0;
        public int pillowY = 0;
        public int pillowOffsetX = 0;
        public int pillowOffsetY = 0;
        public int xOffset = 0;
        public int yOffset = 0;
        private readonly Rectangle[] collidables;
        private readonly Point[] offsets;
        public Image levelDark;
        public Image levelLight;
        public DungeonCollision collision;
        //Sets the variables
        public Dungeon(int aLevelNumber, int aPillowX, int aPillowY, int aPillowOffsetX, int aPillowOffsetY, int aXOffset, int aYOffset, Rectangle[] aCollidables, Image aLevelDark, Image aLevelLight, Point[] aOffsets)
        {
            levelNumber = aLevelNumber;
            pillowX = aPillowX;
            pillowY = aPillowY;
            pillowOffsetX = aPillowOffsetX;
            pillowOffsetY = aPillowOffsetY;
            xOffset = aXOffset;
            yOffset = aYOffset;
            collidables = aCollidables;
            levelDark = aLevelDark;
            levelLight = aLevelLight;
            offsets = aOffsets;
            collision = new DungeonCollision(collidables, offsets, xOffset, yOffset);
        }
    }
}
