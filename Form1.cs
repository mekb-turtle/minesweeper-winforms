using Minesweeper.Game;
using Minesweeper.Rendering;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Windows.Forms;
using static Minesweeper.Game.Tile;
using static Minesweeper.Rendering.DrawingUtils;

namespace Minesweeper {
    public partial class Form1 : Form {
        private MinesweeperGame Game;

        public Form1() {
            InitializeComponent();
        }

        private SpriteSheet faceSpriteSheet, digitSpriteSheet, tileSpriteSheet;

        private void Form1_Load(object sender, EventArgs e) {
            faceSpriteSheet = new SpriteSheet(new Size(24, 24), Properties.Resources.faceColor, Properties.Resources.faceGray);
            digitSpriteSheet = new SpriteSheet(new Size(13, 23), Properties.Resources.digitColor, Properties.Resources.digitGray);
            tileSpriteSheet = new SpriteSheet(new Size(16, 16), Properties.Resources.tileColor, Properties.Resources.tileGray);

            marksButton.Checked = true;
            colorButton.Checked = true;
            soundButton.Checked = false;

            UpdateSpriteSheets();

            Game = new MinesweeperGame();
            StartBeginnerGame(null, null);
        }

        private void StartNewGame(object sender, EventArgs e) {
            Game.NewGame();
        }

        private void UncheckNewGameButtons() {
            beginnerButton.Checked = false;
            intermediateButton.Checked = false;
            expertButton.Checked = false;
            customButton.Checked = false;
        }

        private void StartBeginnerGame(object sender, EventArgs e) {
            Game.NewGame(9, 9, 10);

            UncheckNewGameButtons();
            beginnerButton.Checked = true;

            ResizeGame();
        }

        private void StartIntermediateGame(object sender, EventArgs e) {
            Game.NewGame(16, 16, 40);

            UncheckNewGameButtons();
            intermediateButton.Checked = true;

            ResizeGame();
        }

        private void StartExpertGame(object sender, EventArgs e) {
            Game.NewGame(30, 16, 99);

            UncheckNewGameButtons();
            expertButton.Checked = true;

            ResizeGame();
        }

        private void StartCustomGame(object sender, EventArgs e) {
            customFieldDialog.Width = Game.Width;
            customFieldDialog.Height = Game.Height;
            customFieldDialog.Mines = Game.Mines;

            if (customFieldDialog.ShowDialog() != DialogResult.OK) return;

            int width = InRange(customFieldDialog.Width, 9, 30);
            int height = InRange(customFieldDialog.Width, 9, 24);
            int mines = InRange(customFieldDialog.Mines, 10, (width - 1) * (height - 1));

            Game.NewGame(width, height, mines);

            UncheckNewGameButtons();
            customButton.Checked = true;

            ResizeGame();
        }

        private int InRange(int number, int min, int max) {
            if (number < min) return min;
            if (number > max) return max;
            return number;
        }

        private readonly CustomFieldDialog customFieldDialog = new CustomFieldDialog();

        private void ResizeGame() {
            // set info panel rect
            infoPanel.X = 9;
            infoPanel.Y = 9;
            infoPanel.Width = Game.Width * tileSpriteSheet.GridSize.Width + 6;
            infoPanel.Height = 14 + digitSpriteSheet.GridSize.Height;

            // set tile board rect
            tileBoard.X = 12;
            tileBoard.Y = infoPanel.Y + infoPanel.Height + 9;
            tileBoard.Width = Game.Width * tileSpriteSheet.GridSize.Width;
            tileBoard.Height = Game.Height * tileSpriteSheet.GridSize.Height;

            // set info panel elements size
            flagsDisplay.Width = timerDisplay.Width = digitSpriteSheet.GridSize.Width * 3;
            flagsDisplay.Height = timerDisplay.Height = digitSpriteSheet.GridSize.Height;
            face.Width = faceSpriteSheet.GridSize.Width;
            face.Height = faceSpriteSheet.GridSize.Height;

            // set info panel elements position
            flagsDisplay.X = 8 + infoPanel.X;
            face.X = (infoPanel.Width - face.Width) / 2 + infoPanel.X;
            timerDisplay.X = infoPanel.Width - timerDisplay.Width - 10 + infoPanel.X;
            flagsDisplay.Y = (infoPanel.Height - flagsDisplay.Height) / 2 + infoPanel.Y;
            timerDisplay.Y = (infoPanel.Height - timerDisplay.Height) / 2 + infoPanel.Y;
            face.Y = (infoPanel.Height - face.Height) / 2 + infoPanel.Y + 1;

            // set main panel size
            gamePanel.Width = tileBoard.Width + 20;
            gamePanel.Height = tileBoard.Height + 63;

            // set minimum window size
            MinimumSize = new Size(Width - panel.Width + gamePanel.Width, Height - panel.Height + gamePanel.Height);

            panel.Invalidate();
        }

