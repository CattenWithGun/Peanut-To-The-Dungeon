using NAudio.Wave;
using System.Drawing.Drawing2D;

namespace PeanutToTheDungeon
{
    public partial class Form1 : Form
    {
        /*
                Dynamic
        */

        //Peanut Control Flags
        private static bool rightHeld = false;
        private static bool leftHeld = false;
        private static bool upHeld = false;
        private static bool downHeld = false;
        private static bool rightLastHeld = true;
        private static bool leftLastHeld = false;
        private static bool isSleeping = false;
        private static bool isLoafSleeping = false;

        //Peanut Animation Ticks
        private static int peanutTicks = 0;
        private static int peanutSleepTicks = 0;

        //Enemy jumpscare ticks
        private static int blubJumpTicks = 0;
        private static int ghostyJumpTicks = 0;
        private static int boxFloofJumpTicks = 0;

        //GameEnd Values
        private static bool isRestarting = false;
        private static int endTicks = 0;

        //Scroll Values
        private static int scrollX = 500;
        private static int scrollY = -1920;

        //Peanut Management Values
        private static int realPeanutX = 480;
        private static int realPeanutY = 2560;
        private static int peanutNextLevelTicks = 0;

        //Peanut
        private static Rectangle peanutHitbox = new(978, 652, 170, 140);

        //Light Boxes
        private static Rectangle lightBox = new(0, 0, 48, 48);
        private static Rectangle darkBox = new(0, 0, 80, 80);

        //UI
        private static Image nextScaredMeter = Properties.Resources.ScaredMeter1;
        private static Image nextLanternFuel = Properties.Resources.LanternFuel1;

        //GameEnd
        private static Image nextEndFrame = Properties.Resources.end1;

        //Enemy Jumpscares
        private static Image nextBlubJumpFrame = Properties.Resources.blubJump1;
        private static Image nextGhostyJumpFrame = Properties.Resources.ghostyJump1;
        private static Image nextBoxFloofJumpFrame = Properties.Resources.boxFloofJump1;

        //Peanut
        private static Image nextPeanutFrame = Properties.Resources.peanutRunningRight1;

        //Regions
        private static Region lightCircle = new();
        private static Region outerLightCircle = new();

        //UI Values
        private static int scaredMeter = 0;
        private static int lanternFuel = 100;
        private static int winTicks = 0;
        private static int menuTicks = 0;

        //Sound Played Flags
        private static bool disappointedMeowPlayed = false;
        private static bool blubJumpscarePlayed = false;
        private static bool ghostyJumpscarePlayed = false;
        private static bool boxFloofJumpscarePlayed = false;
        private static bool winSoundPlayed = false;

        //Global disappear flag, for limiting jumpscare checks during the graphics processing
        private static bool globalDisappear = false;

        /*
                Constant
        */

        //Graphics Paths
        private static readonly GraphicsPath lightPath1 = new();
        private static readonly GraphicsPath lightPath2 = new();
        private static readonly GraphicsPath excludePath = new();
        private static readonly GraphicsPath darkExcludePath = new();

        //Meter clipping rectangles
        private static readonly Rectangle scaredRectangle = new(10, 1290, 420, 110);
        private static readonly Rectangle lanternRectangle = new(1830, 1290, 420, 110);

        //Peanut Hitbox Values
        private static readonly int peanutHitboxX = 978;
        private static readonly int peanutHitboxY = 652;

