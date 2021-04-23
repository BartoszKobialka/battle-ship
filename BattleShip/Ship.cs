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
            Point point = new Point(this.coords);
            
            if (this.direction == Direction.Horizontal)
                point.x += this.length;
            else if (this.direction == Direction.Vertical)
                point.y += this.length;

            return point;
        }
    }
}
