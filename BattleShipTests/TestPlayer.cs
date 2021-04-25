using Xunit;
using BattleShip;
using System.Collections.Generic;

namespace BattleShipTests {
    public class TestPlayer {
        
        [Fact]
        public void TestMarkNotAvaliableVerticalFields() {
            Player player = new Player();
            Ship ship = new Ship(4);
            ship.coords = new Point(1, 1);
            ship.direction = Direction.Vertical;
            player.enemyBoard.PlaceShip(ship);
            player.hittedShip = ship;
            player.MarkNotAvaliableFields();

            Assert.Equal(FieldType.MisHitted, player.enemyBoard.boardMatrix[1][0].fieldType);
            Assert.Equal(FieldType.MisHitted, player.enemyBoard.boardMatrix[1][5].fieldType);
        }

        [Fact]
       public void TestMarkNotAvaliableHorizontalFields() {
            Player player = new Player();
            Ship ship = new Ship(4);
            ship.coords = new Point(1, 1);
            ship.direction = Direction.Horizontal;
            player.enemyBoard.PlaceShip(ship);
            player.hittedShip = ship;
            player.MarkNotAvaliableFields();

            Assert.Equal(FieldType.MisHitted, player.enemyBoard.boardMatrix[0][1].fieldType);
            Assert.Equal(FieldType.MisHitted, player.enemyBoard.boardMatrix[5][1].fieldType);
        }

        [Theory, MemberData(nameof(TestMarkGuessedAsSettingShipDirectionData))]
        public void TestMarkGuessedAsSettingShipDirection(int shipX, int shipY, int guessX, int guessY, Direction direction) {
            Player player = new Player();
            player.lastGuess = new Point(guessX, guessY);
            player.hittedShip = new Ship(1);
            player.hittedShip.coords = new Point(shipX, shipY);

            player.MarkGuessedAs(FieldType.Ship);

            Assert.Equal(direction, player.hittedShip.direction);
        }

        public static IEnumerable<object[]> TestMarkGuessedAsSettingShipDirectionData {
            get {
                return new[]
                {
                     new object[] { 0, 0, 0, 1, Direction.Vertical },
                     new object[] { 1, 3, 1, 2, Direction.Vertical },
                     new object[] { 5, 8, 6, 8, Direction.Horizontal },
                     new object[] { 5, 8, 4, 8, Direction.Horizontal }
                };
            }
        }

    }
}
