using MyPackMan;
using static System.Console;

namespace PacManGame
{
    class Game
    {
        public const int PixelSize = 3;
        public static int MaxLives = 3;
        private static int SpeedGame = 150;
        public const int BoostDuration = 5000;
        public const int ScoreForGhost = 200;
        public const int ScoreForFood = 10;
        private const int MaxScoreFood = 1960;
        public static int CurrentLives;
        private const int ScreenWidth = Map.MapWidth * PixelSize;
        private const int ScreenHeight = Map.MapHeight * PixelSize;
        private static List<Ghost> Ghosts = new List<Ghost>()
        {
            new Ghost(11, 8, Map.GhostColor1),
            new Ghost(10, 6, Map.GhostColor2),
            new Ghost(12, 6, Map.GhostColor3),
            new Ghost(11, 6, Map.GhostColor4)
        };
        public static bool IsGhostInvulnerable = true;
        public static bool IsMatchContinue;
        public static int EatenFoodScore;
        public static int EatenGhostsScore;

        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;
            StartMatch();
            Clear();
            SetCursorPosition((ScreenWidth / 2) - (ScreenWidth / 10),
                (ScreenHeight / 2) - (ScreenHeight / 10));
            Write(EatenFoodScore + EatenGhostsScore + " YOUR SCORE \n \t");
            if (EatenFoodScore == MaxScoreFood)
                WriteLine("                " +
                    "\tYOU WIN");
        }

        public static void StartMatch()
        {
            IsMatchContinue = true;
            CurrentLives = MaxLives;
            EatenFoodScore = 0;
            EatenGhostsScore = 0;

            var map = new Map();
            map.DrawEnvironment();
            var pac = new Pac(11, 13, Map.PacColor);
            pac.Head.Draw();
            var currentDirection = Direction.Stop;

            foreach (var ghost in Ghosts)
                ghost.Head.Draw();

            Map.OpenGateAsync();

            while (IsMatchContinue)
            {
                Thread.Sleep(SpeedGame);
                if (EatenFoodScore == MaxScoreFood || CurrentLives < 0)
                    IsMatchContinue = false;
                foreach (var ghost in Ghosts)
                    ghost.Move(pac);
                currentDirection = ReadMove(currentDirection);
                pac.Move(currentDirection);
            }
            return;
        }

        private static Direction ReadMove(Direction currentDirection)
        {
            if (!KeyAvailable) return currentDirection;
            ConsoleKeyInfo keyInfo = ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    currentDirection = Direction.Right;
                    break;
                case ConsoleKey.LeftArrow:
                    currentDirection = Direction.Left;
                    break;
                case ConsoleKey.UpArrow:
                    currentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    currentDirection = Direction.Down;
                    break;
            }
            return currentDirection;
        }
    }
}