        private Rectangle
            gamePanel = new Rectangle(),
            infoPanel = new Rectangle(),
            tileBoard = new Rectangle(),
            flagsDisplay = new Rectangle(),
            timerDisplay = new Rectangle(),
            face = new Rectangle();

        private void PanelPaint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(panel.BackColor);

            Transform transform = GetFitMode(gamePanel.Size, ClientSize);

            using (Bitmap bitmap = new Bitmap(gamePanel.Width, gamePanel.Height)) {
                using (Graphics g = Graphics.FromImage(bitmap)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;

                    // background
                    g.Clear(Color.Silver);

                    // borders
                    g.FillRectangle(Brushes.White, 0, 0, gamePanel.Width, 3);
                    g.FillRectangle(Brushes.White, 0, 0, 3, gamePanel.Height);
                    g.DrawBorder(infoPanel, 2);
                    g.DrawBorderOutside(tileBoard, 3);
                    g.DrawBorderOutside(flagsDisplay, 1);
                    g.DrawBorderOutside(timerDisplay, 1);
                    g.FillRectangle(Brushes.Gray, face);
                    g.DrawBorderOutside(face, 1, Brushes.Gray, Brushes.Gray);

                    // draw flags and timer
                    g.FillRectangle(Brushes.Black, flagsDisplay);
                    g.FillRectangle(Brushes.Black, timerDisplay);
                    g.RenderDigits(Game.Flags, 3, digitSpriteSheet, flagsDisplay.Location);
                    g.RenderDigits((int) (Game.Timer % 999L), 3, digitSpriteSheet, timerDisplay.Location);

                    int faceSprite = 4;

                    if (Game.Win) faceSprite = 1;
                    if (Game.Lose) faceSprite = 2;
                    if (TileMouseDown) faceSprite = 3;
                    if (FaceMouseDown) faceSprite = 0;

                    // draw face
                    g.DrawImage(faceSpriteSheet.GetSprite(0, faceSprite), face);

                    // draw board
                    for (int x = 0; x < Game.Width; x++) {
                        for (int y = 0; y < Game.Height; y++) {
                            Tile tile = Game.GetTile(new Position(x, y));

                            g.DrawImage(
                                tileSpriteSheet.GetSprite(0, GetTileSprite(tile)),
                                new PointF(
                                    x * tileSpriteSheet.GridSize.Width + tileBoard.X,
                                    y * tileSpriteSheet.GridSize.Height + tileBoard.Y));
                        }
                    }
                }

                e.Graphics.DrawSharpUpscaledImage(bitmap, transform.Rectangle);

                if (Screenshot) {
                    Screenshot = false;

                    SaveImage(bitmap);
                }
            }
        }

        private int GetTileSprite(Tile tile) {
            if (Game.Lose) {
                switch (tile.State) {
                    case TileState.Unstepped:
                    case TileState.Question:
                        if (tile.HasMine) return 5; // mine
                        break;
                    case TileState.Stepped:
                        if (tile.HasMine) return 3; // mine with red background
                        break;
                    case TileState.Flag:
                        if (!tile.HasMine) return 4; // mine with cross on it
                        break;
                }
            } else if (TileMouseDown && GetHoveredTilePosition().Equals(tile.Position)) {
                switch (tile.State) {
                    case TileState.Unstepped:
                        return 15; // stepped (0)
                    case TileState.Question:
                        return 6; // stepped question
                }
            }
            switch (tile.State) {
                case TileState.Unstepped:
                    return 0; // unstepped
                case TileState.Stepped:
                    return 15 - tile.NeighborMines; // 15 = stepped (0), 14 = 1, 13 = 2, etc
                case TileState.Flag:
                    return 1; // flag
                case TileState.Question:
                    return 2; // question
            }
            return 0; // unstepped
        }

        private void TimerUpdateTick(object sender, EventArgs e) {
            if (!Game.TimerEnabled) return;

            panel.Invalidate();
        }

        private bool FormFaceMouseDown = false;
        private bool FormTileMouseDown = false;
        private Point FormCursorLocation = new Point();

        private Point CursorLocation {
            get {
                Transform transform = GetFitMode(gamePanel.Size, ClientSize);
                return new Point(
                    (int) ((FormCursorLocation.X - transform.Rectangle.X) / transform.Scale),
                    (int) ((FormCursorLocation.Y - transform.Rectangle.Y) / transform.Scale));
            }
        }

        private bool FaceMouseDown {
            get {
                return FormFaceMouseDown && face.Contains(CursorLocation);
            }
        }

