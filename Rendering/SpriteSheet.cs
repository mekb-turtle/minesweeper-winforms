using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper.Rendering {
    public class SpriteSheet {
        readonly public List<Bitmap> SpriteSheets;

        public Bitmap SelectedSpriteSheet {
            get {
                return SpriteSheets[SelectedIndex];
            }
        }

        public int SelectedIndex = 0;

        public Size GridSize {
            get {
                return new Size(gridSize.Width, gridSize.Height);
            }
            set {
                if (value.Width < 1 || value.Height < 1 || value.Width > SelectedSpriteSheet.Width || value.Height > SelectedSpriteSheet.Height) {
                    throw new Exception("Invalid grid size");
                }

                gridSize.Width = value.Width;
                gridSize.Height = value.Height;
            }
        }

        private Size gridSize;

        public SpriteSheet(Size gridSize, List<Bitmap> spriteSheets) {
            SpriteSheets = spriteSheets;

            SelectedIndex = 0;

            GridSize = gridSize;
        }

        public SpriteSheet(Size gridSize, params Bitmap[] spriteSheets) : this(gridSize, spriteSheets.ToList()) {
        }

        public Bitmap GetSprite(int x, int y) {
            Rectangle spriteRect = new Rectangle(
                x * gridSize.Width, y * gridSize.Height,
                gridSize.Width, gridSize.Height);

            if (spriteRect.X < 0 ||
                spriteRect.Y < 0 ||
                spriteRect.X + spriteRect.Width > SelectedSpriteSheet.Width ||
                spriteRect.Y + spriteRect.Height > SelectedSpriteSheet.Height) {
                throw new Exception("Invalid sprite position");
            }

            return SelectedSpriteSheet.Clone(spriteRect, SelectedSpriteSheet.PixelFormat);
        }
    }
}