        //Dungeons
        private static Rectangle pillow = new(2080, 960, 80, 80);
        private static readonly Dungeon tutorialDungeon = new(0, 2120, 1020, -60, -80, 0, 10, new Rectangle[] { new Rectangle(0, 0, 2400, 160), new Rectangle(0, 0, 160, 1120), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 960), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 960), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 960, 160), new Rectangle(0, 0, 480, 320), new Rectangle(0, 0, 160, 640), new Rectangle(0, 0, 160, 1280), new Rectangle(0, 0, 320, 480), new Rectangle(0, 0, 480, 480), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 1920, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160) }, Properties.Resources.tutorialFloorDark, Properties.Resources.tutorialFloor, new Point[] { new Point(160, 160), new Point(160, 320), new Point(320, 320), new Point(960, 480), new Point(2240, 320), new Point(2400, 320), new Point(1440, 960), new Point(1600, 640), new Point(320, 1280), new Point(640, 800), new Point(1920, 1280), new Point(800, 1440), new Point(2240, 1600), new Point(1600, 1920), new Point(1120, 2080), new Point(320, 2240), new Point(160, 2400), new Point(320, 2880), new Point(800, 2720), new Point(1600, 2720), new Point(960, 2080) });
        private static readonly Dungeon dungeon1 = new(1, 2760, 5340, 3480, -1360, 0, -10, new Rectangle[] { new Rectangle(0, 0, 2880, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 1760), new Rectangle(0, 0, 480, 1120), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 1280), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 640, 1280), new Rectangle(0, 0, 640, 480), new Rectangle(0, 0, 640, 800), new Rectangle(0, 0, 960, 480), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 1600, 960), new Rectangle(0, 0, 2400, 160), new Rectangle(0, 0, 800, 800), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 480, 640), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 160, 1280), new Rectangle(0, 0, 160, 640), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 1120, 480), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 3360, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 960), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 1120, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 1280, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 800), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 640, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 480, 480), new Rectangle(0, 0, 640, 480), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 640, 320), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 1120, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 1120, 320), new Rectangle(0, 0, 320, 480), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 1600, 160), new Rectangle(0, 0, 160, 3040) }, Properties.Resources.darkDungeon1, Properties.Resources.dungeon1, new Point[] { new Point(-1920, 1460), new Point(-1760, 1620), new Point(-1920, 1620), new Point(-1280, 2100), new Point(-1280, 3220), new Point(480, 1620), new Point(800, 1620), new Point(-480, 2100), new Point(-800, 2580), new Point(-320, 2100), new Point(320, 2900), new Point(800, 3380), new Point(320, 3860), new Point(-640, 3700), new Point(160, 3860), new Point(-480, 4180), new Point(1440, 3220), new Point(-2400, 3220), new Point(-1600, 3700), new Point(-1280, 3860), new Point(-2240, 4020), new Point(-3200, 3540), new Point(-3200, 3700), new Point(-3360, 3700), new Point(-3200, 4180), new Point(-2720, 4180), new Point(-2560, 4820), new Point(-3040, 5300), new Point(-2880, 5460), new Point(-2560, 5940), new Point(-2400, 6420), new Point(-2240, 4980), new Point(-1280, 4980), new Point(-1120, 4500), new Point(-960, 5300), new Point(-800, 5620), new Point(-480, 5300), new Point(-320, 5460), new Point(-160, 5140), new Point(-960, 5780), new Point(-640, 6100), new Point(0, 6260), new Point(800, 5780), new Point(960, 5620), new Point(1280, 5940), new Point(1280, 6100), new Point(1920, 6260), new Point(1760, 5620), new Point(2080, 5460), new Point(1280, 4820), new Point(1440, 4500), new Point(1440, 4660), new Point(2080, 3380), new Point(2240, 3860), new Point(3040, 4180), new Point(3680, 4020), new Point(3200, 4180), new Point(2400, 4660), new Point(2400, 4340), new Point(2240, 4820), new Point(3040, 5140), new Point(3680, 4980), new Point(3040, 5460), new Point(2240, 5620), new Point(2240, 5140), new Point(2880, 5940), new Point(2880, 6260), new Point(3200, 6100), new Point(2240, 6420), new Point(3840, 3380) });
        private static readonly Dungeon dungeon2 = new(2, 4540, 2780, 2000, 1350, 20, 10, new Rectangle[] { new Rectangle(0, 0, 1120, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 800, 320), new Rectangle(0, 0, 2240, 480), new Rectangle(0, 0, 960, 480), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 2400, 480), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 640, 320), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 640, 320), new Rectangle(0, 0, 4320, 160), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 160, 1440), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 160, 960), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 160, 800), new Rectangle(0, 0, 160, 800), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 320, 1280), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 1600, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 960), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 1920, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 4000, 160), new Rectangle(0, 0, 800, 640), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 640), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 640, 480), new Rectangle(0, 0, 960, 640), new Rectangle(0, 0, 480, 320), new Rectangle(0, 0, 160, 2720), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 160, 640), }, Properties.Resources.darkDungeon2, Properties.Resources.dungeon2, new Point[] { new Point(480, 160), new Point(480, 480), new Point(1280, 480), new Point(480, 800), new Point(480, 960), new Point(1280, 800), new Point(2560, 320), new Point(2560, 160), new Point(2240, 0), new Point(480, -480), new Point(1120, -800), new Point(2880, -480), new Point(2880, -800), new Point(3520, -960), new Point(1920, -1120), new Point(0, -1280), new Point(0, -1120), new Point(-160, -1120), new Point(-480, 0), new Point(-1120, 0), new Point(-1280, 160), new Point(-1760, 960), new Point(-1920, 1120), new Point(-1760, 1600), new Point(-1280, 1760), new Point(-800, 1440), new Point(-480, 800), new Point(-320, 800), new Point(-320, 2400), new Point(0, 1920), new Point(0, 1600), new Point(1120, 1920), new Point(1280, 1920), new Point(1120, 2560), new Point(-1120, 2560), new Point(640, 2720), new Point(800, 3040), new Point(2080, 2240), new Point(1600, 1440), new Point(1280, 1280), new Point(2080, 1280), new Point(2240, 1760), new Point(2720, 1600), new Point(2720, 1280), new Point(3520, 1600), new Point(3680, 1600), new Point(3360, 2240), new Point(4320, 2240), new Point(4800, 320), new Point(4160, 640), new Point(3840, 800), new Point(4000, 960), new Point(4160, 160), new Point(3840, 0), new Point(4160, -480), new Point(4320, -1120), });
        private static readonly Dungeon dungeon3 = new(3, 1020, 60, -560, 3120, 20, 20, new Rectangle[] { new Rectangle(0, 0, 160, 1600), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 1920, 160), new Rectangle(0, 0, 480, 1120), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 1280, 320), new Rectangle(0, 0, 320, 640), new Rectangle(0, 0, 3040, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 1600, 160), new Rectangle(0, 0, 480, 320), new Rectangle(0, 0, 800, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 800), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 800), new Rectangle(0, 0, 640, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 1440, 320), new Rectangle(0, 0, 160, 800), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 160, 800), new Rectangle(0, 0, 1600, 160), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 160, 640), new Rectangle(0, 0, 160, 800), new Rectangle(0, 0, 2400, 160), new Rectangle(0, 0, 160, 640), new Rectangle(0, 0, 1440, 160), new Rectangle(0, 0, 640, 800), new Rectangle(0, 0, 160, 1280), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 800, 640), new Rectangle(0, 0, 800, 480), new Rectangle(0, 0, 320, 960), new Rectangle(0, 0, 160, 320) }, Properties.Resources.darkDungeon3, Properties.Resources.dungeon3, new Point[] { new Point(640, -640), new Point(800, -800), new Point(800, 320), new Point(800, 960), new Point(1280, -320), new Point(1760, 0), new Point(1760, -320), new Point(2720, -960), new Point(3040, -960), new Point(3520, -640), new Point(4480, -480), new Point(4480, -640), new Point(5120, -480), new Point(4480, 0), new Point(4960, 320), new Point(2720, 160), new Point(3040, 480), new Point(3360, 320), new Point(3520, 0), new Point(3360, -320), new Point(3840, 0), new Point(3840, 800), new Point(4480, 480), new Point(5920, -320), new Point(6080, -320), new Point(6880, -1120), new Point(5280, -1280), new Point(6400, -1120), new Point(5280, -1920), new Point(5120, -2720), new Point(2720, -2880), new Point(2560, -2720), new Point(1120, -2240), new Point(1600, -2080), new Point(960, -2080), new Point(1600, -800), new Point(2880, -1920), new Point(4000, -2400), new Point(4160, -1920), new Point(4480, -1280) });
        private static readonly Dungeon dungeon4 = new(4, 2640, 690, -570, -80, 40, 15, new Rectangle[] { new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 1920), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 480), new Rectangle(0, 0, 480, 1280), new Rectangle(0, 0, 480, 320), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 1120, 320), new Rectangle(0, 0, 320, 640), new Rectangle(0, 0, 800, 320), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 960, 160), new Rectangle(0, 0, 2400, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 480, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 320, 800), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 480, 800), new Rectangle(0, 0, 640, 480), new Rectangle(0, 0, 960, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 480, 480), new Rectangle(0, 0, 160, 1280), new Rectangle(0, 0, 160, 640), new Rectangle(0, 0, 800, 640), new Rectangle(0, 0, 640, 480), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 800, 800), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 480, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 960, 320), new Rectangle(0, 0, 960, 320), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 160, 2080), new Rectangle(0, 0, 960, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 1280, 320), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 1280, 320), new Rectangle(0, 0, 800, 640), new Rectangle(0, 0, 1600, 320), new Rectangle(0, 0, 1280, 160), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 800, 800), new Rectangle(0, 0, 1280, 1280), new Rectangle(0, 0, 1280, 320), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 480, 480), new Rectangle(0, 0, 480, 800), new Rectangle(0, 0, 640, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 1600, 320), new Rectangle(0, 0, 800, 800), new Rectangle(0, 0, 320, 640), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 160, 3360), new Rectangle(0, 0, 2400, 320), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 320, 480), new Rectangle(0, 0, 320, 480), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 320, 160), new Rectangle(0, 0, 480, 480), new Rectangle(0, 0, 320, 480), new Rectangle(0, 0, 1120, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 1440, 320), new Rectangle(0, 0, 320, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 640, 640), new Rectangle(0, 0, 160, 960), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 2880, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 640, 800), new Rectangle(0, 0, 640, 320), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 2880, 1120), new Rectangle(0, 0, 1920, 480), new Rectangle(0, 0, 640, 640), new Rectangle(0, 0, 160, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 160, 1600) }, Properties.Resources.darkDungeon4, Properties.Resources.dungeon4, new Point[] { new Point(800, 320), new Point(640, 480), new Point(800, 2400), new Point(800, 1440), new Point(1280, 1440), new Point(1280, 960), new Point(1280, 640), new Point(1280, 320), new Point(2080, 640), new Point(2400, 960), new Point(2880, 640), new Point(2400, 320), new Point(3360, 160), new Point(3360, 960), new Point(3680, 1120), new Point(4000, 640), new Point(4000, 1120), new Point(4160, 1440), new Point(4800, 800), new Point(5120, 1280), new Point(5440, 800), new Point(5600, 800), new Point(5760, 320), new Point(6400, 320), new Point(7200, 640), new Point(7360, 320), new Point(7840, 320), new Point(8160, 800), new Point(8000, 960), new Point(6720, 960), new Point(6400, 2080), new Point(7040, 2560), new Point(7360, 2080), new Point(7520, 2080), new Point(7360, 2720), new Point(7520, 2880), new Point(7200, 2880), new Point(7040, 3520), new Point(6880, 4160), new Point(6720, 4320), new Point(6720, 4640), new Point(8320, 2880), new Point(7360, 4960), new Point(4640, 1600), new Point(4800, 1760), new Point(4640, 1920), new Point(3840, 1760), new Point(3840, 2240), new Point(2080, 1440), new Point(2400, 1760), new Point(2560, 2240), new Point(2080, 2400), new Point(800, 2720), new Point(1600, 3040), new Point(1120, 3360), new Point(1440, 3520), new Point(2880, 3360), new Point(3680, 2880), new Point(4800, 2880), new Point(4800, 2080), new Point(4800, 2400), new Point(5600, 2400), new Point(6080, 3360), new Point(5760, 4160), new Point(5760, 4320), new Point(3680, 4160), new Point(4480, 4480), new Point(3680, 4480), new Point(4000, 4800), new Point(640, 3360), new Point(800, 4000), new Point(1280, 4320), new Point(1120, 4800), new Point(1920, 4800), new Point(2720, 4800), new Point(3200, 5280), new Point(1920, 5600), new Point(1120, 5600), new Point(1120, 6080), new Point(800, 6560), new Point(800, 6720), new Point(2720, 5600), new Point(3040, 5920), new Point(3680, 5920), new Point(3360, 6560), new Point(1440, 6880), new Point(1600, 7680), new Point(1600, 7840), new Point(2080, 7520), new Point(2240, 7040), new Point(3360, 7520), new Point(4000, 7680), new Point(4480, 6720), new Point(4960, 5920), new Point(6240, 5280), new Point(6880, 5760), new Point(7200, 5280), new Point(7360, 5120) });
        private static readonly Dungeon finalPillowRoom = new(5, 2240, 1720, -1040, -720, 20, 20, new Rectangle[] { new Rectangle(0, 0, 2080, 160), new Rectangle(0, 0, 160, 1600), new Rectangle(0, 0, 160, 1600), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 800, 160), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 480, 160), new Rectangle(0, 0, 840, 80), new Rectangle(0, 0, 160, 320), new Rectangle(0, 0, 80, 640), new Rectangle(0, 0, 1120, 160), new Rectangle(0, 0, 80, 640), new Rectangle(0, 0, 220, 160) }, Properties.Resources.darkFinalPillowRoom, Properties.Resources.finalPillowRoom, new Point[] { new Point(1280, 800), new Point(1120, 960), new Point(3360, 960), new Point(1280, 2560), new Point(2560, 2560), new Point(1920, 2720), new Point(2560, 2720), new Point(2080, 3040), new Point(2120, 2000), new Point(1600, 1600), new Point(1760, 1440), new Point(1760, 1360), new Point(2880, 1360), new Point(2240, 2000) });

        //Peanut Animations
        private static readonly Image[] peanutRunningRight = { Properties.Resources.peanutRunningRight1, Properties.Resources.peanutRunningRight2, Properties.Resources.peanutRunningRight3 };
        private static readonly Image[] peanutRunningLeft = { Properties.Resources.peanutRunningLeft1, Properties.Resources.peanutRunningLeft2, Properties.Resources.peanutRunningLeft3 };
        private static readonly Image[] peanutSleepingRight = { Properties.Resources.peanutRunningRight1, Properties.Resources.peanutSleepingRight2, Properties.Resources.peanutSleepingRight3, Properties.Resources.peanutSleepingRight4, Properties.Resources.peanutSleepingRight5, Properties.Resources.peanutSleepingRight6, Properties.Resources.peanutSleepingRight7 };
        private static readonly Image[] peanutSleepingLeft = { Properties.Resources.peanutRunningLeft1, Properties.Resources.peanutSleepingLeft2, Properties.Resources.peanutSleepingLeft3, Properties.Resources.peanutSleepingLeft4, Properties.Resources.peanutSleepingLeft5, Properties.Resources.peanutSleepingLeft6, Properties.Resources.peanutSleepingLeft7 };

        //Enemy Animations
        private static readonly Image[] blubRunningRight = { Properties.Resources.blubRight1, Properties.Resources.blubRight2, Properties.Resources.blubRight3 };
        private static readonly Image[] blubRunningLeft = { Properties.Resources.blubLeft1, Properties.Resources.blubLeft2, Properties.Resources.blubLeft3 };
        private static readonly Image[] ghostyRunningRight = { Properties.Resources.ghostyRight1, Properties.Resources.ghostyRight2, Properties.Resources.ghostyRight3, Properties.Resources.ghostyRight4 };
        private static readonly Image[] ghostyRunningLeft = { Properties.Resources.ghostyLeft1, Properties.Resources.ghostyLeft2, Properties.Resources.ghostyLeft3, Properties.Resources.ghostyLeft4 };
        private static readonly Image[] boxFloofRunningRight = { Properties.Resources.boxFloofRight1, Properties.Resources.boxFloofRight2, Properties.Resources.boxFloofRight3 };
        private static readonly Image[] boxFloofRunningLeft = { Properties.Resources.boxFloofLeft1, Properties.Resources.boxFloofLeft2, Properties.Resources.boxFloofLeft3 };

        //FuelPot animation
        private static readonly Image[] fuelPotAnimation = { Properties.Resources.fuelPot1, Properties.Resources.fuelPot2, Properties.Resources.fuelPot3, Properties.Resources.fuelPot4, Properties.Resources.fuelPot5, Properties.Resources.fuelPot6 };

        //UI Animations
        private static readonly Image[] scaredMeterFrames = { Properties.Resources.ScaredMeter1, Properties.Resources.ScaredMeter1, Properties.Resources.ScaredMeter1, Properties.Resources.ScaredMeter1, Properties.Resources.ScaredMeter2, Properties.Resources.ScaredMeter2, Properties.Resources.ScaredMeter2, Properties.Resources.ScaredMeter2, Properties.Resources.ScaredMeter3, Properties.Resources.ScaredMeter3, Properties.Resources.ScaredMeter3, Properties.Resources.ScaredMeter3, Properties.Resources.ScaredMeter4, Properties.Resources.ScaredMeter4, Properties.Resources.ScaredMeter4, Properties.Resources.ScaredMeter4, Properties.Resources.ScaredMeter5, Properties.Resources.ScaredMeter5, Properties.Resources.ScaredMeter5, Properties.Resources.ScaredMeter5, Properties.Resources.ScaredMeter6, Properties.Resources.ScaredMeter6, Properties.Resources.ScaredMeter6, Properties.Resources.ScaredMeter6, Properties.Resources.ScaredMeter7, Properties.Resources.ScaredMeter7, Properties.Resources.ScaredMeter7, Properties.Resources.ScaredMeter7, Properties.Resources.ScaredMeter8, Properties.Resources.ScaredMeter8, Properties.Resources.ScaredMeter8, Properties.Resources.ScaredMeter9, Properties.Resources.ScaredMeter9, Properties.Resources.ScaredMeter9, Properties.Resources.ScaredMeter10, Properties.Resources.ScaredMeter10, Properties.Resources.ScaredMeter10, Properties.Resources.ScaredMeter11, Properties.Resources.ScaredMeter11, Properties.Resources.ScaredMeter11, Properties.Resources.ScaredMeter12, Properties.Resources.ScaredMeter12, Properties.Resources.ScaredMeter12, Properties.Resources.ScaredMeter13, Properties.Resources.ScaredMeter13, Properties.Resources.ScaredMeter13, Properties.Resources.ScaredMeter14, Properties.Resources.ScaredMeter14, Properties.Resources.ScaredMeter14, Properties.Resources.ScaredMeter15, Properties.Resources.ScaredMeter15, Properties.Resources.ScaredMeter15, Properties.Resources.ScaredMeter16, Properties.Resources.ScaredMeter16, Properties.Resources.ScaredMeter16, Properties.Resources.ScaredMeter17, Properties.Resources.ScaredMeter17, Properties.Resources.ScaredMeter17, Properties.Resources.ScaredMeter18, Properties.Resources.ScaredMeter18, Properties.Resources.ScaredMeter18, Properties.Resources.ScaredMeter19, Properties.Resources.ScaredMeter19, Properties.Resources.ScaredMeter19, Properties.Resources.ScaredMeter20, Properties.Resources.ScaredMeter20, Properties.Resources.ScaredMeter20, Properties.Resources.ScaredMeter21, Properties.Resources.ScaredMeter21, Properties.Resources.ScaredMeter21, Properties.Resources.ScaredMeter22, Properties.Resources.ScaredMeter22, Properties.Resources.ScaredMeter22, Properties.Resources.ScaredMeter23, Properties.Resources.ScaredMeter23, Properties.Resources.ScaredMeter23, Properties.Resources.ScaredMeter24, Properties.Resources.ScaredMeter24, Properties.Resources.ScaredMeter24, Properties.Resources.ScaredMeter25, Properties.Resources.ScaredMeter25, Properties.Resources.ScaredMeter25, Properties.Resources.ScaredMeter26, Properties.Resources.ScaredMeter26, Properties.Resources.ScaredMeter26, Properties.Resources.ScaredMeter27, Properties.Resources.ScaredMeter27, Properties.Resources.ScaredMeter27, Properties.Resources.ScaredMeter28, Properties.Resources.ScaredMeter28, Properties.Resources.ScaredMeter28, Properties.Resources.ScaredMeter29, Properties.Resources.ScaredMeter29, Properties.Resources.ScaredMeter29, Properties.Resources.ScaredMeter30, Properties.Resources.ScaredMeter30, Properties.Resources.ScaredMeter30, Properties.Resources.ScaredMeter31, Properties.Resources.ScaredMeter31, Properties.Resources.ScaredMeter31, Properties.Resources.ScaredMeter31 };
        private static readonly Image[] lanternMeterFrames = { Properties.Resources.LanternFuel31, Properties.Resources.LanternFuel31, Properties.Resources.LanternFuel31, Properties.Resources.LanternFuel31, Properties.Resources.LanternFuel30, Properties.Resources.LanternFuel30, Properties.Resources.LanternFuel30, Properties.Resources.LanternFuel30, Properties.Resources.LanternFuel29, Properties.Resources.LanternFuel29, Properties.Resources.LanternFuel29, Properties.Resources.LanternFuel29, Properties.Resources.LanternFuel28, Properties.Resources.LanternFuel28, Properties.Resources.LanternFuel28, Properties.Resources.LanternFuel28, Properties.Resources.LanternFuel27, Properties.Resources.LanternFuel27, Properties.Resources.LanternFuel27, Properties.Resources.LanternFuel27, Properties.Resources.LanternFuel26, Properties.Resources.LanternFuel26, Properties.Resources.LanternFuel26, Properties.Resources.LanternFuel26, Properties.Resources.LanternFuel25, Properties.Resources.LanternFuel25, Properties.Resources.LanternFuel25, Properties.Resources.LanternFuel25, Properties.Resources.LanternFuel24, Properties.Resources.LanternFuel24, Properties.Resources.LanternFuel24, Properties.Resources.LanternFuel23, Properties.Resources.LanternFuel23, Properties.Resources.LanternFuel23, Properties.Resources.LanternFuel22, Properties.Resources.LanternFuel22, Properties.Resources.LanternFuel22, Properties.Resources.LanternFuel21, Properties.Resources.LanternFuel21, Properties.Resources.LanternFuel21, Properties.Resources.LanternFuel20, Properties.Resources.LanternFuel20, Properties.Resources.LanternFuel20, Properties.Resources.LanternFuel19, Properties.Resources.LanternFuel19, Properties.Resources.LanternFuel19, Properties.Resources.LanternFuel18, Properties.Resources.LanternFuel18, Properties.Resources.LanternFuel18, Properties.Resources.LanternFuel17, Properties.Resources.LanternFuel17, Properties.Resources.LanternFuel17, Properties.Resources.LanternFuel16, Properties.Resources.LanternFuel16, Properties.Resources.LanternFuel16, Properties.Resources.LanternFuel15, Properties.Resources.LanternFuel15, Properties.Resources.LanternFuel15, Properties.Resources.LanternFuel14, Properties.Resources.LanternFuel14, Properties.Resources.LanternFuel14, Properties.Resources.LanternFuel13, Properties.Resources.LanternFuel13, Properties.Resources.LanternFuel13, Properties.Resources.LanternFuel12, Properties.Resources.LanternFuel12, Properties.Resources.LanternFuel12, Properties.Resources.LanternFuel11, Properties.Resources.LanternFuel11, Properties.Resources.LanternFuel11, Properties.Resources.LanternFuel10, Properties.Resources.LanternFuel10, Properties.Resources.LanternFuel10, Properties.Resources.LanternFuel9, Properties.Resources.LanternFuel9, Properties.Resources.LanternFuel9, Properties.Resources.LanternFuel8, Properties.Resources.LanternFuel8, Properties.Resources.LanternFuel8, Properties.Resources.LanternFuel7, Properties.Resources.LanternFuel7, Properties.Resources.LanternFuel7, Properties.Resources.LanternFuel6, Properties.Resources.LanternFuel6, Properties.Resources.LanternFuel6, Properties.Resources.LanternFuel5, Properties.Resources.LanternFuel5, Properties.Resources.LanternFuel5, Properties.Resources.LanternFuel4, Properties.Resources.LanternFuel4, Properties.Resources.LanternFuel4, Properties.Resources.LanternFuel3, Properties.Resources.LanternFuel3, Properties.Resources.LanternFuel3, Properties.Resources.LanternFuel2, Properties.Resources.LanternFuel2, Properties.Resources.LanternFuel2, Properties.Resources.LanternFuel1, Properties.Resources.LanternFuel1, Properties.Resources.LanternFuel1, Properties.Resources.LanternFuel1 };

        //Game end animation
        private static readonly Image[] endFrames = { Properties.Resources.end1, Properties.Resources.end2, Properties.Resources.end3, Properties.Resources.end4, Properties.Resources.end5, Properties.Resources.end6, Properties.Resources.end7, Properties.Resources.end8, Properties.Resources.end9, Properties.Resources.end10, Properties.Resources.end11, Properties.Resources.end12, Properties.Resources.end13, Properties.Resources.end14, Properties.Resources.end15, Properties.Resources.end16, Properties.Resources.end17, Properties.Resources.end18 };

        //Jumpscare Frames
        private static readonly Image[] blubJumpFrames = { Properties.Resources.blubJump1, Properties.Resources.blubJump2, Properties.Resources.blubJump3, Properties.Resources.blubJump4 };
        private static readonly Image[] ghostyJumpFrames = { Properties.Resources.ghostyJump1, Properties.Resources.ghostyJump2, Properties.Resources.ghostyJump3, Properties.Resources.ghostyJump4, Properties.Resources.ghostyJump5, Properties.Resources.ghostyJump6, Properties.Resources.ghostyJump7, Properties.Resources.ghostyJump8, Properties.Resources.ghostyJump9 };
        private static readonly Image[] boxFloofJumpFrames = { Properties.Resources.boxFloofJump1, Properties.Resources.boxFloofJump2, Properties.Resources.boxFloofJump3, Properties.Resources.boxFloofJump4, Properties.Resources.boxFloofJump5, Properties.Resources.boxFloofJump6 };

        //Sound Streams and Outs
        private static WaveStream PeanutPurrStream;
        private static readonly WaveOut PeanutPurrOut = new();
        private static WaveStream DisappointedMeowStream;
        private static readonly WaveOut DisappointedMeowOut = new();
        private static WaveStream RefillLanternStream;
        private static readonly WaveOut RefillLanternOut = new();
        private static WaveStream BlubJumpscareStream;
        private static readonly WaveOut BlubJumpscareOut = new();
        private static WaveStream GhostyJumpscareStream;
        private static readonly WaveOut GhostyJumpscareOut = new();
        private static WaveStream BoxFloofJumpscareStream;
        private static readonly WaveOut BoxFloofJumpscareOut = new();
        private static WaveStream BrickFallStream;
        private static readonly WaveOut BrickFallOut = new();
        private static WaveStream DistantMeowStream;
        private static readonly WaveOut DistantMeowOut = new();
        private static WaveStream MetalClangStream;
        private static readonly WaveOut MetalClangOut = new();
        private static WaveStream WindStream;
        private static readonly WaveOut WindOut = new();
        private static WaveStream WoodCreakStream;
        private static readonly WaveOut WoodCreakOut = new();
        private static WaveStream BoopStream;
        private static readonly WaveOut BoopOut = new();
        private static WaveStream WinSoundStream;
        private static readonly WaveOut WinSoundOut = new();

        //Miscellaneous
        private static bool win = false;
        private static bool menu = true;
        private static int scaredMeterDecreaseTicks = 0;
        private static Image nextRestartMessageFrame = Properties.Resources.restartIn3;
        private static readonly Image winImage = Properties.Resources.win;
        private static readonly Image[] menuImages = { Properties.Resources.title1, Properties.Resources.title2 };
        private static readonly Image[] restartMessage = { Properties.Resources.restartIn3, Properties.Resources.restartIn2, Properties.Resources.restartIn1 };
        private static readonly Random random = new();
        private static readonly Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
        private float scaleFactor;
        private static Rectangle playButton = new(1788, 891, 442, 325);
        private static Rectangle boopButton = new(1305, 1197, 91, 75);
        private static Rectangle mouseCheck = new(0, 0, 1, 1);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Finds scale factors for resolution, maintaining aspect ratio
            scaleFactor = screenBounds.Height / 1504f;
            //Resize the window to fit the new scaled graphics, resizes and repositions playButton and boopButton
            if(screenBounds.Width != 2256 || screenBounds.Height != 1504)
            {
                WindowState = FormWindowState.Normal;
                Width = Convert.ToInt32(Math.Round(2256f * scaleFactor));
                Height = Convert.ToInt32(Math.Round(1504f * scaleFactor));
                CenterToScreen();
                playButton.Location = new Point(Convert.ToInt32(Math.Round(Convert.ToDouble(playButton.X * scaleFactor))), Convert.ToInt32(Math.Round(Convert.ToDouble(playButton.Y * scaleFactor))));
                playButton.Size = new Size(Convert.ToInt32(Math.Round(Convert.ToDouble(playButton.Width * scaleFactor))), Convert.ToInt32(Math.Round(Convert.ToDouble(playButton.Height * scaleFactor))));
                boopButton.Location = new Point(Convert.ToInt32(Math.Round(Convert.ToDouble(boopButton.X * scaleFactor))), Convert.ToInt32(Math.Round(Convert.ToDouble(boopButton.Y * scaleFactor))));
                boopButton.Size = new Size(Convert.ToInt32(Math.Round(Convert.ToDouble(boopButton.Width * scaleFactor))), Convert.ToInt32(Math.Round(Convert.ToDouble(boopButton.Height * scaleFactor))));
            }
            //Gets dungeons loaded
            ProgressionManagement.allDungeons = new Dungeon[] { tutorialDungeon, dungeon1, dungeon2, dungeon3, dungeon4, finalPillowRoom };
            ProgressionManagement.InitializeClassArrays();
            //Loads in lights
            //Polygons for inner circle
            lightPath1.AddPolygon(Light.TopRightQuadrant1);
            lightPath1.AddPolygon(Light.TopLeftQuadrant1);
            lightPath1.AddPolygon(Light.BottomRightQuadrant1);
            lightPath1.AddPolygon(Light.BottomLeftQuadrant1);
            //Polygons for outer circle
            lightPath2.AddPolygon(Light.TopRightQuadrant2);
            lightPath2.AddPolygon(Light.TopLeftQuadrant2);
            lightPath2.AddPolygon(Light.BottomRightQuadrant2);
            lightPath2.AddPolygon(Light.BottomLeftQuadrant2);
            //Creates regions
            lightCircle = new Region(lightPath1);
            outerLightCircle = new Region(lightPath2);
            //Exclusion paths
            excludePath.AddPolygon(Light.excludeOutsideLeft);
            excludePath.AddPolygon(Light.excludeOutsideRight);
            darkExcludePath.AddPolygon(Light.darkExcludeOutsideRight);
            darkExcludePath.AddPolygon(Light.darkExcludeOutsideLeft);
            //Gets sounds loaded
            Stream stream1 = Properties.Resources.peanutpurr;
            Stream stream2 = Properties.Resources.disappointedMeow;
            Stream stream3 = Properties.Resources.refillLantern;
            Stream stream4 = Properties.Resources.blubJumpscare;
            Stream stream5 = Properties.Resources.ghostyJumpscare;
            Stream stream6 = Properties.Resources.boxFloofJumpscare;
            Stream stream7 = Properties.Resources.brickFall;
            Stream stream8 = Properties.Resources.distantMeow;
            Stream stream9 = Properties.Resources.metal;
            Stream stream10 = Properties.Resources.wind;
            Stream stream11 = Properties.Resources.woodCreak;
            Stream stream12 = Properties.Resources.boop;
            Stream stream13 = Properties.Resources.winSound;
            PeanutPurrStream = new WaveFileReader(stream1);
            DisappointedMeowStream = new WaveFileReader(stream2);
            RefillLanternStream = new WaveFileReader(stream3);
            BlubJumpscareStream = new WaveFileReader(stream4);
            GhostyJumpscareStream = new WaveFileReader(stream5);
            BoxFloofJumpscareStream = new WaveFileReader(stream6);
            BrickFallStream = new WaveFileReader(stream7);
            DistantMeowStream = new WaveFileReader(stream8);
            MetalClangStream = new WaveFileReader(stream9);
            WindStream = new WaveFileReader(stream10);
            WoodCreakStream = new WaveFileReader(stream11);
            BoopStream = new WaveFileReader(stream12);
            WinSoundStream = new WaveFileReader(stream13);
            PeanutPurrOut.Init(PeanutPurrStream);
            DisappointedMeowOut.Init(DisappointedMeowStream);
            RefillLanternOut.Init(RefillLanternStream);
            BlubJumpscareOut.Init(BlubJumpscareStream);
            GhostyJumpscareOut.Init(GhostyJumpscareStream);
            BoxFloofJumpscareOut.Init(BoxFloofJumpscareStream);
            BrickFallOut.Init(BrickFallStream);
            DistantMeowOut.Init(DistantMeowStream);
            MetalClangOut.Init(MetalClangStream);
            WindOut.Init(WindStream);
            WoodCreakOut.Init(WoodCreakStream);
            BoopOut.Init(BoopStream);
            WinSoundOut.Init(WinSoundStream);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //If right is held
            if(e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                rightHeld = true;
            }
            //If left is held
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                leftHeld = true;
            }
            //If up is held
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                upHeld = true;
            }
            //If down is held
            if(e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                downHeld = true;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //If right is no longer held
            if(e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                rightHeld = false;
            }
            //If left is no longer held
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                leftHeld = false;
            }
            //If up is no longer held
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                upHeld = false;
            }
            //If down is no longer held
            if(e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                downHeld = false;
            }
        }

        //Manages graphics, extremely optimized
        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Scales graphics to resolution
            e.Graphics.ScaleTransform(scaleFactor, scaleFactor);
            //Interpolation and PixelOffsetMode is for scaling images up, without warring on them
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            if(win)
            {
                e.Graphics.DrawImage(winImage, 0, 0, 2260, 1420);
                return;
            }
            if(menu)
            {
                if(menuTicks > 20)
                {
                    menuTicks = 0;
                }
                if(menuTicks <= 10)
                {
                    e.Graphics.DrawImage(menuImages[0], 0, 0, 2250, 1400);
                }
                else if(menuTicks <= 20)
                {
                    e.Graphics.DrawImage(menuImages[1], 0, 0, 2250, 1400);
                }
                menuTicks++;
                return;
            }
            if(globalDisappear)
            {
                //If a blub jumpscare is supposed to happen, do it
                for(int i = 0; i < ProgressionManagement.allBlubs.Length; i++)
                {
                    if(ProgressionManagement.allBlubs[i].disappear)
                    {
                        e.Graphics.DrawImage(nextBlubJumpFrame, 0, 0, 2400, 1600);
                        return;
                    }
                }
                //If a ghosty jumpscare is supposed to happen, do it
                for(int i = 0; i < ProgressionManagement.allGhosties.Length; i++)
                {
                    if(ProgressionManagement.allGhosties[i].disappear)
                    {
                        e.Graphics.DrawImage(nextGhostyJumpFrame, 0, 0, 2400, 1600);
                        return;
                    }
                }
                //If a boxFloof jumpscare is supposed to happen, do it
                for(int i = 0; i < ProgressionManagement.allBoxFloofs.Length; i++)
                {
                    if(ProgressionManagement.allBoxFloofs[i].disappear)
                    {
                        e.Graphics.DrawImage(nextBoxFloofJumpFrame, 0, 0, 2400, 1600);
                        return;
                    }
                }
            }
            //If the game is supposed to restart, restart it
            if(scaredMeter == 100)
            {
                e.Graphics.DrawImage(nextEndFrame, 0, -100, 2260, 1510);
                if(isRestarting)
                {
                    e.Graphics.DrawImage(nextRestartMessageFrame, 750, 100, 710, 110);
                }
                return;
            }
            //Makes sure lightBox is at the right position
            lightBox.Location = new Point(realPeanutX / 10 - 16, realPeanutY / 10 - 16);
            darkBox.Location = new Point(realPeanutX / 10 - 32, realPeanutY / 10 - 32);
            //Draws the background at the scrollX and scrollY
            if(lanternFuel != 0)
            {
                e.Graphics.SetClip(outerLightCircle, CombineMode.Xor);
                e.Graphics.DrawImage(ProgressionManagement.CurrentDungeon().levelDark, new Rectangle(peanutHitboxX - 320, peanutHitboxY - 320, 800, 800), darkBox, GraphicsUnit.Pixel);
                e.Graphics.SetClip(lightCircle, CombineMode.Xor);
                e.Graphics.DrawImage(ProgressionManagement.CurrentDungeon().levelLight, new Rectangle(peanutHitboxX - 160, peanutHitboxY - 160, 480, 480), lightBox, GraphicsUnit.Pixel);
            }
            else
            {
                e.Graphics.SetClip(lightCircle, CombineMode.Xor);
                e.Graphics.DrawImage(ProgressionManagement.CurrentDungeon().levelDark, new Rectangle(peanutHitboxX - 160, peanutHitboxY - 160, 480, 480), lightBox, GraphicsUnit.Pixel);
            }
            //Sets the clipping mode for all the blubs
            for(int i = 0; i < ProgressionManagement.allBlubs.Length; i++)
            {
                if(ProgressionManagement.allBlubs[i].hidden != true)
                {
                    e.Graphics.SetClip(new Rectangle(scrollX + ProgressionManagement.allBlubs[i].X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + ProgressionManagement.allBlubs[i].Y + ProgressionManagement.CurrentDungeon().yOffset, 160, 160), CombineMode.Union);
                }
            }
            //Sets the clipping mode for all the ghosties
            for(int i = 0; i < ProgressionManagement.allGhosties.Length; i++)
            {
                if(ProgressionManagement.allGhosties[i].hidden != true)
                {
                    e.Graphics.SetClip(new Rectangle(scrollX + ProgressionManagement.allGhosties[i].X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + ProgressionManagement.allGhosties[i].Y + ProgressionManagement.CurrentDungeon().yOffset, 160, 160), CombineMode.Union);
                }
            }
            //Sets the clipping mode for all the boxFloofs
            for(int i = 0; i < ProgressionManagement.allBoxFloofs.Length; i++)
            {
                if(ProgressionManagement.allBoxFloofs[i].hidden != true)
                {
                    e.Graphics.SetClip(new Rectangle(scrollX + ProgressionManagement.allBoxFloofs[i].X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + ProgressionManagement.allBoxFloofs[i].Y + ProgressionManagement.CurrentDungeon().yOffset, 160, 160), CombineMode.Union);
                }
            }
            //Sets the clipping mode for all the fuelpots
            for(int i = 0; i < ProgressionManagement.allFuelPots.Length; i++)
            {
                if(ProgressionManagement.allFuelPots[i].hidden != true)
                {
                    e.Graphics.SetClip(new Rectangle(scrollX + ProgressionManagement.allFuelPots[i].X, scrollY + ProgressionManagement.allFuelPots[i].Y, 160, 160), CombineMode.Union);
                }
            }
            //Clips outside enemies and other objects
            if(lanternFuel != 0)
            {
                e.Graphics.SetClip(excludePath, CombineMode.Xor);
            }
            else
            {
                e.Graphics.SetClip(darkExcludePath, CombineMode.Xor);
            }
            //Draws image nextPeanutFrame (set to the correct next animation frame using timer1) at peanut's normal location
            e.Graphics.DrawImage(nextPeanutFrame, peanutHitboxX, peanutHitboxY, 170, 140);
            //Draws all blubs
            for(int i = 0; i < ProgressionManagement.allBlubs.Length; i++)
            {
                if(!ProgressionManagement.allBlubs[i].hidden)
                {
                    e.Graphics.DrawImage(ProgressionManagement.allBlubs[i].nextFrame, scrollX + ProgressionManagement.allBlubs[i].X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + ProgressionManagement.allBlubs[i].Y + ProgressionManagement.CurrentDungeon().yOffset, 160, 160);
                }
            }
            //Draws all ghosties
            for(int i = 0; i < ProgressionManagement.allGhosties.Length; i++)
            {
                if(!ProgressionManagement.allGhosties[i].hidden)
                {
                    e.Graphics.DrawImage(ProgressionManagement.allGhosties[i].nextFrame, scrollX + ProgressionManagement.allGhosties[i].X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + ProgressionManagement.allGhosties[i].Y + ProgressionManagement.CurrentDungeon().yOffset, 160, 160);
                }
            }
            //Draws all boxFloofs
            for(int i = 0; i < ProgressionManagement.allBoxFloofs.Length; i++)
            {
                if(!ProgressionManagement.allBoxFloofs[i].hidden)
                {
                    e.Graphics.DrawImage(ProgressionManagement.allBoxFloofs[i].nextFrame, scrollX + ProgressionManagement.allBoxFloofs[i].X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + ProgressionManagement.allBoxFloofs[i].Y + ProgressionManagement.CurrentDungeon().yOffset, 160, 160);
                }
            }
            //Draws all FuelPots
            for(int i = 0; i < ProgressionManagement.allFuelPots.Length; i++)
            {
                if(!ProgressionManagement.allFuelPots[i].hidden)
                {
                    e.Graphics.DrawImage(ProgressionManagement.allFuelPots[i].nextFuelPot, scrollX + ProgressionManagement.allFuelPots[i].X, scrollY + ProgressionManagement.allFuelPots[i].Y, 160, 160);
                }
            }
            //Clips the meters
            e.Graphics.SetClip(scaredRectangle, CombineMode.Union);
            e.Graphics.SetClip(lanternRectangle, CombineMode.Union);
            //Draws the meters
            e.Graphics.DrawImage(nextScaredMeter, 10, 1290, 420, 110);
            e.Graphics.DrawImage(nextLanternFuel, 1830, 1290, 420, 110);
        }
        //Used for managing most of the things in the game
        public void GameManager_Tick(object sender, EventArgs e)
        {
            if(win)
            {
                if(WinSoundOut.PlaybackState == PlaybackState.Stopped && !winSoundPlayed)
                {
                    winSoundPlayed = true;
                    WinSoundStream.CurrentTime = new TimeSpan(0);
                    WinSoundOut.Play();
                }
                winTicks++;
                if(winTicks > 400)
                {
                    win = false;
                    winTicks = 0;
                    menu = true;
                    winSoundPlayed = false;
                }
                return;
            }
            if(menu)
            {
                return;
            }
            //Makes sure the dungeon's hitboxes are in the correct place
            pillow.Location = new Point(scrollX + ProgressionManagement.CurrentDungeon().pillowX, scrollY + ProgressionManagement.CurrentDungeon().pillowY);
            ProgressionManagement.CurrentDungeon().collision.ScrollCollision(scrollX, scrollY);
            //Handles movement, movement speed, and collision if peanut isn't sleeping
            if(!isSleeping && !globalDisappear && scaredMeter != 100)
            {
                CollisionLogic();
            }
            //Manages peanutTicks, used for animation
            if(peanutTicks > 16)
            {
                peanutTicks = 0;
            }
            //Manages all the blubs
            for(int i = 0; i < ProgressionManagement.allBlubs.Length; i++)
            {
                BlubManager(ProgressionManagement.allBlubs[i]);
            }
            //Manages all the ghosties
            for(int i = 0; i < ProgressionManagement.allGhosties.Length; i++)
            {
                GhostyManager(ProgressionManagement.allGhosties[i]);
            }
            //Manages all the boxFloofs
            for(int i = 0; i < ProgressionManagement.allBoxFloofs.Length; i++)
            {
                BoxFloofManager(ProgressionManagement.allBoxFloofs[i]);
            }
            //Manages all the fuelpots
            for(int i = 0; i < ProgressionManagement.allFuelPots.Length; i++)
            {
                FuelPotManager(ProgressionManagement.allFuelPots[i]);
            }
            //Manages peanut animations
            PeanutAnimationManager();
            //Sets the meters to the correct frame
            if(scaredMeter > 100)
            {
                scaredMeter = 100;
            }
            nextScaredMeter = scaredMeterFrames[scaredMeter];
            nextLanternFuel = lanternMeterFrames[lanternFuel];
        }

        private static void PeanutAnimationManager()
        {
            //Makes sure that if Peanut is sleeping, it cancels animations
            if(!isSleeping)
            {
                //Makes peanut stand still if opposite directions are held and makes peanut stand still if not moving
                if(rightHeld && leftHeld || upHeld && downHeld)
                {
                    if(rightLastHeld)
                    {
                        nextPeanutFrame = peanutRunningRight[0];
                    }
                    else
                    {
                        nextPeanutFrame = peanutRunningLeft[0];
                    }
                }
                else
                {
                    //Stops moving animation if not moving
                    if(rightHeld == false && leftHeld == false && upHeld == false && downHeld == false)
                    {
                        if(rightLastHeld)
                        {
                            nextPeanutFrame = peanutRunningRight[0];
                        }
                        else
                        {
                            nextPeanutFrame = peanutRunningLeft[0];
                        }
                    }
                    else
                    {
                        //Animates right running when needed
                        if(rightHeld || rightLastHeld && upHeld || rightLastHeld && downHeld)
                        {
                            if(peanutTicks < 4)
                            {
                                //Frame 1
                                nextPeanutFrame = peanutRunningRight[0];
                            }
                            else if(peanutTicks > 3 && peanutTicks < 8 || peanutTicks > 11 && peanutTicks < 16)
                            {
                                //Frame 2 and frame 4
                                nextPeanutFrame = peanutRunningRight[1];
                            }
                            else if(peanutTicks > 7 && peanutTicks < 12)
                            {
                                //Frame 3
                                nextPeanutFrame = peanutRunningRight[2];
                            }
                        }
                        //Animates left running when needed
                        else if(leftHeld || leftLastHeld && upHeld || leftLastHeld && downHeld)
                        {
                            //Frame 1
                            if(peanutTicks < 4)
                            {
                                nextPeanutFrame = peanutRunningLeft[0];
                            }
                            else if(peanutTicks > 3 && peanutTicks < 8 || peanutTicks > 11 && peanutTicks < 16)
                            {
                                //Frame 2 and frame 4
                                nextPeanutFrame = peanutRunningLeft[1];
                            }
                            else if(peanutTicks > 7 && peanutTicks < 12)
                            {
                                //Frame 3
                                nextPeanutFrame = peanutRunningLeft[2];
                            }
                        }
                        peanutTicks++;
                    }
                }
            }
        }

        private static void BoxFloofManager(BoxFloof boxFloof)
        {
            if(!boxFloof.hidden)
            {
                //Checks if the boxFloof needs to change directions
                if(AnimationHelp.IsClose(new Point(boxFloof.X + scrollX + ProgressionManagement.CurrentDungeon().xOffset, boxFloof.Y + scrollY + ProgressionManagement.CurrentDungeon().yOffset), new Point(boxFloof.point3X + scrollX + ProgressionManagement.CurrentDungeon().xOffset, boxFloof.point3Y + scrollY + ProgressionManagement.CurrentDungeon().yOffset)))
                {
                    Point[] currentPoints = { new Point(boxFloof.point1X, boxFloof.point1Y), new Point(boxFloof.point2X, boxFloof.point2Y), new Point(boxFloof.point3X, boxFloof.point3Y) };
                    int randomPointIndex;
                    do
                    {
                        randomPointIndex = random.Next(0, 3);
                    }
                    while(currentPoints[randomPointIndex] == new Point(boxFloof.point3X, boxFloof.point3Y));
                    Point switchPoint = new(boxFloof.point3X, boxFloof.point3Y);
                    boxFloof.point3X = currentPoints[randomPointIndex].X;
                    boxFloof.point3Y = currentPoints[randomPointIndex].Y;
                    currentPoints[randomPointIndex] = switchPoint;
                    //Sets the points to the new points
                    boxFloof.point1X = currentPoints[0].X;
                    boxFloof.point1Y = currentPoints[0].Y;
                    boxFloof.point2X = currentPoints[1].X;
                    boxFloof.point2Y = currentPoints[1].Y;
                }
                //Checks which direction it needs to go if it doesn't need to switch directions
                else if(boxFloof.X < boxFloof.point3X && boxFloof.Y < boxFloof.point3Y)
                {
                    //If the boxFloof needs to go up and to the right
                    boxFloof.X += 4;
                    boxFloof.Y += 4;
                }
                else if(boxFloof.X > boxFloof.point3X && boxFloof.Y < boxFloof.point3Y)
                {
                    //If the boxFloof needs to go up and to the left
                    boxFloof.X -= 4;
                    boxFloof.Y += 4;
                }
                else if(boxFloof.X < boxFloof.point3X && boxFloof.Y > boxFloof.point3Y)
                {
                    //If the boxFloof needs to go down and to the right
                    boxFloof.X += 4;
                    boxFloof.Y -= 4;
                }
                else if(boxFloof.X > boxFloof.point3X && boxFloof.Y > boxFloof.point3Y)
                {
                    //If the boxFloof needs to go down and to the left
                    boxFloof.X -= 4;
                    boxFloof.Y -= 4;
                }
                else if(boxFloof.X < boxFloof.point3X)
                {
                    //If the boxFloof needs to go right
                    boxFloof.X += 4;
                }
                else if(boxFloof.X > boxFloof.point3X)
                {
                    //If the boxFloof needs to go left
                    boxFloof.X -= 4;
                }
                else if(boxFloof.Y < boxFloof.point3Y)
                {
                    //If the boxFloof needs to go up
                    boxFloof.Y += 4;
                }
                else if(boxFloof.Y > boxFloof.point3Y)
                {
                    //If the boxFloof needs to go down
                    boxFloof.Y -= 4;
                }
                //Starts doing animation stuff
                //Manages boxFloofTicks, used for animation
                if(boxFloof.ticks > 9)
                {
                    boxFloof.ticks = 0;
                }
                //Sets blub at the right frame and location
                if(boxFloof.X < boxFloof.point3X || boxFloof.Y < boxFloof.point3Y)
                {
                    //Running right animation
                    //Frame 1
                    if(boxFloof.ticks < 6)
                    {
                        boxFloof.nextFrame = boxFloofRunningRight[0];
                    }
                    //Frame 2
                    else if(boxFloof.ticks > 5 && boxFloof.ticks < 8)
                    {

                        boxFloof.nextFrame = boxFloofRunningRight[1];
                    }
                    //Frame 3
                    else if(boxFloof.ticks > 7 && boxFloof.ticks < 9)
                    {
                        boxFloof.nextFrame = boxFloofRunningRight[2];
                    }
                }
                else
                {
                    //Running left animation
                    //Frame 1
                    if(boxFloof.ticks < 6)
                    {
                        boxFloof.nextFrame = boxFloofRunningLeft[0];
                    }
                    //Frame 2
                    else if(boxFloof.ticks > 5 && boxFloof.ticks < 8)
                    {
                        boxFloof.nextFrame = boxFloofRunningLeft[1];
                    }
                    //Frame 3
                    else if(boxFloof.ticks > 7 && boxFloof.ticks < 9)
                    {
                        boxFloof.nextFrame = boxFloofRunningLeft[2];
                    }
                }
            }
            //If boxFloof is supposed to be jumpscaring, do that
            if(boxFloof.disappear)
            {
                boxFloof.X = 0;
                boxFloof.Y = 0;
                boxFloof.hidden = true;
                if(boxFloofJumpTicks < 6)
                {
                    //Frame 1
                    nextBoxFloofJumpFrame = boxFloofJumpFrames[0];
                    if(BoxFloofJumpscareOut.PlaybackState == PlaybackState.Stopped && !boxFloofJumpscarePlayed)
                    {
                        BoxFloofJumpscareStream.CurrentTime = new TimeSpan(0);
                        BoxFloofJumpscareOut.Play();
                        boxFloofJumpscarePlayed = true;
                    }
                }
                else if(boxFloofJumpTicks > 5 && boxFloofJumpTicks < 12)
                {
                    //Frame 2
                    nextBoxFloofJumpFrame = boxFloofJumpFrames[1];
                }
                else if(boxFloofJumpTicks > 11 && boxFloofJumpTicks < 18)
                {
                    //Frame 3
                    nextBoxFloofJumpFrame = boxFloofJumpFrames[2];
                }
                else if(boxFloofJumpTicks > 17 && boxFloofJumpTicks < 24)
                {
                    //Frame 4
                    nextBoxFloofJumpFrame = boxFloofJumpFrames[3];
                }
                else if(boxFloofJumpTicks > 23 && boxFloofJumpTicks < 30)
                {
                    //Frame 5
                    nextBoxFloofJumpFrame = boxFloofJumpFrames[4];
                }
                else if(boxFloofJumpTicks > 29 && boxFloofJumpTicks < 54)
                {
                    //Frame 6, 7, 8, and 9
                    nextBoxFloofJumpFrame = boxFloofJumpFrames[5];
                }
                else
                {
                    boxFloofJumpTicks = 0;
                    globalDisappear = false;
                    boxFloof.disappear = false;
                    boxFloofJumpscarePlayed = false;
                }
                boxFloofJumpTicks++;
            }
            boxFloof.hitbox.Location = new Point(scrollX + boxFloof.X, scrollY + boxFloof.Y);
            boxFloof.ticks++;
        }

        private static void GhostyManager(Ghosty ghosty)
        {
            if(!ghosty.hidden)
            {
                //Checks if the ghosty needs to switch directions
                if(AnimationHelp.IsClose(new Point(ghosty.X + scrollX + ProgressionManagement.CurrentDungeon().xOffset, ghosty.Y + scrollY + ProgressionManagement.CurrentDungeon().yOffset), new Point(ghosty.point2X + scrollX + ProgressionManagement.CurrentDungeon().xOffset, ghosty.point2Y + scrollY + ProgressionManagement.CurrentDungeon().yOffset)))
                {
                    //Switchs the place of point1 and point2, so that the ghosty can continue only going after point2
                    Point switchPoint = new(ghosty.point1X, ghosty.point1Y);
                    ghosty.point1X = ghosty.point2X;
                    ghosty.point1Y = ghosty.point2Y;
                    ghosty.point2X = switchPoint.X;
                    ghosty.point2Y = switchPoint.Y;
                }
                //Checks which direction it needs to go if it doesn't need to switch directions
                else if(ghosty.X < ghosty.point2X && ghosty.Y < ghosty.point2Y)
                {
                    //If the ghosty needs to go up and to the right
                    ghosty.X += 4;
                    ghosty.Y += 4;
                }
                else if(ghosty.X > ghosty.point2X && ghosty.Y < ghosty.point2Y)
                {
                    //If the ghosty needs to go up and to the left
                    ghosty.X -= 4;
                    ghosty.Y += 4;
                }
                else if(ghosty.X < ghosty.point2X && ghosty.Y > ghosty.point2Y)
                {
                    //If the ghosty needs to go down and to the right
                    ghosty.X += 4;
                    ghosty.Y -= 4;
                }
                else if(ghosty.X > ghosty.point2X && ghosty.Y > ghosty.point2Y)
                {
                    //If the ghosty needs to go down and to the left
                    ghosty.X -= 4;
                    ghosty.Y -= 4;
                }
                //Starts doing animation stuff
                //Manages the ghosty ticks, so they don't overflow
                if(ghosty.ticks > 15)
                {
                    ghosty.ticks = 0;
                }
                //Sets the next frame to the correct frame based on the ticks and direction
                //If the ghosty is going right
                if(ghosty.X < ghosty.point2X)
                {
                    if(ghosty.ticks < 4)
                    {
                        //Frame 1
                        ghosty.nextFrame = ghostyRunningRight[0];
                    }
                    else if(ghosty.ticks < 8 && ghosty.ticks > 3)
                    {
                        //Frame 2
                        ghosty.nextFrame = ghostyRunningRight[1];
                    }
                    else if(ghosty.ticks < 12 && ghosty.ticks > 7)
                    {
                        //Frame 3
                        ghosty.nextFrame = ghostyRunningRight[2];
                    }
                    else if(ghosty.ticks < 16 && ghosty.ticks > 11)
                    {
                        //Frame 4
                        ghosty.nextFrame = ghostyRunningRight[3];
                    }
                }
                //If the ghosty is going left
                else
                {
                    if(ghosty.ticks < 4)
                    {
                        //Frame 1
                        ghosty.nextFrame = ghostyRunningLeft[0];
                    }
                    else if(ghosty.ticks < 8 && ghosty.ticks > 3)
                    {
                        //Frame 2
                        ghosty.nextFrame = ghostyRunningLeft[1];
                    }
                    else if(ghosty.ticks < 12 && ghosty.ticks > 7)
                    {
                        //Frame 3
                        ghosty.nextFrame = ghostyRunningLeft[2];
                    }
                    else if(ghosty.ticks < 16 && ghosty.ticks > 11)
                    {
                        //Frame 4
                        ghosty.nextFrame = ghostyRunningLeft[3];
                    }
                }
            }
            //If ghosty is supposed to be jumpscaring, do that
            if(ghosty.disappear)
            {
                ghosty.X = 0;
                ghosty.Y = 0;
                ghosty.hidden = true;
                if(ghostyJumpTicks < 4)
                {
                    //Frame 1
                    nextGhostyJumpFrame = ghostyJumpFrames[0];
                    if(GhostyJumpscareOut.PlaybackState == PlaybackState.Stopped && !ghostyJumpscarePlayed)
                    {
                        GhostyJumpscareStream.CurrentTime = new TimeSpan(0);
                        GhostyJumpscareOut.Play();
                        ghostyJumpscarePlayed = true;
                    }
                }
                else if(ghostyJumpTicks > 3 && ghostyJumpTicks < 8)
                {
                    //Frame 2
                    nextGhostyJumpFrame = ghostyJumpFrames[1];
                }
                else if(ghostyJumpTicks > 7 && ghostyJumpTicks < 12)
                {
                    //Frame 3
                    nextGhostyJumpFrame = ghostyJumpFrames[2];
                }
                else if(ghostyJumpTicks > 11 && ghostyJumpTicks < 16)
                {
                    //Frame 4
                    nextGhostyJumpFrame = ghostyJumpFrames[3];
                }
                else if(ghostyJumpTicks > 15 && ghostyJumpTicks < 20)
                {
                    //Frame 5
                    nextGhostyJumpFrame = ghostyJumpFrames[4];
                }
                else if(ghostyJumpTicks > 19 && ghostyJumpTicks < 24)
                {
                    //Frame 6
                    nextGhostyJumpFrame = ghostyJumpFrames[5];
                }
                else if(ghostyJumpTicks > 23 && ghostyJumpTicks < 28)
                {
                    //Frame 7
                    nextGhostyJumpFrame = ghostyJumpFrames[6];
                }
                else if(ghostyJumpTicks > 27 && ghostyJumpTicks < 32)
                {
                    //Frame 8
                    nextGhostyJumpFrame = ghostyJumpFrames[7];
                }
                else if(ghostyJumpTicks > 31 && ghostyJumpTicks < 36)
                {
                    //Frame 9
                    nextGhostyJumpFrame = ghostyJumpFrames[8];
                }
                else
                {
                    ghostyJumpTicks = 0;
                    globalDisappear = false;
                    ghosty.disappear = false;
                    ghostyJumpscarePlayed = false;
                }
                ghostyJumpTicks++;
            }
            ghosty.hitbox.Location = new Point(scrollX + ghosty.X, scrollY + ghosty.Y);
            ghosty.ticks++;
        }

        private static void FuelPotManager(FuelPot fuelPot)
        {
            //Sets the FuelPot into the right position
            fuelPot.fuelbox.Location = new Point(scrollX + fuelPot.X, scrollY + fuelPot.Y + 10);
            fuelPot.hitbox.Location = new Point(scrollX + fuelPot.X + 20, scrollY + fuelPot.Y + 20);
            //Makes sure the FuelPot's ticks don't overflow
            if(fuelPot.ticks > 24)
            {
                fuelPot.ticks = 0;
            }
            //Sets the next frame to the correct frame based on the ticks
            //Frame 1
            if(fuelPot.ticks < 5)
            {
                fuelPot.nextFuelPot = fuelPotAnimation[0];
            }
            else
            {
                //Frame 2
                if(fuelPot.ticks < 9 && fuelPot.ticks > 4)
                {
                    fuelPot.nextFuelPot = fuelPotAnimation[1];
                }
                else
                {
                    //Frame 3
                    if(fuelPot.ticks < 13 && fuelPot.ticks > 8)
                    {
                        fuelPot.nextFuelPot = fuelPotAnimation[2];
                    }
                    else
                    {
                        //Frame 4
                        if(fuelPot.ticks < 17 && fuelPot.ticks > 12)
                        {
                            fuelPot.nextFuelPot = fuelPotAnimation[3];
                        }
                        else
                        {
                            //Frame 5
                            if(fuelPot.ticks < 21 && fuelPot.ticks > 16)
                            {
                                fuelPot.nextFuelPot = fuelPotAnimation[4];
                            }
                            else
                            {
                                //Frame 6
                                if(fuelPot.ticks < 25 && fuelPot.ticks > 20)
                                {
                                    fuelPot.nextFuelPot = fuelPotAnimation[5];
                                }
                            }
                        }
                    }
                }
            }
            fuelPot.ticks++;
        }

        //Manages the blubs
        private static void BlubManager(Blub blub)
        {
            if(!blub.hidden)
            {
                if(AnimationHelp.IsClose(new Point(blub.X, blub.Y), new Point(blub.point2X, blub.point2Y)))
                {
                    //Switchs the place of point1 and point2, so that the blub can continue only going after point2
                    Point switchPoint = new(blub.point1X, blub.point1Y);
                    blub.point1X = blub.point2X;
                    blub.point1Y = blub.point2Y;
                    blub.point2X = switchPoint.X;
                    blub.point2Y = switchPoint.Y;
                }
                //If the blub needs to go right
                else if(blub.X < blub.point2X)
                {
                    blub.X += 4;
                }
                //If the blub needs to go left
                else if(blub.X > blub.point2X)
                {
                    blub.X -= 4;
                }
                //If the blub needs to go up
                else if(blub.Y < blub.point2Y)
                {
                    blub.Y += 4;
                }
                //If the blub needs to go down
                else if(blub.Y > blub.point2Y)
                {
                    blub.Y -= 4;
                }
                //Manages blubTicks, used for animation
                if(blub.ticks > 9)
                {
                    blub.ticks = 0;
                }
                //Sets blub at the right frame and location
                if(blub.X < blub.point2X || blub.Y < blub.point2Y)
                {
                    //Running right animation
                    //Frame 1
                    if(blub.ticks < 6)
                    {
                        blub.nextFrame = blubRunningRight[0];
                    }
                    //Frame 2
                    else if(blub.ticks > 5 && blub.ticks < 8)
                    {

                        blub.nextFrame = blubRunningRight[1];
                    }
                    //Frame 3
                    else if(blub.ticks > 7 && blub.ticks < 9)
                    {
                        blub.nextFrame = blubRunningRight[2];
                    }
                }
                else
                {
                    //Running left animation
                    //Frame 1
                    if(blub.ticks < 6)
                    {
                        blub.nextFrame = blubRunningLeft[0];
                    }
                    //Frame 2
                    else if(blub.ticks > 5 && blub.ticks < 8)
                    {
                        blub.nextFrame = blubRunningLeft[1];
                    }
                    //Frame 3
                    else if(blub.ticks > 7 && blub.ticks < 9)
                    {
                        blub.nextFrame = blubRunningLeft[2];
                    }
                }
            }
            //If blub is supposed to be jumpscaring, do that
            if(blub.disappear)
            {
                blub.X = 0;
                blub.Y = 0;
                blub.hidden = true;
                if(blubJumpTicks < 6)
                {
                    nextBlubJumpFrame = blubJumpFrames[0];
                    if(BlubJumpscareOut.PlaybackState == PlaybackState.Stopped && !blubJumpscarePlayed)
                    {
                        BlubJumpscareStream.CurrentTime = new TimeSpan(0);
                        BlubJumpscareOut.Play();
                        blubJumpscarePlayed = true;
                    }
                }
                else if(blubJumpTicks > 5 && blubJumpTicks < 12)
                {
                    nextBlubJumpFrame = blubJumpFrames[1];
                }
                else if(blubJumpTicks > 11 && blubJumpTicks < 18)
                {
                    nextBlubJumpFrame = blubJumpFrames[2];
                }
                else if(blubJumpTicks > 17 && blubJumpTicks < 24)
                {
                    nextBlubJumpFrame = blubJumpFrames[3];
                }
                else
                {
                    blubJumpTicks = 0;
                    globalDisappear = false;
                    blub.disappear = false;
                    blubJumpscarePlayed = false;
                }
                blubJumpTicks++;
            }
            blub.hitbox.Location = new Point(scrollX + blub.X + ProgressionManagement.CurrentDungeon().xOffset, scrollY + blub.Y + ProgressionManagement.CurrentDungeon().yOffset);
            blub.ticks++;
        }

        //Description where called
        private void CollisionLogic()
        {
            //Declares variables
            int xOffset = 0;
            int yOffset = 0;
            int keysHeldCount = 0;
            bool diagonal = false;
            bool[] keysHeld = { rightHeld, leftHeld, upHeld, downHeld };
            //Makes sure that the hitbox location is in the right spot
            peanutHitbox.Location = new Point(peanutHitboxX, peanutHitboxY);
            //If peanut is touching the pillow, then make him sleep
            if(peanutHitbox.IntersectsWith(pillow))
            {
                StartSleep();
            }
            //If peanut is touching a blub, then add to scared meter and make the blub disappear
            for(int i = 0; i < ProgressionManagement.allBlubs.Length; i++)
            {
                if(peanutHitbox.IntersectsWith(ProgressionManagement.allBlubs[i].hitbox) && !ProgressionManagement.allBlubs[i].disappear && scaredMeter != 100 && !ProgressionManagement.allBlubs[i].hidden)
                {
                    scaredMeter += 50;
                    globalDisappear = true;
                    ProgressionManagement.allBlubs[i].disappear = true;
                }
            }
            //If peanut is touching a ghosty, then add to scared meter and make the ghosty disappear
            for(int i = 0; i < ProgressionManagement.allGhosties.Length; i++)
            {
                if(peanutHitbox.IntersectsWith(ProgressionManagement.allGhosties[i].hitbox) && !ProgressionManagement.allGhosties[i].disappear && scaredMeter != 100 && !ProgressionManagement.allGhosties[i].hidden)
                {
                    scaredMeter += 75;
                    globalDisappear = true;
                    ProgressionManagement.allGhosties[i].disappear = true;
                }
            }
            //If peanut is touching a boxFloof, then add to scared meter and make the boxFloof disappear
            for(int i = 0; i < ProgressionManagement.allBoxFloofs.Length; i++)
            {
                if(peanutHitbox.IntersectsWith(ProgressionManagement.allBoxFloofs[i].hitbox) && !ProgressionManagement.allBoxFloofs[i].disappear && scaredMeter != 100 && !ProgressionManagement.allBoxFloofs[i].hidden)
                {
                    scaredMeter += random.Next(50, 76);
                    globalDisappear = true;
                    ProgressionManagement.allBoxFloofs[i].disappear = true;
                }
            }
            //Makes sure that no more than 2 keys are held, and less than 1
            for(int i = 0; i < keysHeld.Length; i++)
            {
                if(keysHeld[i])
                {
                    keysHeldCount++;
                }
            }
            if(keysHeldCount > 2 || keysHeldCount < 1)
            {
                return;
            }
            //Returns if opposite directions are held
            if(rightHeld && leftHeld || upHeld && downHeld)
            {
                return;
            }
            //Checks if direction held is a diagonal
            if(keysHeldCount == 2 && !(keysHeld[0] && keysHeld[1] || keysHeld[2] && keysHeld[3]))
            {
                diagonal = true;
            }
            //If direction held is a diagonal, then do collision differently
            if(diagonal)
            {
                if(rightHeld)
                {
                    if(upHeld)
                    {
                        //Right and up are held
                        peanutHitbox.Location = new Point(peanutHitboxX + 6, peanutHitboxY - 6);
                        if(NoCollision())
                        {
                            scrollX -= 6;
                            scrollY += 6;
                            realPeanutX += 6;
                            realPeanutY -= 6;
                            rightLastHeld = true;
                            leftLastHeld = false;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX + 8, peanutHitboxY);
                        if(NoCollision())
                        {
                            scrollX -= 8;
                            realPeanutX += 8;
                            rightLastHeld = true;
                            leftLastHeld = false;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX, peanutHitboxY - 8);
                        if(NoCollision())
                        {
                            scrollY += 8;
                            realPeanutY -= 8;
                            return;
                        }
                        return;
                    }
                    else
                    {
                        //Right and down are held
                        peanutHitbox.Location = new Point(peanutHitboxX + 6, peanutHitboxY + 6);
                        if(NoCollision())
                        {
                            scrollX -= 6;
                            scrollY -= 6;
                            realPeanutX += 6;
                            realPeanutY += 6;
                            rightLastHeld = true;
                            leftLastHeld = false;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX + 8, peanutHitboxY);
                        if(NoCollision())
                        {
                            scrollX -= 8;
                            realPeanutX += 8;
                            rightLastHeld = true;
                            leftLastHeld = false;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX, peanutHitboxY + 8);
                        if(NoCollision())
                        {
                            scrollY -= 8;
                            realPeanutY -= -8;
                            return;
                        }
                        return;
                    }
                }
                else
                {
                    if(upHeld)
                    {
                        //Left and up are held
                        peanutHitbox.Location = new Point(peanutHitboxX - 6, peanutHitboxY - 6);
                        if(NoCollision())
                        {
                            scrollX += 6;
                            scrollY += 6;
                            realPeanutX -= 6;
                            realPeanutY -= 6;
                            rightLastHeld = false;
                            leftLastHeld = true;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX - 8, peanutHitboxY);
                        if(NoCollision())
                        {
                            scrollX += 8;
                            realPeanutX -= 8;
                            rightLastHeld = false;
                            leftLastHeld = true;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX, peanutHitboxY - 8);
                        if(NoCollision())
                        {
                            scrollY += 8;
                            realPeanutY -= 8;
                            return;
                        }
                        return;
                    }
                    else
                    {
                        //Left and down are held
                        peanutHitbox.Location = new Point(peanutHitboxX - 6, peanutHitboxY + 6);
                        if(NoCollision())
                        {
                            scrollX += 6;
                            scrollY -= 6;
                            realPeanutX -= 6;
                            realPeanutY += 6;
                            rightLastHeld = false;
                            leftLastHeld = true;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX - 8, peanutHitboxY);
                        if(NoCollision())
                        {
                            scrollX += 8;
                            realPeanutX -= 8;
                            rightLastHeld = false;
                            leftLastHeld = true;
                            return;
                        }
                        peanutHitbox.Location = new Point(peanutHitboxX, peanutHitboxY + 8);
                        if(NoCollision())
                        {
                            scrollY -= 8;
                            realPeanutY += 8;
                            return;
                        }
                        return;
                    }
                }
            }
            //Else do collision normally
            if(rightHeld)
            {
                xOffset = 8;
                yOffset = 0;
                rightLastHeld = true;
                leftLastHeld = false;
            }
            else if(leftHeld)
            {
                xOffset = -8;
                yOffset = 0;
                rightLastHeld = false;
                leftLastHeld = true;
            }
            else if(upHeld)
            {
                xOffset = 0;
                yOffset = -8;
            }
            else if(downHeld)
            {
                xOffset = 0;
                yOffset = 8;
            }
            peanutHitbox.Location = new Point(peanutHitboxX + xOffset, peanutHitboxY + yOffset);
            if(!NoCollision())
            {
                return;
            }
            scrollX -= xOffset;
            scrollY -= yOffset;
            realPeanutX += xOffset;
            realPeanutY += yOffset;
        }
        private static bool NoCollision()
        {
            //Refill the lantern fuel if touching a fuelpot's fuelbox and isn't hidden
            for(int i = 0; i < ProgressionManagement.allFuelPots.Length; i++)
            {
                if(peanutHitbox.IntersectsWith(ProgressionManagement.allFuelPots[i].fuelbox) && !ProgressionManagement.allFuelPots[i].hidden)
                {
                    lanternFuel = 100;
                    PlayRefillLanternLogic();
                }
            }
            //Check for no collision with walls
            for(int i = 0; i < ProgressionManagement.CurrentDungeon().collision.collidables.Length; i++)
            {
                if(peanutHitbox.IntersectsWith(ProgressionManagement.CurrentDungeon().collision.collidables[i]))
                {
                    return false;
                }
            }
            //If peanut is touching a fuel pot hitbox that isn't hidden, manage that
            for(int i = 0; i < ProgressionManagement.allFuelPots.Length; i++)
            {
                if(peanutHitbox.IntersectsWith(ProgressionManagement.allFuelPots[i].hitbox) && !ProgressionManagement.allFuelPots[i].hidden)
                {
                    return false;
                }
            }
            return true;
        }
        //Manages meters and endFrames
        private void MeterManager_Tick(object sender, EventArgs e)
        {
            //Decreases Lantern Fuel as longs as it's not empty
            if(lanternFuel != 0)
            {
                lanternFuel--;
            }
            //Ends the game if the Scared Meter is full
            if(scaredMeter == 100)
            {
                if(!(endTicks >= 18))
                {
                    nextEndFrame = endFrames[endTicks];
                    endTicks++;
                }
                else if(endTicks >= 18 && endTicks < 28)
                {
                    if(DisappointedMeowOut.PlaybackState == PlaybackState.Stopped && !disappointedMeowPlayed)
                    {
                        DisappointedMeowStream.CurrentTime = new TimeSpan(0);
                        DisappointedMeowOut.Play();
                        disappointedMeowPlayed = true;
                    }
                    isRestarting = true;
                    nextRestartMessageFrame = restartMessage[0];
                    endTicks++;
                }
                else if(endTicks >= 28 && endTicks < 38)
                {
                    nextRestartMessageFrame = restartMessage[1];
                    endTicks++;
                }
                else if(endTicks >= 38 && endTicks < 48)
                {
                    nextRestartMessageFrame = restartMessage[2];
                    endTicks++;
                }
                else
                {
                    ResetGame();
                }
                return;
            }
            //Decreases scared meter
            if(lanternFuel > 0 && scaredMeter - 1 >= 0 && scaredMeterDecreaseTicks >= 10)
            {
                scaredMeter--;
                scaredMeterDecreaseTicks = 0;
            }
            if(scaredMeterDecreaseTicks >= 10)
            {
                scaredMeterDecreaseTicks = 0;
            }
            scaredMeterDecreaseTicks++;
        }

        //Resets almost all fluid variables
        private static void ResetGame()
        {
            //Resets all variables that don't rely on which level peanut is in
            //Control Flags
            rightHeld = false;
            leftHeld = false;
            upHeld = false;
            downHeld = false;
            rightLastHeld = true;
            leftLastHeld = false;
            isSleeping = false;
            isLoafSleeping = false;
            isRestarting = false;
            //Ticks
            peanutTicks = 0;
            peanutSleepTicks = 0;
            blubJumpTicks = 0;
            ghostyJumpTicks = 0;
            boxFloofJumpTicks = 0;
            endTicks = 0;
            //UI Values
            scaredMeter = 0;
            lanternFuel = 100;
            //Sound Played Flags
            disappointedMeowPlayed = false;
            blubJumpscarePlayed = false;
            ghostyJumpscarePlayed = false;
            boxFloofJumpscarePlayed = false;
            //Animation
            nextPeanutFrame = peanutRunningRight[0];
            //Resets all variables for the tutorial dungeon
            if(ProgressionManagement.CurrentDungeon() == tutorialDungeon)
            {
                //Scroll
                scrollX = 500;
                scrollY = -1920;
                //Peanut Management Values
                realPeanutX = 480;
                realPeanutY = 2560;
                //Enemies
                ProgressionManagement.allBlubs[0].X = 1760;
                ProgressionManagement.allBlubs[0].Y = 2560;
                ProgressionManagement.allBlubs[1].X = 1120;
                ProgressionManagement.allBlubs[1].Y = 1280;
                ProgressionManagement.allBlubs[2].X = 1440;
                ProgressionManagement.allBlubs[2].Y = 800;
                for(int i = 0; i < 3; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = false;
                }
            }
            else if(ProgressionManagement.CurrentDungeon() == dungeon1)
            {
                //Scroll
                scrollX = 500;
                scrollY = -1920;
                //Peanut Management Values
                realPeanutX = 4000;
                realPeanutY = 1280;
                //Sets elements to their correct x and y values
                ProgressionManagement.allBlubs[3].X = -1600;
                ProgressionManagement.allBlubs[3].Y = 1760;
                ProgressionManagement.allBlubs[4].X = 0;
                ProgressionManagement.allBlubs[4].Y = 3520;
                ProgressionManagement.allBlubs[5].X = -3040;
                ProgressionManagement.allBlubs[5].Y = 4960;
                ProgressionManagement.allBlubs[6].X = 320;
                ProgressionManagement.allBlubs[6].Y = 5600;
                ProgressionManagement.allBlubs[7].X = 1920;
                ProgressionManagement.allBlubs[7].Y = 6080;
                ProgressionManagement.allBlubs[8].X = 1280;
                ProgressionManagement.allBlubs[8].Y = 4160;
                ProgressionManagement.allGhosties[0].X = -800;
                ProgressionManagement.allGhosties[0].Y = 3840;
                ProgressionManagement.allGhosties[1].X = -1760;
                ProgressionManagement.allGhosties[1].Y = 5760;
                ProgressionManagement.allGhosties[2].X = 1120;
                ProgressionManagement.allGhosties[2].Y = 4960;
                ProgressionManagement.allGhosties[3].X = 2240;
                ProgressionManagement.allGhosties[3].Y = 4160;
                ProgressionManagement.allGhosties[4].X = 2560;
                ProgressionManagement.allGhosties[4].Y = 4160;
                //Un-hides dungeon1 elements
                for(int i = 3; i < 9; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = false;
                }
                for(int i = 0; i < 5; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = false;
                }
                for(int i = 1; i < 6; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = false;
                }
                //Hides previous elements
                for(int i = 0; i < 3; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = true;
                }
                ProgressionManagement.allFuelPots[0].hidden = true;
            }
            else if(ProgressionManagement.CurrentDungeon() == dungeon2)
            {
                //Scroll
                scrollX = 0;
                scrollY = 0;
                //Peanut Management Values
                realPeanutX = 3040;
                realPeanutY = 2080;
                //Sets elements to their correct x and y values
                ProgressionManagement.allBlubs[9].X = -1120;
                ProgressionManagement.allBlubs[9].Y = 2240;
                ProgressionManagement.allBlubs[10].X = 800;
                ProgressionManagement.allBlubs[10].Y = 2880;
                ProgressionManagement.allBlubs[11].X = 2560;
                ProgressionManagement.allBlubs[11].Y = 2880;
                ProgressionManagement.allBlubs[12].X = 1440;
                ProgressionManagement.allBlubs[12].Y = -640;
                ProgressionManagement.allBlubs[13].X = 4480;
                ProgressionManagement.allBlubs[13].Y = 1920;
                ProgressionManagement.allGhosties[5].X = 1600;
                ProgressionManagement.allGhosties[5].Y = 480;
                ProgressionManagement.allGhosties[6].X = 1600;
                ProgressionManagement.allGhosties[6].Y = 2400;
                ProgressionManagement.allGhosties[7].X = 3520;
                ProgressionManagement.allGhosties[7].Y = 640;
                ProgressionManagement.allGhosties[8].X = -1120;
                ProgressionManagement.allGhosties[8].Y = 960;
                //Un-hides current elements
                for(int i = 6; i < 12; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = false;
                }
                for(int i = 5; i < 9; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = false;
                }
                for(int i = 9; i < 14; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = false;
                }
                //Hides previous elements
                for(int i = 0; i < 9; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = true;
                }
                for(int i = 0; i < 5; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = true;
                }
                for(int i = 0; i < 6; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = true;
                }
            }
            else if(ProgressionManagement.CurrentDungeon() == dungeon3)
            {
                //Scroll
                scrollX = 0;
                scrollY = 0;
                //Peanut management values
                realPeanutX = 480;
                realPeanutY = 3840;
                //Sets elements to their correct x and y values
                ProgressionManagement.allBlubs[14].X = 3200;
                ProgressionManagement.allBlubs[14].Y = -480;
                ProgressionManagement.allBlubs[15].X = 2400;
                ProgressionManagement.allBlubs[15].Y = -1920;
                ProgressionManagement.allGhosties[9].X = 4480;
                ProgressionManagement.allGhosties[9].Y = -1920;
                ProgressionManagement.allGhosties[10].X = 1760;
                ProgressionManagement.allGhosties[10].Y = -960;
                ProgressionManagement.allBoxFloofs[0].X = 1920;
                ProgressionManagement.allBoxFloofs[0].Y = 640;
                ProgressionManagement.allBoxFloofs[1].X = 4320;
                ProgressionManagement.allBoxFloofs[1].Y = -160;
                ProgressionManagement.allBoxFloofs[2].X = 5760;
                ProgressionManagement.allBoxFloofs[2].Y = -160;
                ProgressionManagement.allBoxFloofs[3].X = 3840;
                ProgressionManagement.allBoxFloofs[3].Y = -2080;
                //Un-hides current elements
                ProgressionManagement.allBlubs[14].hidden = false;
                ProgressionManagement.allBlubs[15].hidden = false;
                ProgressionManagement.allGhosties[9].hidden = false;
                ProgressionManagement.allGhosties[10].hidden = false;
                for(int i = 0; i < 4; i++)
                {
                    ProgressionManagement.allBoxFloofs[i].hidden = false;
                }
                for(int i = 12; i < 16; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = false;
                }
                //Hides previous elements
                for(int i = 0; i < 14; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = true;
                }
                for(int i = 0; i < 9; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = true;
                }
                for(int i = 0; i < 12; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = true;
                }
            }
            else if(ProgressionManagement.CurrentDungeon() == dungeon4)
            {
                //Scroll
                scrollX = 0;
                scrollY = 0;
                //Peanut Management Values
                realPeanutX = 460;
                realPeanutY = 640;
                //Sets elements to their correct x and y values
                ProgressionManagement.allBlubs[16].X = 3360;
                ProgressionManagement.allBlubs[16].Y = 3040;
                ProgressionManagement.allBlubs[17].X = 5120;
                ProgressionManagement.allBlubs[17].Y = 1760;
                ProgressionManagement.allBlubs[18].X = 6720;
                ProgressionManagement.allBlubs[18].Y = 3200;
                ProgressionManagement.allBlubs[19].X = 7040;
                ProgressionManagement.allBlubs[19].Y = 6560;
                ProgressionManagement.allBlubs[20].X = 3040;
                ProgressionManagement.allBlubs[20].Y = 6720;
                ProgressionManagement.allGhosties[11].X = 3360;
                ProgressionManagement.allGhosties[11].Y = 320;
                ProgressionManagement.allGhosties[12].X = 4160;
                ProgressionManagement.allGhosties[12].Y = 2240;
                ProgressionManagement.allGhosties[13].X = 7520;
                ProgressionManagement.allGhosties[13].Y = 4000;
                ProgressionManagement.allGhosties[14].X = 6560;
                ProgressionManagement.allGhosties[14].Y = 4480;
                ProgressionManagement.allGhosties[15].X = 2720;
                ProgressionManagement.allGhosties[15].Y = 5440;
                ProgressionManagement.allGhosties[16].X = 1120;
                ProgressionManagement.allGhosties[16].Y = 5440;
                ProgressionManagement.allBoxFloofs[4].X = 1760;
                ProgressionManagement.allBoxFloofs[4].Y = 2240;
                ProgressionManagement.allBoxFloofs[5].X = 2400;
                ProgressionManagement.allBoxFloofs[5].Y = 3360;
                ProgressionManagement.allBoxFloofs[6].X = 2240;
                ProgressionManagement.allBoxFloofs[6].Y = 4320;
                ProgressionManagement.allBoxFloofs[7].X = 1600;
                ProgressionManagement.allBoxFloofs[7].Y = 7200;
                ProgressionManagement.allBoxFloofs[8].X = 4000;
                ProgressionManagement.allBoxFloofs[8].Y = 7200;
                ProgressionManagement.allBoxFloofs[9].X = 4160;
                ProgressionManagement.allBoxFloofs[9].Y = 6080;
                ProgressionManagement.allBoxFloofs[10].X = 6080;
                ProgressionManagement.allBoxFloofs[10].Y = 3040;
                ProgressionManagement.allBoxFloofs[11].X = 6080;
                ProgressionManagement.allBoxFloofs[11].Y = 960;
                //Un-hides current elements
                for(int i = 16; i < 21; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = false;
                }
                for(int i = 11; i < 17; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = false;
                }
                for(int i = 4; i < 12; i++)
                {
                    ProgressionManagement.allBoxFloofs[i].hidden = false;
                }
                for(int i = 16; i < 28; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = false;
                }
                //Hides previous elements
                for(int i = 0; i < 16; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = true;
                }
                for(int i = 0; i < 11; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = true;
                }
                for(int i = 0; i < 4; i++)
                {
                    ProgressionManagement.allBoxFloofs[i].hidden = true;
                }
                for(int i = 0; i < 16; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = true;
                }
            }
            else
            {
                //Scroll
                scrollX = -1260;
                scrollY = -2080;
                //Peanut Management Values
                realPeanutX = 1260;
                realPeanutY = 2080;
                //Un-hides current elements
                for(int i = 28; i < 32; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = false;
                }
                //Hides previous elements
                for(int i = 0; i < ProgressionManagement.allBlubs.Length; i++)
                {
                    ProgressionManagement.allBlubs[i].hidden = true;
                }
                for(int i = 0; i < ProgressionManagement.allGhosties.Length; i++)
                {
                    ProgressionManagement.allGhosties[i].hidden = true;
                }
                for(int i = 0; i < ProgressionManagement.allBoxFloofs.Length; i++)
                {
                    ProgressionManagement.allBoxFloofs[i].hidden = true;
                }
                for(int i = 0; i < 28; i++)
                {
                    ProgressionManagement.allFuelPots[i].hidden = true;
                }
            }
        }

        private void StartSleep()
        {
            isSleeping = true;
            peanutTicks = 0;
            SleepManager.Enabled = true;
        }

        //Manages all the sleeping stuff, if peanut is sleeping
        private void SleepManager_Tick(object sender, EventArgs e)
        {
            //Sleep walking is weird, it returns true if it had to move peanut, returns false if he needs to start the sleeping animation
            if(IsSleepWalking())
            {
                //Manages peanutTicks, used for animation
                if(peanutTicks > 16)
                {
                    peanutTicks = 0;
                }
                //Animates left running when needed -60 -80
                if(AnimationHelp.IsLeft(new Point(scrollX + realPeanutX, scrollY + realPeanutY), new Point(scrollX + ProgressionManagement.CurrentDungeon().pillowX + ProgressionManagement.CurrentDungeon().pillowOffsetX, scrollY + ProgressionManagement.CurrentDungeon().pillowY + ProgressionManagement.CurrentDungeon().pillowOffsetY)))
                {
                    rightLastHeld = false;
                    leftLastHeld = true;
                    //Frame 1
                    if(peanutTicks < 4)
                    {
                        nextPeanutFrame = peanutRunningLeft[0];
                    }
                    else
                    {
                        //Frame 2 and frame 4
                        if(peanutTicks > 3 && peanutTicks < 8 || peanutTicks > 11 && peanutTicks < 16)
                        {
                            nextPeanutFrame = peanutRunningLeft[1];
                        }
                        else
                        {
                            //Frame 3
                            if(peanutTicks > 7 && peanutTicks < 12)
                            {
                                nextPeanutFrame = peanutRunningLeft[2];
                            }
                        }
                    }
                }
                //Animates right running when needed
                else
                {
                    rightLastHeld = true;
                    leftLastHeld = false;
                    //Frame 1
                    if(peanutTicks < 4)
                    {
                        nextPeanutFrame = peanutRunningRight[0];
                    }
                    else
                    {
                        //Frame 2 and frame 4
                        if(peanutTicks > 3 && peanutTicks < 8 || peanutTicks > 11 && peanutTicks < 16)
                        {
                            nextPeanutFrame = peanutRunningRight[1];
                        }
                        else
                        {
                            //Frame 3
                            if(peanutTicks > 7 && peanutTicks < 12)
                            {
                                nextPeanutFrame = peanutRunningRight[2];
                            }
                        }
                    }
                }
                peanutTicks++;
            }
            else
            {
                //Manages peanutSleepTicks, used for animation of peanut sleeping
                if(peanutSleepTicks > 36)
                {
                    peanutSleepTicks = 16;
                    peanutNextLevelTicks++;
                    if(peanutNextLevelTicks == 10 && ProgressionManagement.CurrentDungeon() != finalPillowRoom)
                    {
                        peanutNextLevelTicks = 0;
                        PeanutPurrOut.Stop();
                        NextDungeon();
                    }
                    else if(peanutNextLevelTicks == 10 && ProgressionManagement.CurrentDungeon() == finalPillowRoom)
                    {
                        peanutNextLevelTicks = 0;
                        PeanutPurrOut.Stop();
                        SleepManager.Enabled = false;
                        isLoafSleeping = false;
                        win = true;
                    }
                }
                if(peanutSleepTicks > 15)
                {
                    isLoafSleeping = true;
                    if(PeanutPurrOut.PlaybackState == PlaybackState.Stopped && !win)
                    {
                        PeanutPurrStream.CurrentTime = new TimeSpan(0);
                        PeanutPurrOut.Play();
                    }
                }
                //Animates the peanut sleeping right
                if(rightLastHeld)
                {
                    if(isLoafSleeping)
                    {
                        if(peanutSleepTicks > 15 && peanutSleepTicks < 25)
                        {
                            //Frame 1
                            nextPeanutFrame = peanutSleepingRight[4];
                        }
                        else if(peanutSleepTicks > 24 && peanutSleepTicks < 31)
                        {
                            //Frame 2
                            nextPeanutFrame = peanutSleepingRight[5];
                        }
                        else if(peanutSleepTicks > 30 && peanutSleepTicks < 36)
                        {
                            //Frame 3
                            nextPeanutFrame = peanutSleepingRight[6];
                        }
                    }
                    else
                    {
                        if(peanutSleepTicks < 4)
                        {
                            //Frame 1
                            nextPeanutFrame = peanutSleepingRight[0];
                        }
                        else if(peanutSleepTicks > 3 && peanutSleepTicks < 8)
                        {
                            //Frame 2
                            nextPeanutFrame = peanutSleepingRight[1];
                        }
                        else if(peanutSleepTicks > 7 && peanutSleepTicks < 12)
                        {
                            //Frame 3
                            nextPeanutFrame = peanutSleepingRight[2];
                        }
                        else if(peanutSleepTicks > 11 && peanutSleepTicks < 16)
                        {
                            //Frame 4
                            nextPeanutFrame = peanutSleepingRight[3];
                        }
                    }
                }
                //Animates the peanut sleeping left
                else
                {
                    if(isLoafSleeping)
                    {
                        if(peanutSleepTicks > 15 && peanutSleepTicks < 20)
                        {
                            //Frame 1
                            nextPeanutFrame = peanutSleepingLeft[4];
                        }
                        else if(peanutSleepTicks > 19 && peanutSleepTicks < 24)
                        {
                            //Frame 2
                            nextPeanutFrame = peanutSleepingLeft[5];
                        }
                        else if(peanutSleepTicks > 23 && peanutSleepTicks < 28)
                        {
                            //Frame 3
                            nextPeanutFrame = peanutSleepingLeft[6];
                        }
                    }
                    else
                    {
                        if(peanutSleepTicks < 4)
                        {
                            //Frame 1
                            nextPeanutFrame = peanutSleepingLeft[0];
                        }
                        else if(peanutSleepTicks > 3 && peanutSleepTicks < 8)
                        {
                            //Frame 2
                            nextPeanutFrame = peanutSleepingLeft[1];
                        }
                        else if(peanutSleepTicks > 7 && peanutSleepTicks < 12)
                        {
                            //Frame 3
                            nextPeanutFrame = peanutSleepingLeft[2];
                        }
                        else if(peanutSleepTicks > 11 && peanutSleepTicks < 16)
                        {
                            //Frame 4
                            nextPeanutFrame = peanutSleepingLeft[3];
                        }
                    }
                }
                peanutSleepTicks++;
            }
        }

        //The movement code for Peanut to walk to the pillow
        private static bool IsSleepWalking()
        {
            Point startPoint = new(scrollX + realPeanutX, scrollY + realPeanutY);
            Point endPoint = new(scrollX + ProgressionManagement.CurrentDungeon().pillowX + ProgressionManagement.CurrentDungeon().pillowOffsetX, scrollY + ProgressionManagement.CurrentDungeon().pillowY + ProgressionManagement.CurrentDungeon().pillowOffsetY);
            if(AnimationHelp.IsClose(startPoint, endPoint))
            {
                return false;
            }
            int[] newPointInformation = AnimationHelp.FindCloserPoint(startPoint, endPoint);
            Point newPoint = new(newPointInformation[0], newPointInformation[1]);
            scrollX = newPoint.X - realPeanutX;
            scrollY = newPoint.Y - realPeanutY;
            realPeanutX -= newPointInformation[2];
            realPeanutY -= newPointInformation[3];
            return true;
        }

        private void NextDungeon()
        {
            ProgressionManagement.currentDungeon++;
            SleepManager.Enabled = false;
            ResetGame();
        }

        private void GraphicsTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void AmbientSoundTimer_Tick(object sender, EventArgs e)
        {
            if(scaredMeter != 100 && !isSleeping && !globalDisappear && !menu && !win && ProgressionManagement.CurrentDungeon() != finalPillowRoom)
            {
                WaveStream[] ambientSoundStreams = { BrickFallStream, DistantMeowStream, MetalClangStream, WindStream, WoodCreakStream };
                WaveOut[] ambientSoundOuts = { BrickFallOut, DistantMeowOut, MetalClangOut, WindOut, WoodCreakOut };
                int randomSoundIndex = random.Next(0, 5);
                ambientSoundStreams[randomSoundIndex].CurrentTime = new TimeSpan(0);
                ambientSoundOuts[randomSoundIndex].Play();
                AmbientSoundTimer.Interval = random.Next(15, 31) * 1000;
            }
        }

        private static void PlayRefillLanternLogic()
        {
            if(RefillLanternOut.PlaybackState == PlaybackState.Stopped)
            {
                RefillLanternStream.CurrentTime = new TimeSpan(0);
                RefillLanternOut.Play();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(menu)
            {
                if(IsPlayBounds(e.X, e.Y))
                {
                    ResetGame();
                    menu = false;
                }
                else if(IsBoopBounds(e.X, e.Y) && BoopOut.PlaybackState == PlaybackState.Stopped)
                {
                    BoopStream.CurrentTime = new TimeSpan(0);
                    BoopOut.Play();
                }
            }
        }

        private static bool IsBoopBounds(int x, int y)
        {
            mouseCheck.Location = new Point(x, y);
            if(mouseCheck.IntersectsWith(boopButton))
            {
                return true;
            }
            return false;
        }

        private static bool IsPlayBounds(int x, int y)
        {
            mouseCheck.Location = new Point(x, y);
            if(mouseCheck.IntersectsWith(playButton))
            {
                return true;
            }
            return false;
        }
    }
}