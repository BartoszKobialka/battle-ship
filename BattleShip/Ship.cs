using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip {
    public class Ship {
        public Point coords;
        public int length;
        public Direction direction;

        public Ship(int length) {
            this.length = length;
        }

        public Point getEndPoint() {
            Point point = new Point(this.coords.x, this.coords.y);
            
            if (direction == Direction.Horizontal)
                point.x += length;
            else if (direction == Direction.Vertical)
                point.y += length;

            return point;
        }
    }
}