        private bool TileMouseDown {
            get {
                return FormTileMouseDown && tileBoard.Contains(CursorLocation) && Game.CanMove;
            }
        }

        private Position GetHoveredTilePosition() {
            return new Position(
                (CursorLocation.X - tileBoard.X) / tileSpriteSheet.GridSize.Width,
                (CursorLocation.Y - tileBoard.Y) / tileSpriteSheet.GridSize.Height);
        }

        private Tile GetHoveredTile() {
            Position pos = GetHoveredTilePosition();

            if (Game.InBounds(pos)) return Game.GetTile(pos);

            return null;
        }

        private void FlagTile() {
            if (!Game.CanMove) return;

            Tile tile = GetHoveredTile();
            if (tile == null) return;

            switch (tile.State) {
                case TileState.Unstepped:
                    Game.Flag(tile.Position, TileState.Flag);
                    break;
                case TileState.Flag:
                    Game.Flag(tile.Position, marksButton.Checked ? TileState.Question : TileState.Unstepped);
                    break;
                case TileState.Question:
                    Game.Flag(tile.Position, TileState.Unstepped);
                    break;
            }
        }

        private void StepTile() {
            if (!Game.CanMove) return;

            Tile tile = GetHoveredTile();
            if (tile == null) return;

            bool started = Game.Started;
            bool result = Game.Step(tile.Position);

            if (result && soundButton.Checked) {
                Stream sound = null;

                if (!started) {
                    // play start sound
                    sound = Properties.Resources.start;
                } else if (Game.Win) {
                    // play win sound
                    sound = Properties.Resources.win;
                } else if (Game.Lose) {
                    // play win sound
                    sound = Properties.Resources.lose;
                }

                if (sound != null) {
                    SoundPlayer soundPlayer = new SoundPlayer(sound);
                    soundPlayer.Play();
                }
            }

            gameTimer.Enabled = Game.TimerEnabled;
        }

        private void GameMouseDown(object sender, MouseEventArgs e) {
            FormCursorLocation = e.Location;

            if (e.Button == MouseButtons.Left) {
                if (face.Contains(CursorLocation))
                    FormFaceMouseDown = true;
                else if (tileBoard.Contains(CursorLocation) && Game.CanMove)
                    FormTileMouseDown = true;
            } else if (e.Button == MouseButtons.Right && Game.CanMove) {
                FlagTile();
            }

            panel.Invalidate();
        }

        private void GameMouseUp(object sender, MouseEventArgs e) {
            FormCursorLocation = e.Location;

            if (e.Button == MouseButtons.Left) {
                if (FaceMouseDown)
                    Game.NewGame();
                else if (TileMouseDown)
                    StepTile();

                FormFaceMouseDown = false;
                FormTileMouseDown = false;
            }

            panel.Invalidate();
        }

        private void GameMouseMove(object sender, MouseEventArgs e) {
            FormCursorLocation = e.Location;

            panel.Invalidate();
        }

        private bool Screenshot = false;

        private void SaveImage(object sender, EventArgs e) {
            Screenshot = true;
            panel.Invalidate();
        }

        private void SaveImage(Image image) {
            // copy to clipboard
            Clipboard.SetImage(image);

            string filename = "minesweeper";
            if (Game.Win) filename += "_" + GetTime(Game.Timer);
            filename += ".png";

            // open save dialog
            SaveFileDialog dialog = new SaveFileDialog {
                AddExtension = true,
                Filter = "PNG Image|*.png|All Files|*",
                Title = "Save Screenshot",
                FileName = filename
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private string GetTime(long seconds) {
            long hours = seconds / 3600;
            int time = (int)(seconds % 3600);
            long minutes = time / 60;
            time %= 60;

            string str = "";
            if (hours > 0) str += hours + "h";
            if (minutes > 0) str += minutes + "m";
            str += time + "s";

            return str;
        }

        private void FormResize(object sender, EventArgs e) {
            panel.Invalidate();
        }

        private void ToggleMarks(object sender, EventArgs e) {
            marksButton.Checked = !marksButton.Checked;
        }

        private void ToggleColor(object sender, EventArgs e) {
            colorButton.Checked = !colorButton.Checked;
            UpdateSpriteSheets();
        }

        private void UpdateSpriteSheets() {
            digitSpriteSheet.SelectedIndex = colorButton.Checked ? 0 : 1;
            faceSpriteSheet.SelectedIndex = colorButton.Checked ? 0 : 1;
            tileSpriteSheet.SelectedIndex = colorButton.Checked ? 0 : 1;
        }

        private void ToggleSound(object sender, EventArgs e) {
            soundButton.Checked = !soundButton.Checked;
        }

        private void ExitButtonClick(object sender, EventArgs e) {
            Close();
        }
    }
}
