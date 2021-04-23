﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip {
    public enum Direction {
        Horizontal = 0,
        Vertical = 1
    }

    public class GameBoard {
        public List<List<BoardField>> boardMatrix;

        public static int MaxIndex { get => 9; }

        public GameBoard() {
            this.boardMatrix = this.GenerateBase();
        }

        private Dictionary<FieldType, char> FieldTypeToCharacters = new Dictionary<FieldType, char> {
            {FieldType.Empty, '#'},
            {FieldType.Ship, '='},
            {FieldType.SunkenShip, 'X'},
            {FieldType.MisHitted, 'O'},
        };

        private List<List<BoardField>> GenerateBase() {
            List<List<BoardField>> matrix = new List<List<BoardField>>();

            for (int i = 0; i <= MaxIndex; i++) {
                matrix.Add(new List<BoardField>());
                for (int j = 0; j <= MaxIndex; j++) {
                    matrix[i].Add(new BoardField(i, j));
                }
            }

            return matrix;
        }

        public void FillBoard() {
            List<int> shipsLenght = new List<int> { 5, 4, 3, 2, 2, 1 };

            foreach (int length in shipsLenght) {
                bool placed = false;
                Ship ship = new Ship(length);

                while (!placed) {
                    ship.direction = (Direction)new Random().Next(2);
                    ship.coords = new Point();

                    if (this.canBePlaced(ship)) {
                        this.PlaceShip(ship);
                        placed = true;
                    }
                }
            }
        }

        public bool canBePlaced(Ship ship) {
            if (ship.direction == Direction.Horizontal) {
                if (ship.coords.x + ship.length - 1 > MaxIndex)
                    return false;

                return this.isRowClear(ship.coords.x - 1, ship.getEndPoint().x, ship.coords.y - 1)
                    && this.isRowClear(ship.coords.x - 1, ship.getEndPoint().x, ship.coords.y) 
                    && this.isRowClear(ship.coords.x - 1, ship.getEndPoint().x, ship.coords.y + 1);
            } else {
                if (ship.coords.y + ship.length - 1 > MaxIndex)
                    return false;

                return this.isColumClear(ship.coords.y - 1, ship.getEndPoint().y, ship.coords.x - 1)
                    && this.isColumClear(ship.coords.y - 1, ship.getEndPoint().y, ship.coords.x)
                    && this.isColumClear(ship.coords.y - 1, ship.getEndPoint().y, ship.coords.x + 1);
            }
        }

        public bool isRowClear(int startX, int endX, int y) {
            if (!Enumerable.Range(0, 10).Contains(y))
                return true;

            for (int i = Math.Max(0, startX); i <= Math.Min(MaxIndex, endX); i++) {
                if (this.boardMatrix[y][i].fieldType != FieldType.Empty)
                    return false;
            }

            return true;
        }
        public bool isColumClear(int startY, int endY, int x) {
            if (!Enumerable.Range(0, 10).Contains(x))
                return true;
            
            for (int i = Math.Max(0, startY); i <= Math.Min(MaxIndex, endY); i++) {
                if (this.boardMatrix[i][x].fieldType != FieldType.Empty)
                    return false;
            }

            return true;
        }

        public void PlaceShip(Ship ship) {
            if (ship.direction == Direction.Horizontal) {
                for (int i = ship.coords.x; i < ship.getEndPoint().x; i++) {
                    this.boardMatrix[ship.coords.y][i].fieldType = FieldType.Ship;
                }
            } else {
                for (int i = ship.coords.y; i < ship.getEndPoint().y; i++) {
                    this.boardMatrix[i][ship.coords.x].fieldType = FieldType.Ship;
                }
            }
        }

        public int CountFieldsByType(FieldType type) {
            int count = 0;

            this.boardMatrix.ForEach(columnElement => {
                columnElement.ForEach(rowElement => {
                    if (rowElement.fieldType == type) count++;
                });
            });

            return count;
        }

        public void SetFieldType(Point fieldCoords, FieldType type) {
            this.boardMatrix[fieldCoords.x][fieldCoords.y].fieldType = type;
        }
        
        public void PrintOnConsole() { 
            for (int i = 0; i <= MaxIndex; i++) {
                for (int j = 0; j <= MaxIndex; j++) {
                    Console.Write(" " + FieldTypeToCharacters[boardMatrix[i][j].fieldType]);
                }
                Console.Write("\n");
            }
        }
    }
}
