namespace PeanutToTheDungeon
{
    public static class ProgressionManagement
    {
        public static int currentDungeon = 0;
        public static Blub[] allBlubs = new Blub[3];
        public static Ghosty[] allGhosties = new Ghosty[1];
        public static BoxFloof[] allBoxFloofs = new BoxFloof[1];
        public static FuelPot[] allFuelPots = new FuelPot[1];
        public static Dungeon[] allDungeons = new Dungeon[5];
        public static Dungeon CurrentDungeon()
        {
            return allDungeons[currentDungeon];
        }

        public static void InitializeClassArrays()
        {
            //tutorialDungeon
            Blub blub1 = new(1760, 2560, 2080, 2560, false);
            Blub blub2 = new(960, 1280, 1440, 1280, false);
            Blub blub3 = new(1440, 640, 1440, 320, false);
            FuelPot fuelPot1 = new(1120, 1760, false);
            //dungeon1
            Blub blub4 = new(-1600, 1760, 0, 1760, true);
            Blub blub5 = new(0, 3520, 640, 3520, true);
            Blub blub6 = new(-3040, 4960, -2400, 4960, true);
            Blub blub7 = new(320, 5600, 800, 5600, true);
            Blub blub8 = new(1920, 6080, 2240, 6080, true);
            Blub blub9 = new(1280, 4160, 1760, 4160, true);
            Ghosty ghosty1 = new(-800, 3840, -1760, 4800, true);
            Ghosty ghosty2 = new(-1760, 5760, -1280, 6240, true);
            Ghosty ghosty3 = new(1120, 4960, 1600, 5440, true);
            Ghosty ghosty4 = new(2240, 4160, 1440, 3360, true);
            Ghosty ghosty5 = new(2560, 4160, 3360, 3360, true);
            FuelPot fuelPot2 = new(0, 5280, true);
            FuelPot fuelPot3 = new(-1600, 2880, true);
            FuelPot fuelPot4 = new(-2240, 6080, true);
            FuelPot fuelPot5 = new(2560, 6080, true);
            FuelPot fuelPot6 = new(-800, 4800, true);
            //dungeon2
            Blub blub10 = new(-1120, 2240, 320, 2240, true);
            Blub blub11 = new(800, 2880, 2240, 2880, true);
            Blub blub12 = new(2560, 2880, 3520, 2880, true);
            Blub blub13 = new(1440, -640, 2720, -640, true);
            Blub blub14 = new(4480, 1920, 4480, 480, true);
            Ghosty ghosty6 = new(1600, 480, 2080, 0, true);
            Ghosty ghosty7 = new(1600, 2400, 2080, 1920, true);
            Ghosty ghosty8 = new(3520, 640, 4000, 160, true);
            Ghosty ghosty9 = new(-1120, 960, -480, 320, true);
            FuelPot fuelPot7 = new(-1580, 1280, true);
            FuelPot fuelPot8 = new(820, 2080, true);
            FuelPot fuelPot9 = new(2420, 1440, true);
            FuelPot fuelPot10 = new(2260, 480, true);
            FuelPot fuelPot11 = new(820, -800, true);
            FuelPot fuelPot12 = new(3060, 0, true);
            //dungeon3
            Blub blub15 = new(3200, -480, 4160, -480, true);
            Blub blub16 = new(2400, -1920, 2400, -640, true);
            Ghosty ghosty10 = new(4480, -1920, 5120, -1280, true);
            Ghosty ghosty11 = new(1760, -960, 1120, -1600, true);
            BoxFloof boxFloof1 = new(1920, 640, 2400, 640, 2400, 160, true);
            BoxFloof boxFloof2 = new(4320, -160, 4320, 320, 3840, 320, true);
            BoxFloof boxFloof3 = new(5760, -160, 5760, 320, 5280, 320, true);
            BoxFloof boxFloof4 = new(3840, -2080, 3840, -2720, 3200, -2080, true);
            FuelPot fuelPot13 = new(1300, -1920, true);
            FuelPot fuelPot14 = new(3860, -1280, true);
            FuelPot fuelPot15 = new(6740, -1120, true);
            FuelPot fuelPot16 = new(4820, 320, true);
            //dungeon4
            Blub blub17 = new(3360, 3040, 3360, 4960, true);
            Blub blub18 = new(5120, 1760, 7840, 1760, true);
            Blub blub19 = new(6720, 3200, 6720, 3840, true);
            Blub blub20 = new(7040, 6560, 4160, 6560, true);
            Blub blub21 = new(3040, 6720, 1760, 6720, true);
            Ghosty ghosty12 = new(3360, 320, 4000, 960, true);
            Ghosty ghosty13 = new(4160, 2240, 4640, 2720, true);
            Ghosty ghosty14 = new(7520, 4000, 8160, 3360, true);
            Ghosty ghosty15 = new(6560, 4480, 5280, 5760, true);
            Ghosty ghosty16 = new(2720, 5440, 2240, 4960, true);
            Ghosty ghosty17 = new(1120, 5440, 800, 5120, true);
            BoxFloof boxFloof5 = new(1760, 2240, 2400, 2240, 1760, 1600, true);
            BoxFloof boxFloof6 = new(2400, 3360, 2720, 3360, 2720, 3680, true);
            BoxFloof boxFloof7 = new(2240, 4320, 2240, 4640, 2560, 4640, true);
            BoxFloof boxFloof8 = new(1600, 7200, 1600, 7520, 1920, 7520, true);
            BoxFloof boxFloof9 = new(4000, 7200, 4000, 7520, 4320, 7520, true);
            BoxFloof boxFloof10 = new(4160, 6080, 4800, 6080, 4160, 5440, true);
            BoxFloof boxFloof11 = new(6080, 3040, 6560, 3040, 6080, 2560, true);
            BoxFloof boxFloof12 = new(6080, 960, 6560, 960, 6560, 1440, true);
            FuelPot fuelPot17 = new(1000, 2090, true);
            FuelPot fuelPot18 = new(1000, 3690, true);
            FuelPot fuelPot19 = new(3560, 2090, true);
            FuelPot fuelPot20 = new(5480, 490, true);
            FuelPot fuelPot21 = new(7560, 650, true);
            FuelPot fuelPot22 = new(6760, 2730, true);
            FuelPot fuelPot23 = new(8040, 4650, true);
            FuelPot fuelPot24 = new(5480, 4330, true);
            FuelPot fuelPot25 = new(4200, 4490, true);
            FuelPot fuelPot26 = new(7080, 6090, true);
            FuelPot fuelPot27 = new(3080, 7530, true);
            FuelPot fuelPot28 = new(1640, 5770, true);
            //Final Pillow Room
            FuelPot fuelPot29 = new(1460, 2240, true);
            FuelPot fuelPot30 = new(3060, 2240, true);
            FuelPot fuelPot31 = new(3060, 1120, true);
            FuelPot fuelPot32 = new(1460, 1120, true);

            allBlubs = new[] { blub1, blub2, blub3, blub4, blub5, blub6, blub7, blub8, blub9, blub10, blub11, blub12, blub13, blub14, blub15, blub16, blub17, blub18, blub19, blub20, blub21 };
            allGhosties = new[] { ghosty1, ghosty2, ghosty3, ghosty4, ghosty5, ghosty6, ghosty7, ghosty8, ghosty9, ghosty10, ghosty11, ghosty12, ghosty13, ghosty14, ghosty15, ghosty16, ghosty17 };
            allBoxFloofs = new[] { boxFloof1, boxFloof2, boxFloof3, boxFloof4, boxFloof5, boxFloof6, boxFloof7, boxFloof8, boxFloof9, boxFloof10, boxFloof11, boxFloof12 };
            allFuelPots = new[] { fuelPot1, fuelPot2, fuelPot3, fuelPot4, fuelPot5, fuelPot6, fuelPot7, fuelPot8, fuelPot9, fuelPot10, fuelPot11, fuelPot12, fuelPot13, fuelPot14, fuelPot15, fuelPot16, fuelPot17, fuelPot18, fuelPot19, fuelPot20, fuelPot21, fuelPot22, fuelPot23, fuelPot24, fuelPot25, fuelPot26, fuelPot27, fuelPot28, fuelPot29, fuelPot30, fuelPot31, fuelPot32 };
        }
    }
}
