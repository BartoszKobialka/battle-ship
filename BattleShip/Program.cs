using System;

namespace BattleShip {
    class Program {
        public const int MAX_SHIP_FIELDS = 17;

        static void Main(string[] args) {
            Console.Write("Any key - Fastforeward game.\na - Step-by-step game.\n");

            bool isStepByStep = Console.ReadKey().Key == ConsoleKey.A;

            Player player1 = new Player();
            Player player2 = new Player();

            Console.WriteLine("Player1");
            player1.playerBoard.PrintOnConsole();
            Console.Write("\n\n");
            Console.WriteLine("Player2");
            player2.playerBoard.PrintOnConsole();

            int turnsCount = 0;
            Console.Write("\n\n");

            while (player1.playerBoard.CountFieldsByType(FieldType.SunkenShip) < MAX_SHIP_FIELDS
                && player2.playerBoard.CountFieldsByType(FieldType.SunkenShip) < MAX_SHIP_FIELDS) {
                
                turnsCount++;

                player1.MakeGuess();
                player1.UpdateAvailableHitPoints();

                if (player2.playerBoard.boardMatrix[player1.lastGuess.x][player1.lastGuess.y].fieldType != FieldType.Empty) {
                    player2.playerBoard.SetFieldType(player1.lastGuess, FieldType.SunkenShip);

                    if (player2.playerBoard.isShipSunken(player1.lastGuess)) {
                        player1.MarkGuessedAs(FieldType.SunkenShip);
                    } else {
                        player1.MarkGuessedAs(FieldType.Ship);
                    }
                } else {
                    player1.MarkGuessedAs(FieldType.MisHitted);
                    player2.playerBoard.SetFieldType(player1.lastGuess, FieldType.MisHitted);
                }

                if (player1.playerBoard.CountFieldsByType(FieldType.SunkenShip) == MAX_SHIP_FIELDS
                    && player2.playerBoard.CountFieldsByType(FieldType.SunkenShip) == MAX_SHIP_FIELDS) 
                    break;

                player2.MakeGuess();
                player2.UpdateAvailableHitPoints();

                if (player1.playerBoard.boardMatrix[player2.lastGuess.x][player2.lastGuess.y].fieldType != FieldType.Empty) {
                    player1.playerBoard.SetFieldType(player2.lastGuess, FieldType.SunkenShip);

                    if (player1.playerBoard.isShipSunken(player2.lastGuess)) {
                        player2.MarkGuessedAs(FieldType.SunkenShip);
                    } else {
                        player2.MarkGuessedAs(FieldType.Ship);
                    }
                } else {
                    player2.MarkGuessedAs(FieldType.MisHitted);
                    player1.playerBoard.SetFieldType(player2.lastGuess, FieldType.MisHitted);
                }
                
                Console.WriteLine("Turn number: " + turnsCount);
                Console.WriteLine("Player1");
                Console.WriteLine("{0}{1}", (player1.lastGuess.x + 1), ((char)((int)'A' + player1.lastGuess.y)));
                player1.playerBoard.PrintOnConsole();

                Console.Write("\n\n");

                Console.WriteLine("Player2");
                Console.WriteLine("{0}{1}", (player2.lastGuess.x + 1), ((char)((int)'A' + player2.lastGuess.y)));
                player2.playerBoard.PrintOnConsole();

                if (isStepByStep) {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                
                Console.Clear();
            }

            Console.WriteLine("Game took " + turnsCount + " turns");

            if (player1.playerBoard.CountFieldsByType(FieldType.SunkenShip) == MAX_SHIP_FIELDS) {
                Console.WriteLine("Player2 won!");
            } else {
                Console.WriteLine("Player1 won!");
            }

            Console.Write("\n");

            Console.WriteLine("Player1");
            player1.playerBoard.PrintOnConsole();

            Console.Write("\n\n");
            
            Console.WriteLine("Player2");
            player2.playerBoard.PrintOnConsole();
        }
    }
}