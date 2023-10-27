using System;
using System.Linq;
using System.Collections.Generic;
using static Minesweeper.Game.Tile;
using System.Windows.Forms;

namespace Minesweeper.Game {
    public class MinesweeperGame {
        public MinesweeperGame() {
            NewGame();
            Solver = new Solver(this);
        }

        readonly public Solver Solver;

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

        public List<Position> GetNeighbors(Tile tile) {
            return GetNeighbors(tile.Position);
        }

        public List<Position> GetNeighbors(Position position) {
            List<Position> list = new List<Position>();
            foreach (Position offset in neighbors) {
                Position neighbor = position + offset;
                if (InBounds(neighbor)) list.Add(neighbor);
            }
            return list;
        }

        // size
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Mines { get; private set; }
        public int Flags { get; private set; }

        // tiles
        private Tile[,] tiles;

        public List<Tile> Tiles {
            get {
                List<Tile> tileList = new List<Tile>();
                foreach (Tile tile in tiles) tileList.Add(tile);
                return tileList;
            }
        }

        // force the first move to have no neighbor mines
        public bool FirstMoveClear { get; set; } = false;

        // prevent having to guess
        public bool LogicMode { get; set; } = false;

        public bool CanMove => !Lose && !Win;

        public bool TimerEnabled => CanMove && Started;

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

            ClearBoard();
        }

        private void ClearBoard() {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    tiles[x, y] = new Tile(new Position(x, y));
        }

        public bool Started { get; private set; }

        private int InRange(int number, int min, int max) {
            if (min > max) return min;
            if (number < min) return min;
            if (number > max) return max;
            return number;
        }

        public void NewGame(int width, int height, int mines) {
            Width = InRange(width, 9, 60);
            Height = InRange(height, 9, 32);
            Mines = InRange(mines, 10, (int)(width * height * 0.45f));

            NewGame();
        }

        public bool InBounds(Position position) {
            if (position.X < 0 || position.X >= Width) return false;
            if (position.Y < 0 || position.Y >= Height) return false;
            return true;
        }

        public Tile GetTile(Position position) {
            if (!InBounds(position)) throw new IndexOutOfRangeException("Out of bounds");
            return tiles[position.X, position.Y];
        }

        private bool DoStep(Position position, bool force) {
            Tile tile = GetTile(position);

            // flagged or already stepped
            if (tile.Stepped || (!force && tile.State == TileState.Flag)) return false;

            // stepped on mine
            if (tile.HasMine) Lose = true;

            // step on tile
            tile.State = TileState.Stepped;

            if (tile.NeighborMines == 0) {
                // floodfill
                GetNeighbors(position).ForEach(pos => Step(pos));
            }

            return true;
        }

        private void PlaceMines(Position initialPosition) {
            Started = true;

            Random random = new Random(Guid.NewGuid().GetHashCode());

            int guessesLeft = 100000 * Mines / (Width * Height);

            while (true) {
                // set random mines
                for (int minesLeft = Mines; minesLeft > 0;) {
                    Position minePosition = new Position(random.Next(Width), random.Next(Height));

                    // try another tile if the tile is the initial step
                    if (minePosition.Equals(initialPosition))
                        continue;

                    // don't put a mine in a 3x3 area of the initial step
                    if (FirstMoveClear) {
                        if (
                            minePosition.X >= initialPosition.X - 1 &&
                            minePosition.Y >= initialPosition.Y - 1 &&
                            minePosition.X <= initialPosition.X + 1 &&
                            minePosition.Y <= initialPosition.Y + 1) {
                            continue;
                        }
                    }

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

                        // count how many neighbors have a mine
                        GetTile(tilePosition).NeighborMines = GetNeighbors(tilePosition).Count(neighborPosition => GetTile(neighborPosition).HasMine);
                    }

                if (LogicMode) {
                    // determine if the game is solvable without guessing
                    Step(initialPosition, true);
                    Solver.Solve();

                    // reset flag count
                    Flags = Mines;

                    // guesses are required if the solver did not win the game, so generate a new board and try again
                    if (!Win) {
                        ClearBoard();
                        guessesLeft--;

                        if (guessesLeft <= 0) {
                            // give up
                            NewGame();
                            throw new Exception("Failed to find game without guesses");
                        }

                        continue;
                    }

                    // otherwise, the board is solvable without guessing

                    // reset win state
                    Win = false;

                    // clear the tiles
                    for (int x = 0; x < Width; x++)
                        for (int y = 0; y < Height; y++) {
                            tiles[x, y].State = TileState.Unstepped;
                        }

                    break;
                } else break;
            }

            startTime = CurrentTime;

            return;
        }

        public bool Step(Tile tile, bool force = false) {
            return Step(tile.Position, force);
        }

        public bool Step(Position position, bool force = false) {
            if (!CanMove) return false;

            if (!Started) {
                PlaceMines(position);
            }

            if (DoStep(position, force)) {
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
                    if (tile.HasMine && tile.Stepped) return false;

                    // hasn't stepped on safe tile
                    if (!tile.HasMine && !tile.Stepped) return false;
                }

            return true;
        }

        public bool Flag(Tile tile, TileState flag) {
            return Flag(tile.Position, flag);
        }

        public bool Flag(Position position, TileState flag) {
            if (flag == TileState.Stepped) throw new Exception("Invalid flag");

            if (!CanMove || !Started) return false;

            Tile tile = GetTile(position);

            // already stepped
            if (tile.Stepped) return false;

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
