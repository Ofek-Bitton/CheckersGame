using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            board.DisplayBoard();

            Console.WriteLine("Press any button to exit..");
            Console.ReadLine();
        }
    }
}
