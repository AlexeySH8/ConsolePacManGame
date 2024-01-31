namespace MyPackMan
{
    class Сreature
    {
        private char _creatureTexture { get; set; }
        private ConsoleColor _creatureColor { get; set; }
        public Pixel Head;
        private const int OutOfBounds = -1;
        public Сreature(int x, int y, char creatureTexture,
            ConsoleColor color)
        {
            _creatureTexture = creatureTexture;
            _creatureColor = color;
            Head = new Pixel(x, y, _creatureTexture, _creatureColor);
        }

        protected void SetNewPosition(Direction direction)
        {
            Head.Clear();
            MovementOutsideTheMap(direction);
            int newHeadX = Head.X;
            int newHeadY = Head.Y;
            switch (direction)
            {
                case Direction.Right:
                    newHeadX++;
                    break;
                case Direction.Left:
                    newHeadX--;
                    break;
                case Direction.Up:
                    newHeadY--;
                    break;
                case Direction.Down:
                    newHeadY++;
                    break;
            }
            UpdateHeadPosition(newHeadX, newHeadY);
        }

        private void MovementOutsideTheMap(Direction direction)
        {
            Head.X = (direction == Direction.Right && Head.X >= Map.MapWidth - 1) ? OutOfBounds :
                (direction == Direction.Left && Head.X <= 0) ? Map.MapWidth : Head.X;

            Head.Y = (direction == Direction.Down && Head.Y >= Map.MapHeight - 1) ? OutOfBounds :
                (direction == Direction.Up && Head.Y <= 0) ? Map.MapHeight : Head.Y;
        }

        private void UpdateHeadPosition(int newHeadX, int newHeadY)
        {
            Head = (Map.CurrentPixel(newHeadX, newHeadY) != Map.WallTexture)
                ? new Pixel(newHeadX, newHeadY, _creatureTexture, _creatureColor)
                : new Pixel(Head.X, Head.Y, _creatureTexture, _creatureColor);
        }
    }
}
