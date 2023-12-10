using PacManGame;

namespace MyPackMan
{
    class Pac : Сreature
    {
        private const char _pacTexture = Map.PacTexture;

        public Pac(int x, int y, ConsoleColor color)
            : base(x, y, _pacTexture, color)
        {

        }

        public void Move(Direction direction)
        {
            SetNewPosition(direction);
            HandleFoodOrBoost();
            Head.Draw();
        }

        private void HandleFoodOrBoost()
        {
            switch (Map.CurrentPixel(Head.X, Head.Y))
            {
                case Map.FoodTexture:
                    HandleFood();
                    break;

                case Map.BoostTexture:
                    HandleBoost();
                    break;
            }
        }

        private void HandleFood()
        {
            Map.ClearPixel(Head.X, Head.Y);
            Game.EatenFoodScore += Game.ScoreForFood;
        }

        private void HandleBoost()
        {
            Map.ClearPixel(Head.X, Head.Y);
            Game.IsGhostInvulnerable = false;
            Task.Run(() => Console.Beep(300, 800));
            Task.Run(async () =>
            {
                await Task.Delay(Game.BoostDuration);
                Game.IsGhostInvulnerable = true;
            });
        }
    }
}
