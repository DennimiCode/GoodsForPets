using System;
using System.Drawing;

namespace GoodsForPets.Helpers
{
    public class Captcha
    {
        private Random _random;
        private const int Length = 4;
        private int _width = 150;
        private int _heigth = 50;
        public static string Text { get; private set; }
        public Bitmap Create()
        {
            _random = new Random();
            Bitmap image = new Bitmap(_width, _heigth);
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Color.Black, 2);
            Graphics graphics = Graphics.FromImage(image);
            Text = string.Empty;

            for (int i = 0; i < Length; i++)
            {
                string symbol = GetChar().ToString();
                Text += symbol;
                graphics.DrawString(symbol, new Font("Comic Sans MS", 25), brush,
                    new Point(i * 20, _random.Next(0, 11)));
            }

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _heigth; j++)
                {
                    if (_random.Next(0, 100) > 50)
                        image.SetPixel(i, j, Color.FromArgb(_random.Next(0, 256), _random.Next(0, 256), _random.Next(0, 256)));
                }
            }

            for (int i = 0; i < Length; i++)
            {
                graphics.DrawLine(pen, new Point(_random.Next(0, _heigth), _random.Next(0, _heigth)),
                    new Point(_random.Next(_heigth / 2, _width), _random.Next(_heigth / 2, _width)));
            }

            return image;
        }

        public char GetChar()
        {
            for (int i = 0; i < Length; i++)
            {
                int randomSymbol = _random.Next(1, 4);

                if (randomSymbol == 1)
                    return (char)_random.Next('A', 'Z' + 1);
                else if (randomSymbol == 2)
                    return (char)_random.Next('a', 'z' + 1);
                else if (randomSymbol == 3)
                    return (char)_random.Next('0', '9' + 1);
            }
            return '0';
        }
    }
}
