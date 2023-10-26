namespace Minesweeper.Game {
    public class Position {
        public int x;
        public int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static Position operator +(Position a, Position b) {
            return new Position(a.x + b.x, a.y + b.y);
        }

        public static Position operator -(Position a, Position b) {
            return new Position(a.x - b.x, a.y - b.y);
        }

        public override bool Equals(object obj) {
            return obj is Position position && Equals(position);
        }

        public bool Equals(Position position) {
            return x == position.x && y == position.y;
        }

        public override int GetHashCode() {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}
