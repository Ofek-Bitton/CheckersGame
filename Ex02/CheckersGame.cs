using System;

namespace Ex02
{
    internal class CheckersGame
    {
        public static void Main()
        {
            Console.Write("Enter board size (6, 8, or 10): ");
            int size = int.Parse(Console.ReadLine());
            Console.WriteLine();

            CheckersBoard board = new CheckersBoard(size);
            board.InitializeBoard();

            // מצב אכילה לבדיקה
            board.SetPieceAt(3, 4, new CheckersPiece(PlayerType.Human)); // חייל אנושי
            board.SetPieceAt(4, 3, new CheckersPiece(PlayerType.Computer)); // חייל יריב
            board.SetPieceAt(5, 2, null); // תא ריק

            board.DisplayBoardDebug();
            board.DisplayBoard();

            while (true)
            {
                board.DisplayBoardDebug(); // הדפס מצב הלוח הנוכחי
                Console.WriteLine("Enter your move (e.g., a3-c4): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                // פרש את המהלך
                try
                {
                    string[] parts = input.Split('-');
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input format. Please enter in the format 'a3-c4'.");
                        continue;
                    }

                    BoardPosition start = ParsePosition(parts[0]);
                    BoardPosition end = ParsePosition(parts[1]);

                    Console.WriteLine($"DEBUG: Start position -> Row: {start.Row}, Col: {start.Col}");
                    Console.WriteLine($"DEBUG: End position -> Row: {end.Row}, Col: {end.Col}");

                    GameMove move = new GameMove(start.Row, start.Col, end.Row, end.Col);

                    if (move.IsValidMove(board))
                    {
                        Console.WriteLine("Valid move!");
                        move.ExecuteMove(board);
                    }
                    else
                    {
                        Console.WriteLine("Invalid move!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                board.DisplayBoard();
            }


            Console.WriteLine("Game over!");
        }

        private static BoardPosition ParsePosition(string position)
        {
            if (string.IsNullOrEmpty(position) || position.Length != 2)
            {
                throw new ArgumentException("Position must be in the format 'a3', where 'a' is a row and '3' is a column.");
            }

            char rowChar = position[0];
            char colChar = position[1];

            if (rowChar < 'a' || rowChar > 'j') // בדוק אם האות נמצאת בתחום
            {
                throw new ArgumentException($"Row character '{rowChar}' is invalid. Must be between 'a' and 'j'.");
            }

            if (colChar < '1' || colChar > '9') // בדוק אם הספרה נמצאת בתחום
            {
                throw new ArgumentException($"Column character '{colChar}' is invalid. Must be between '1' and '9'.");
            }

            int row = rowChar - 'a'; // ממיר את האות לשורה (a -> 0, b -> 1 וכו')
            int col = colChar - '1'; // ממיר את הספרה לעמודה (1 -> 0, 2 -> 1 וכו')

            Console.WriteLine($"DEBUG: Parsed position {position} -> Row: {row}, Col: {col}");
            return new BoardPosition(row, col);
        }


    }
}
