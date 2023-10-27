using System.Linq;
using System.Collections.Generic;
using System;
using static Minesweeper.Game.Solver.TileScore;

namespace Minesweeper.Game {
    public class Solver {
        private MinesweeperGame game;

        public Solver(MinesweeperGame game) {
            this.game = game;
        }

        // makes it easier to modify in an array, otherwise we use pointers
        public class TileScore {
            public TileScore() { }

            public enum Score {
                Unset,
                Safe,
                Unsure,
                Mine
            }

            public Score score = Score.Unset;

            public bool Unset => score == Score.Unset;
            public bool Safe => score == Score.Safe;
            public bool Unsure => score == Score.Unsure;
            public bool Mine => score == Score.Mine;

            public void Set(Score score) => this.score = score;
        }

        private TileScore[,] tileScores = null;

        private void NewBoard() {
            tileScores = new TileScore[game.Width, game.Height];
            for (int x = 0; x < game.Width; x++)
                for (int y = 0; y < game.Height; y++)
                    tileScores[x, y] = new TileScore();
        }

        private TileScore GetScore(Tile tile) {
            return GetScore(tile.Position);
        }

        private TileScore GetScore(Position position) {
            return tileScores[position.X, position.Y];
        }

        public void Solve() {
            while (SolveOnce()) ;
        }

        private List<Tile> getUnsteppedNeighbors(Position position) =>
            game.GetNeighbors(position)
            .Select(pos => game.GetTile(pos)).ToList()
            .FindAll(neighborTile => !neighborTile.Stepped);

        public bool SolveOnce() {
            if (!game.CanMove) return false;

            NewBoard();

            bool stepped = false;

            int mines = 0;

            foreach (Tile tile in game.Tiles) {
                if (tile.Stepped && tile.NeighborMines > 0) {
                    List<Tile> neighbors = getUnsteppedNeighbors(tile.Position);

                    // if the number on the tile == the number of neighbors that haven't been stepped on, all unstepped neighbors are mines
                    if (neighbors.Count == tile.NeighborMines) {
                        neighbors.ForEach(neighbor => {
                            TileScore score = GetScore(neighbor);

                            if (!score.Mine)
                                mines++;

                            score.Set(Score.Mine);

                            game.Flag(neighbor, Tile.TileState.Flag);
                        });
                    } else {
                        // flag neighbors as question
                        neighbors.ForEach(neighbor => {
                            TileScore score = GetScore(neighbor);

                            if (score.Mine) return;

                            score.Set(Score.Unsure);

                            game.Flag(neighbor, Tile.TileState.Question);
                        });
                    }
                }
            }

            foreach (Tile tile in game.Tiles) {
                if (tile.Stepped && tile.NeighborMines > 0) {
                    List<Tile> neighbors = getUnsteppedNeighbors(tile.Position);

                    // if the number on the tile == number of surrounding flagged mines, all other unstepped neighbors are safe
                    if (tile.NeighborMines == neighbors.Count(neighbor => GetScore(neighbor).Mine)) {
                        neighbors.ForEach(neighbor => {
                            if (GetScore(neighbor).Mine) return;

                            stepped = true;
                            tileScores[neighbor.Position.X, neighbor.Position.Y].Set(Score.Safe);

                            game.Step(neighbor, true);
                        });
                    }
                }
            }

            if (mines == game.Mines) {
                // if the number of flagged mines == the total number of mines in the game, all other unstepped tiles are safe
                foreach (Tile tile in game.Tiles) {
                    if (tile.Stepped || GetScore(tile).Mine) continue;

                    stepped = true;
                    tileScores[tile.Position.X, tile.Position.Y].Set(Score.Safe);

                    game.Step(tile, true);
                }
            }

            tileScores = null;

            return stepped;
        }
    }
}
