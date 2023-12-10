using PacManGame;

namespace MyPackMan
{
    class Ghost : Сreature
    {
        private const char GhostTexture = Map.GhostTexture;
        private const ConsoleColor GhostIsFoodColor = ConsoleColor.Blue;
        private Direction _direction { get; set; }
        private int _movesBeforeDirectionChange = 3;
        private const int spawnX = 11;
        private const int spawnY = 6;
        private Dictionary<int, Direction> _dicDirection = new Dictionary<int, Direction>()
        {
            {0,Direction.Right}, {1,Direction.Left},
            {2,Direction.Up}, {3,Direction.Down}
        };

        public Ghost(int x, int y, ConsoleColor color)
            : base(x, y, GhostTexture, color)
        {

        }

        public void Move(Pac pac)
        {
            int oldX = Head.X;
            int oldY = Head.Y;
            SetNewPosition(_direction);
            if (!Game.IsGhostInvulnerable)
                Head.Color = GhostIsFoodColor;
            HandleCollision(pac, oldX, oldY);
            Head.Draw();
            MakeFoodAfterStepGhost(oldX, oldY);
            if (_movesBeforeDirectionChange <= 0)
                _direction = GenerateRandomDirection();
            _movesBeforeDirectionChange--;
        }

        private void HandleCollision(Pac pac, int oldX, int oldY)
        {
            if ((pac.Head.X == Head.X && pac.Head.Y == Head.Y) ||
                (pac.Head.X == oldX && pac.Head.Y == oldY))
            {
                if (Game.IsGhostInvulnerable)
                {                   
                    Game.CurrentLives--;
                    Map.ClearLivesLine();
                    Task.Run(() => Console.Beep(500, 500));
                }
                else
                {
                    Head.X = spawnX;
                    Head.Y = spawnY;
                    Game.EatenGhostsScore += Game.ScoreForGhost;
                }
            }
        }

        private void MakeFoodAfterStepGhost(int oldX, int oldY)
        {
            if ((Map.CurrentPixel(oldX, oldY) == Map.FoodTexture) &&
                (oldX != Head.X || oldY != Head.Y))
                new Pixel(oldX, oldY, Map.FoodTexture, Map.FoodColor).DrawPixelFood();

            if ((Map.CurrentPixel(oldX, oldY) == Map.BoostTexture) &&
                (oldX != Head.X || oldY != Head.Y))
                new Pixel(oldX, oldY, Map.BoostTexture, Map.FoodColor).Draw();
        }

        private Direction GenerateRandomDirection()
        {
            var random = new Random();
            var keyDirection = random.Next(0, 4);
            return _dicDirection[keyDirection];
        }
    }
}
