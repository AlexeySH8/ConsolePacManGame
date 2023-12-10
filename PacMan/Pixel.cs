using PacManGame;

namespace MyPackMan
{
    struct Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char TexturePixel;
        public ConsoleColor Color { get; set; }
        private const int PixelSize = Game.PixelSize;      
        public Pixel(int x, int y, char texturePixel,
            ConsoleColor color = ConsoleColor.White)
        {
            X = x;
            Y = y;
            Color = color;
            TexturePixel = texturePixel;
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(TexturePixel);
                }
            }
        }

        public void DrawPixelFood()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(X * PixelSize + 1, Y * PixelSize + 1);
            Console.Write(TexturePixel);
        }

        public void Clear()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }
    }
}
