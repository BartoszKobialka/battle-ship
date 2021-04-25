using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip {
    public class Point {
        public int x, y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Point(Point other) {
            this.x = other.x;
            this.y = other.y;
        }

        public Point() {
            this.x = new Random().Next(GameBoard.MaxIndex);
            this.y = new Random().Next(GameBoard.MaxIndex);
        }

        public static bool operator ==(Point pointOne, Point pointTwo) {
            return pointOne.x == pointTwo.x && pointOne.y == pointTwo.y;
        }

        public static bool operator !=(Point pointOne, Point pointTwo) {
            return pointOne.x != pointTwo.x || pointOne.y != pointTwo.y;
        }
    }
}
