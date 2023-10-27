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

            StartBeginnerGame();

            // twice for when the toolbar overlaps and changes the size
            ScaleWindow1();
            ScaleWindow1();

            UpdateFullscreen();
        }

        private void StartNewGame(object sender, EventArgs e) {
            Game.NewGame();
        }

        private void UpdateWindowScaleButtons() {
            scale1Button.Checked = Size.Equals(GetWindowSize(1));
            scale2Button.Checked = Size.Equals(GetWindowSize(2));
            scale3Button.Checked = Size.Equals(GetWindowSize(3));
            scale4Button.Checked = Size.Equals(GetWindowSize(4));
        }

        private void UncheckNewGameButtons() {
            beginnerButton.Checked = false;
            intermediateButton.Checked = false;
            expertButton.Checked = false;
            customButton.Checked = false;
        }

        private void StartBeginnerGame(object sender = null, EventArgs e = null) {
            Game.NewGame(9, 9, 10);

            UncheckNewGameButtons();
            beginnerButton.Checked = true;

            ResizeGame();
        }

        private void StartIntermediateGame(object sender = null, EventArgs e = null) {
            Game.NewGame(16, 16, 40);

            UncheckNewGameButtons();
            intermediateButton.Checked = true;

            ResizeGame();
        }

        private void StartExpertGame(object sender = null, EventArgs e = null) {
            Game.NewGame(30, 16, 99);

            UncheckNewGameButtons();
            expertButton.Checked = true;

            ResizeGame();
        }

        private void StartCustomGame(object sender = null, EventArgs e = null) {
            customFieldDialog.Width = Game.Width;
            customFieldDialog.Height = Game.Height;
            customFieldDialog.Mines = Game.Mines;

            if (customFieldDialog.ShowDialog() != DialogResult.OK) return;

            Game.NewGame(customFieldDialog.Width, customFieldDialog.Height, customFieldDialog.Mines);

            UncheckNewGameButtons();
            customButton.Checked = true;

            ResizeGame();
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
            MinimumSize = GetWindowSize(1);

            panel.Invalidate();
        }

        private Size GetWindowSize(float scale) {
            return new Size((int)(Width - panel.Width + (scale * gamePanel.Width)), (int)(Height - panel.Height + (scale * gamePanel.Height)));
        }

        private Rectangle
            gamePanel = new Rectangle(),
            infoPanel = new Rectangle(),
            tileBoard = new Rectangle(),
            flagsDisplay = new Rectangle(),
            timerDisplay = new Rectangle(),
            face = new Rectangle();

        private void RenderGame(Graphics g) {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            // background
            g.FillRectangle(Brushes.Silver, gamePanel);

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
            g.RenderDigits((int)(Game.Timer % 999L), 3, digitSpriteSheet, timerDisplay.Location);

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

        private Bitmap panelImage = null;

        private void PanelPaint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(panel.BackColor);

            Transform transform = GetFitMode(gamePanel.Size, panel.Size);

            if (panelImage != null && !panelImage.Size.Equals(gamePanel.Size)) {
                panelImage.Dispose();
                panelImage = null;
            }

            if (panelImage == null) {
                panelImage = new Bitmap(gamePanel.Width, gamePanel.Height);
            }

            using (Graphics g = Graphics.FromImage(panelImage)) {
                g.Clear(Color.Transparent);
                RenderGame(g);
            }

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(panelImage, transform.Rectangle);
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
                    (int)((FormCursorLocation.X - transform.Rectangle.X) / transform.Scale),
                    (int)((FormCursorLocation.Y - transform.Rectangle.Y) / transform.Scale));
            }
        }

        private bool FaceMouseDown => FormFaceMouseDown && face.Contains(CursorLocation);

        private bool TileMouseDown => FormTileMouseDown && tileBoard.Contains(CursorLocation) && Game.CanMove;

        private Position GetHoveredTilePosition() => new Position(
                (CursorLocation.X - tileBoard.X) / tileSpriteSheet.GridSize.Width,
                (CursorLocation.Y - tileBoard.Y) / tileSpriteSheet.GridSize.Height);

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
                    Game.Flag(tile, TileState.Flag);
                    break;
                case TileState.Flag:
                    Game.Flag(tile, marksButton.Checked ? TileState.Question : TileState.Unstepped);
                    break;
                case TileState.Question:
                    Game.Flag(tile, TileState.Unstepped);
                    break;
            }
        }

        private void StepTile() {
            if (!Game.CanMove) return;

            Tile tile = GetHoveredTile();
            if (tile == null) return;

            bool started = Game.Started;

            bool result;
            try {
                result = Game.Step(tile);
            } catch (Exception err) {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Error.WriteLine(err);
                return;
            }

            if (result) {
                if (autoSolveButton.Checked) {
                    Game.Solver.Solve();
                }
            }

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

        private void SaveImage(object sender, EventArgs e) {
            using (Bitmap image = new Bitmap(gamePanel.Width, gamePanel.Height)) {
                using (Graphics g = Graphics.FromImage(image)) {
                    RenderGame(g);
                }

                // copy to clipboard
                Clipboard.SetImage(image);

                string filename = "minesweeper";
                if (Game.Win) filename += "_" + GetTime(Game.Timer);
                filename += ".png";

                // open save dialog
                using (SaveFileDialog dialog = new SaveFileDialog {
                    AddExtension = true,
                    Filter = "PNG Image|*.png|All Files|*",
                    Title = "Save Screenshot",
                    FileName = filename
                }) {
                    if (dialog.ShowDialog() == DialogResult.OK) {
                        image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }

        private void ScaleWindow1(object sender = null, EventArgs e = null) {
            Size = GetWindowSize(1);
        }

        private void ScaleWindow2(object sender = null, EventArgs e = null) {
            Size = GetWindowSize(2);
        }

        private void ScaleWindow3(object sender = null, EventArgs e = null) {
            Size = GetWindowSize(3);
        }

        private void ScaleWindow4(object sender = null, EventArgs e = null) {
            Size = GetWindowSize(4);
        }

        private void MakeOneMove(object sender, EventArgs e) {
            Game.Solver.SolveOnce();
        }

        private void MakeMultipleMoves(object sender, EventArgs e) {
            Game.Solver.Solve();
        }

        private void ToggleFirstMoveClear(object sender, EventArgs e) {
            Game.FirstMoveClear = !Game.FirstMoveClear;
            firstMoveClearButton.Checked = Game.FirstMoveClear;
        }

        private void ToggleLogic(object sender, EventArgs e) {
            Game.LogicMode = !Game.LogicMode;
            logicButton.Checked = Game.LogicMode;
        }

        private void ToggleAutoSolve(object sender, EventArgs e) {
            autoSolveButton.Checked = !autoSolveButton.Checked;
        }

        private bool Fullscreen = false;

        private void ToggleFullscreen(object sender, EventArgs e) {
            Fullscreen = !Fullscreen;
            UpdateFullscreen();
        }

        private void UpdateFullscreen() {
            fullscreenButton.Checked = Fullscreen;
            if (Fullscreen) {
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.None;
            } else {
                FormBorderStyle = FormBorderStyle.Sizable;
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

        private void FormSizeChanged(object sender, EventArgs e) {
            if (WindowState != FormWindowState.Maximized) Fullscreen = false;
            UpdateFullscreen();
            UpdateWindowScaleButtons();
            MinimumSize = GetWindowSize(1);
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
