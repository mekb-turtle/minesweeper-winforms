namespace Minesweeper.Game {
    public class Tile {
        public enum TileState {
            Unstepped,
            Stepped,
            Flag,
            Question
        }

        public bool HasMine;
        public TileState State;

        public readonly Position Position;

        public int NeighborMines;

        public Tile(Position Position) {
            HasMine = false;
            State = TileState.Unstepped;
            this.Position = Position;
            NeighborMines = 0;
        }
    }
}
