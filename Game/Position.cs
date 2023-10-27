namespace Minesweeper.Game {
    public class Position {
        public int X;
        public int Y;

        public Position(int X, int Y) {
            this.X = X;
            this.Y = Y;
        }

        public static Position operator +(Position a, Position b) {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b) {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public override bool Equals(object obj) {
            return obj is Position position && Equals(position);
        }

        public bool Equals(Position position) {
            return X == position.X && Y == position.Y;
        }

        public override int GetHashCode() {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString() {
            return "X: " + X + ", Y: " + Y;
        }
    }
}
