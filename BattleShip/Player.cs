using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip { 
    public class Player {
        public GameBoard playerBoard = new GameBoard();
        public GameBoard enemyBoard = new GameBoard();
        public Ship hittedShip = null;
        public List<Point> availableHitPoints = new List<Point>();
        public Point lastGuess = new Point(10, 10);

        public Player() {
            this.playerBoard.FillBoard();
        }

        public void MakeGuess() {
            this.UpdateAvailableHitPoints();
           
            var tempLastGuess = new Point(this.lastGuess);

            if (this.hittedShip != null) {
                if (this.hittedShip.length > 1) {
                    if (this.hittedShip.direction == Direction.Horizontal) {
                        this.HorizontalGuess();
                    } else {
                        this.VerticalGuess();
                    }
                } else {
                    this.HorizontalGuess();

                    if (tempLastGuess == this.lastGuess)
                        this.VerticalGuess();
                }
            } else {
                this.RandomGuess();
            }

            bool isAvaliable = false;
            this.availableHitPoints.ForEach(point => {
                if (this.lastGuess == point) {
                    isAvaliable = true;
                }
            });

            if (!isAvaliable) {
                this.RandomGuess();
                this.hittedShip = null;
            }
        }

        public void HorizontalGuess() {
            if (this.hittedShip.coords.x > 0 ) {
                if (this.enemyBoard.boardMatrix[this.hittedShip.coords.x - 1][this.hittedShip.coords.y].fieldType == FieldType.Empty)
                    this.lastGuess = new Point(this.hittedShip.coords.x - 1, this.hittedShip.coords.y);
            } 
            
            if (this.hittedShip.getEndPoint().x <= GameBoard.MaxIndex) {
                if (this.enemyBoard.boardMatrix[this.hittedShip.getEndPoint().x][this.hittedShip.coords.y].fieldType == FieldType.Empty) 
                    this.lastGuess = new Point(this.hittedShip.getEndPoint().x, this.hittedShip.coords.y);
            }
        }

        public void VerticalGuess() {
            if (this.hittedShip.coords.y > 0) {
                if (this.enemyBoard.boardMatrix[this.hittedShip.coords.x][this.hittedShip.coords.y - 1].fieldType == FieldType.Empty) 
                    this.lastGuess = new Point(this.hittedShip.coords.x, this.hittedShip.coords.y - 1);
            }  
            
            if (this.hittedShip.getEndPoint().y <= GameBoard.MaxIndex) {
                if (this.enemyBoard.boardMatrix[this.hittedShip.coords.x][this.hittedShip.getEndPoint().y].fieldType == FieldType.Empty)
                    this.lastGuess = new Point(this.hittedShip.coords.x, this.hittedShip.getEndPoint().y);
            }
        }

        public void RandomGuess() {
            int availableFieldsCount = this.availableHitPoints.Count;
            int index = new Random().Next(availableFieldsCount);

            if (availableFieldsCount != 0)
                this.lastGuess = new Point(availableHitPoints[index]);
        }

        public void MarkGuessedAs(FieldType type) {
            this.enemyBoard.SetFieldType(this.lastGuess, type == FieldType.SunkenShip ? FieldType.Ship : type);

            if (type != FieldType.MisHitted) {
                if (this.hittedShip == null) {
                    this.hittedShip = new Ship(1);
                    this.hittedShip.coords = new Point(lastGuess);
                } else {
                    if (this.hittedShip.coords.y == this.lastGuess.y) {
                        if (this.hittedShip.coords.x > this.lastGuess.x)
                            this.hittedShip.coords = new Point(this.lastGuess);
                        this.hittedShip.direction = Direction.Horizontal;
                        this.hittedShip.length += 1;
                    } else {
                        if (this.hittedShip.coords.y > this.lastGuess.y)
                            this.hittedShip.coords  = new Point(this.lastGuess);
                        this.hittedShip.direction = Direction.Vertical;
                         this.hittedShip.length += 1;
                    }
                }

                if (type == FieldType.SunkenShip) {
                    this.MarkNotAvaliableFields();
                    this.hittedShip = null;
                }
            }
        }

        public void UpdateAvailableHitPoints() {
            this.availableHitPoints.Clear();
            
            for (int i = 0; i <= GameBoard.MaxIndex; i++) {
                for (int j = 0; j <= GameBoard.MaxIndex; j++) {
                    if (this.enemyBoard.boardMatrix[i][j].fieldType == FieldType.Empty)
                        this.availableHitPoints.Add(new Point(i, j));
                }
            }
        }

        public void MarkNotAvaliableFields() {
            if (this.hittedShip == null) {
                this.hittedShip = new Ship(1);
                this.hittedShip.coords = new Point(this.lastGuess);
                this.hittedShip.direction = Direction.Horizontal;   
            }

            if (hittedShip.direction == Direction.Horizontal) {
                for (int i = -1; i <= 1; i++) {
                    this.MarkNotAvaliableHorizontalFields(this.hittedShip.coords.x - 1, this.hittedShip.getEndPoint().x, this.hittedShip.coords.y + i);
                }
            } else {
                for (int i = -1; i <= 1; i++) {
                    this.MarkNotAvaliableVerticalFields(this.hittedShip.coords.y - 1, this.hittedShip.getEndPoint().y, this.hittedShip.coords.x + i);
                }
            }
        }

        public void MarkNotAvaliableHorizontalFields(int startX, int endX, int y) {
            if (!Enumerable.Range(0, 10).Contains(y))
                return;

            for (int i = Math.Max(0, startX); i <= Math.Min(GameBoard.MaxIndex, endX); i++) {
                if (this.enemyBoard.boardMatrix[i][y].fieldType == FieldType.Empty)
                    this.enemyBoard.boardMatrix[i][y].fieldType = FieldType.MisHitted;
            }
        }

        public void MarkNotAvaliableVerticalFields(int startY, int endY, int x) {
            if (!Enumerable.Range(0, 10).Contains(x))
                return;

            for (int i = Math.Max(0, startY); i <= Math.Min(GameBoard.MaxIndex, endY); i++) {
                if (this.enemyBoard.boardMatrix[x][i].fieldType == FieldType.Empty)
                    this.enemyBoard.boardMatrix[x][i].fieldType = FieldType.MisHitted;
            }
        }
    }
}
