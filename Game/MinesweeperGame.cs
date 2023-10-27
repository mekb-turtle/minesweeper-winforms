using System;
using static Minesweeper.Game.Tile;

namespace Minesweeper.Game {
    public class MinesweeperGame {
        public MinesweeperGame() {
            NewGame();
        }

        readonly static private Position[] neighbors = {
            new Position(-1, -1),
            new Position(0, -1),
            new Position(1, -1),
            new Position(-1, 0),
            new Position(1, 0),
            new Position(-1, 1),
            new Position(0, 1),
            new Position(1, 1)
        };

        // size
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Mines { get; private set; }
        public int Flags { get; private set; }

        // tiles
        private Tile[,] tiles;

        public bool CanMove {
            get {
                return !Lose && !Win;
            }
        }

        public bool TimerEnabled {
            get {
                return CanMove && Started;
            }
        }

        public bool Lose { get; private set; }
        public bool Win { get; private set; }

        public void NewGame() {
            // reset game over
            Lose = false;
            Win = false;
            Started = false;

            // reset flag count
            Flags = Mines;

            // reset tiles
            tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    tiles[x, y] = new Tile(new Position(x, y));
        }

        public bool Started { get; private set; }

        public void NewGame(int width, int height, int mines) {
            if (width < 1 || height < 1) throw new Exception("Invalid size");
            if (mines < 1 || mines >= width * height) throw new Exception("Invalid number of mines");
            Width = width;
            Height = height;
            Mines = mines;
            NewGame();
        }

        public bool InBounds(Position position) {
            if (position.x < 0 || position.x >= Width) return false;
            if (position.y < 0 || position.y >= Height) return false;
            return true;
        }

        public Tile GetTile(Position position) {
            if (!InBounds(position)) throw new IndexOutOfRangeException("Out of bounds");
            return tiles[position.x, position.y];
        }

        private bool DoStep(Position position) {
            Tile tile = GetTile(position);

            // flagged or already stepped
            if (tile.State == TileState.Stepped || tile.State == TileState.Flag) return false;

            // stepped on mine
            if (tile.HasMine) Lose = true;

            // step on tile
            tile.State = TileState.Stepped;

            if (tile.NeighborMines == 0) {
                // floodfill
                foreach (Position offset in neighbors) {
                    Position neighborPosition = position + offset;
                    if (InBounds(neighborPosition)) {
                        Step(neighborPosition);
                    }
                }
            }

            return true;
        }

        private void PlaceMines(Position initialPosition) {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            // set random mines
            for (int minesLeft = Mines; minesLeft > 0;) {
                Position minePosition = new Position(random.Next(Width), random.Next(Height));

                // try another tile if the tile is the initial step
                if (minePosition.Equals(initialPosition))
                    continue;

                // try another tile if the tile already has a mine
                if (GetTile(minePosition).HasMine)
                    continue;

                // put a mine on the tile
                --minesLeft;
                GetTile(minePosition).HasMine = true;
            }

            // set neighbor mines
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++) {
                    Position tilePosition = new Position(x, y);
                    int count = 0;

                    foreach (Position offset in neighbors) {
                        Position neighborPosition = tilePosition + offset;

                        // increment count if neighbor is in bounds and has a mine on the tile
                        if (InBounds(neighborPosition))
                            if (GetTile(neighborPosition).HasMine) count++;
                    }

                    GetTile(tilePosition).NeighborMines = count;
                }

            startTime = CurrentTime;

            Started = true;
        }

        public bool Step(Position position) {
            if (!CanMove) return false;

            if (!Started) {
                PlaceMines(position);
            }

            if (DoStep(position)) {
                Win = CheckWin();

                if (!CanMove) finishTime = CurrentTime;

                return true;
            }

            return false;
        }

        private long startTime;
        private long finishTime;

        private long CurrentTime {
            get {
                return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        }

        public long Timer {
            get {
                if (!Started) return 0;
                if (!CanMove) return finishTime - startTime;
                return CurrentTime - startTime;
            }
        }

        private bool CheckWin() {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++) {
                    Tile tile = GetTile(new Position(x, y));

                    // stepped on mine
                    if (tile.HasMine && tile.State == TileState.Stepped) return false;

                    // hasn't stepped on safe tile
                    if (!tile.HasMine && tile.State != TileState.Stepped) return false;
                }

            return true;
        }

        public bool Flag(Position position, Tile.TileState flag) {
            if (flag == TileState.Stepped) throw new Exception("Invalid flag");

            if (!CanMove) return false;

            Tile tile = GetTile(position);

            // already stepped
            if (tile.State == TileState.Stepped) return false;

            // nothing changed
            if (flag == tile.State) return false;

            // update flag count
            if (tile.State != TileState.Flag && flag == TileState.Flag) Flags--;
            else if (tile.State == TileState.Flag && flag != TileState.Flag) Flags++;

            // update tile flag
            tile.State = flag;
            return true;
        }
    }
}
