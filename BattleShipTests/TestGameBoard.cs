using System;
using Xunit;
using BattleShip;
using System.Collections.Generic;

namespace BattleShipTests {
    public class TestGameBoard {
        
        [Fact]
        public void TestFillBoard() {
            GameBoard gameBoard = new GameBoard();
            gameBoard.FillBoard();
            const int shipFieldAmount = 17;

            Assert.Equal(shipFieldAmount, gameBoard.CountFieldsByType(FieldType.Ship));
        }

        [Theory, MemberData(nameof(TestCheckCanBePlacedData))]
        public void TestCheckCanBePlaced(int x, int y, int shipLength, Direction direction, bool expected) {
            GameBoard gameBoard = new GameBoard();
            gameBoard.boardMatrix[0][3].fieldType = FieldType.Ship;
            gameBoard.boardMatrix[3][0].fieldType = FieldType.Ship;

            var ship = new Ship(shipLength);
            ship.coords = new Point(x, y);
            ship.direction = direction;

            Assert.Equal(expected, gameBoard.canBePlaced(ship));
        }

        public static IEnumerable<object[]> TestCheckCanBePlacedData {
            get {
                return new[]
                {
                     new object[] { 0, 0, 2, Direction.Horizontal, true },
                     new object[] { 0, 0, 3, Direction.Horizontal, false },
                     new object[] { 5, 8, 2, Direction.Vertical, true },
                     new object[] { 0, 0, 5, Direction.Horizontal, false },
                     new object[] { 0, 0, 5, Direction.Vertical, false },
                     new object[] { 0, 0, 3, Direction.Vertical, false },
                     new object[] { 6, 7, 5, Direction.Horizontal, false },
                     new object[] { 9, 9, 2, Direction.Vertical, false },
                     new object[] { 9, 9, 1, Direction.Vertical, true }
                };
            }
        }
    }
}
