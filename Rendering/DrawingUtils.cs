using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper.Rendering {
    internal class DrawingUtils {
        private DrawingUtils() {

        }

        public static void DrawBorder(Graphics graphics, Rectangle bounds, int size, Brush topLeft = null, Brush bottomRight = null) {
            if (topLeft == null) topLeft = Brushes.Gray;
            if (bottomRight == null) bottomRight = Brushes.White;

            graphics.FillRectangle(topLeft, new Rectangle(
                bounds.X, bounds.Y,
                bounds.Width - size, size));
            graphics.FillRectangle(topLeft, new Rectangle(
                bounds.X, bounds.Y,
                size, bounds.Height - size));

            graphics.FillRectangle(bottomRight, new Rectangle(
                bounds.X + size, bounds.Y + bounds.Height - size,
                bounds.Width - size, size));
            graphics.FillRectangle(bottomRight, new Rectangle(
                bounds.X + bounds.Width - size, bounds.Y + size,
                size, bounds.Height - size));

            // corners
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y) {
                    Brush brush;
                    if (x < size - y - 1) {
                        brush = Brushes.Gray;
                    } else if (x > size - y - 1) {
                        brush = bottomRight;
                    } else continue;

                    graphics.FillRectangle(brush, new Rectangle(
                        bounds.X + x, bounds.Y + bounds.Height + y - size, 1, 1));
                    graphics.FillRectangle(brush, new Rectangle(
                        bounds.X + bounds.Width + x - size, bounds.Y + y, 1, 1));
                }
        }

        public static void DrawBorderOutside(Graphics graphics, Rectangle bounds, int size, Brush topLeft = null, Brush bottomRight = null) {
            bounds.X -= size;
            bounds.Y -= size;
            bounds.Width += size * 2;
            bounds.Height += size * 2;
            DrawBorder(graphics, bounds, size, topLeft, bottomRight);
        }

        public static void RenderDigits(int number, int digits, Graphics graphics, SpriteSheet spriteSheet, Point offset) {
            bool negative = number < 0;
            if (negative) number = -number;

            // pad number with zeroes
            List<char> str = number.ToString().PadLeft(digits, '0').ToList();

            // remove leading zeroes
            while (str.Count > digits) str.RemoveAt(0);

            // set first digit to negative sign if number is negative
            if (negative) str[0] = '-';

            for (int i = 0; i < str.Count; i++) {
                char digit = str[i];
                int sprite = 1;
                if (digit >= '0' && digit <= '9')
                    sprite = 11 - (digit - '0');
                else if (digit == '-') sprite = 0;

                graphics.DrawImage(spriteSheet.getSprite(0, sprite), new Rectangle(
                    i * spriteSheet.GridSize.Width + offset.X, offset.Y,
                    spriteSheet.GridSize.Width, spriteSheet.GridSize.Height));
            }
        }
    }
}
