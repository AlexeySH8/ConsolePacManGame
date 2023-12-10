using PacManGame;

namespace MyPackMan
{
    class Map
    {
        public const int MapHeight = 16;
        public const int MapWidth = 23;
        public const char WallTexture = '0';
        public const char FoodTexture = '*';
        public const char BoostTexture = '▓';
        public const char PacTexture = '1';
        public const char GhostTexture = '6';
        public const ConsoleColor FoodColor = ConsoleColor.DarkCyan;
        public const ConsoleColor PacColor = ConsoleColor.Yellow;
        public const ConsoleColor GhostColor1 = ConsoleColor.Red;
        public const ConsoleColor GhostColor2 = ConsoleColor.DarkGreen;
        public const ConsoleColor GhostColor3 = ConsoleColor.Magenta;
        public const ConsoleColor GhostColor4 = ConsoleColor.DarkYellow;
        private static char[,] MapLayout = new char[MapHeight, MapWidth]
   {
    { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' },
    { '0', '▓', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▓', ' ', ' ', '0' },
    { '0', ' ', '0', '0', '0', ' ', '0', '0', '0', '0', ' ', '0', ' ', '0', '0', '0', '0', ' ', '0', '0', '0', ' ', '0' },
    { '0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '0' },
    { '0', ' ', '0', '0', '0', ' ', ' ', ' ', ' ', '0', '0', '0', '0', '0', ' ', ' ', ' ', ' ', '0', '0', '0', ' ', '0' },
    { '0', ' ', ' ', ' ', ' ', ' ', ' ', '0', ' ', ' ', '0', '0', '0', ' ', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' ', '0' },
    { '0', '0', '0', '0', '0', ' ', ' ', '0', '0', '0', 'x', 'x', 'x', '0', '0', '0', ' ', ' ', '0', '0', '0', '0', '0' },
    { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '0', ' ', '0', '0', '0', '0', '0', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
    { '0', '0', '0', '0', '0', ' ', ' ', ' ', ' ', ' ', ' ', 'x', ' ', ' ', ' ', ' ', ' ', ' ', '0', '0', '0', '0', '0' },
    { '0', ' ', ' ', ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' ', '0' },
    { '0', ' ', ' ', '0', '0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '0', '0', ' ', ' ', '0' },
    { '0', '0', ' ', ' ', '0', ' ', ' ', ' ', ' ', '0', '0', '0', '0', '0', ' ', ' ', ' ', ' ', '0', ' ', '▓', '0', '0' },
    { '0', '▓', ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' ', '0' },
    { '0', ' ', '0', '0', '0', '0', '0', '0', ' ', ' ', ' ', ' ', ' ', ' ', '0', '0', '0', '0', '0', '0', '0', ' ', '0' },
    { '0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '0' },
    { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' }
   };

        public void DrawEnvironment()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    switch (MapLayout[y, x])
                    {
                        case '0':
                            new Pixel(x, y, WallTexture).Draw();
                            break;
                        case BoostTexture:
                            MapLayout[y, x] = BoostTexture;
                            new Pixel(x, y, BoostTexture, FoodColor).Draw();
                            break;
                        case ' ':
                            MapLayout[y, x] = FoodTexture;
                            new Pixel(x, y, FoodTexture, FoodColor).DrawPixelFood();
                            break;
                    }
                }
            }
            DrawLivesLine();
        }

        public static void DrawLivesLine()
        {
            for (int x = 0; x < Game.CurrentLives * 2; x++)
            {
                if (x % 2 == 0)
                    new Pixel(x, MapHeight - 1, WallTexture, PacColor).Draw();
            }
        }

        public static void ClearLivesLine()
        {
            for (int x = 0; x < Game.MaxLives * 2 - Game.CurrentLives * 2; x++)
            {
                if (x % 2 == 0)
                    new Pixel(x, MapHeight - 1, WallTexture).Draw();
            }
        }

        public static async Task OpenGateAsync()
        {
            await Task.Delay(3000);
            ClearPixel(10, 5);
            ClearPixel(12, 5);
        }

        public static char CurrentPixel(int x, int y)
        {
            return MapLayout[y, x];
        }

        public static void ClearPixel(int x, int y)
        {
            MapLayout[y, x] = ' ';
            new Pixel(x, y, WallTexture).Clear();
        }
    }
}
