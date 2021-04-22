using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip {
    public class BoardField {
        public int x { get; }
        public int y { get; }
        public FieldType fieldType { get; set; }

        public BoardField(int x, int y) {
            this.x = x;
            this.y = y;
            this.fieldType = FieldType.Empty;
        }

    }
}
