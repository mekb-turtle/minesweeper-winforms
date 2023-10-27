using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Minesweeper.Rendering {
    internal static class DrawingUtils {
        public static void DrawBorder(this Graphics graphics, RectangleF bounds, int size, Brush topLeft = null, Brush bottomRight = null) {
            if (topLeft == null) topLeft = Brushes.Gray;
            if (bottomRight == null) bottomRight = Brushes.White;

            // top
            graphics.FillRectangle(topLeft, new RectangleF(
                bounds.X, bounds.Y,
                bounds.Width - size, size));

            // left
            graphics.FillRectangle(topLeft, new RectangleF(
                bounds.X, bounds.Y,
                size, bounds.Height - size));

            // bottom
            graphics.FillRectangle(bottomRight, new RectangleF(
                bounds.X + size, bounds.Bottom - size,
                bounds.Width - size, size));

            // right
            graphics.FillRectangle(bottomRight, new RectangleF(
                bounds.Right - size, bounds.Y + size,
                size, bounds.Height - size));

            // corners
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y) {
                    Brush brush;
                    if (x < size - y - 1) {
                        brush = topLeft;
                    } else if (x > size - y - 1) {
                        brush = bottomRight;
                    } else continue;

                    graphics.FillRectangle(brush, new RectangleF(
                    bounds.X + x, bounds.Y + bounds.Height + y - size, 1, 1));
                    graphics.FillRectangle(brush, new RectangleF(
                        bounds.X + bounds.Width + x - size, bounds.Y + y, 1, 1));
                }
        }

        public static void DrawBorderOutside(this Graphics graphics, RectangleF bounds, int size, Brush topLeft = null, Brush bottomRight = null) {
            bounds.X -= size;
            bounds.Y -= size;
            bounds.Width += size * 2;
            bounds.Height += size * 2;
            DrawBorder(graphics, bounds, size, topLeft, bottomRight);
        }

        public static void RenderDigits(this Graphics graphics, int number, int digits, SpriteSheet spriteSheet, PointF offset) {
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

                graphics.DrawImage(spriteSheet.GetSprite(0, sprite), new RectangleF(
                    i * spriteSheet.GridSize.Width + offset.X, offset.Y,
                    spriteSheet.GridSize.Width, spriteSheet.GridSize.Height));
            }
        }

        public class Transform {
            public float Scale;
            public RectangleF Rectangle;

            public Transform(float Scale, float X, float Y, float Width, float Height) {
                this.Scale = Scale;
                Rectangle = new RectangleF(X, Y, Width, Height);
            }

            public Transform(float Scale, Rectangle Rectangle) {
                this.Scale = Scale;
                this.Rectangle = Rectangle;
            }
        }

        public static Transform GetFitMode(SizeF imageSize, SizeF windowSize) {
            float scale;

            if (imageSize.Width == 0 || imageSize.Height == 0 || windowSize.Width == 0 || windowSize.Height == 0) return new Transform(0, 0, 0, 0, 0);

            if (imageSize.Width / imageSize.Height > windowSize.Width / windowSize.Height) {
                scale = windowSize.Width / imageSize.Width;
                return new Transform(scale,
                    0, (windowSize.Height * 0.5F) - (imageSize.Height * scale * 0.5F),
                    windowSize.Width, (int)(imageSize.Height * scale));
            } else {
                scale = windowSize.Height / imageSize.Height;
                return new Transform(scale,
                    (windowSize.Width * 0.5F) - (imageSize.Width * scale * 0.5F), 0,
                    (int)(imageSize.Width * scale), windowSize.Height);
            }
        }
    }
}
