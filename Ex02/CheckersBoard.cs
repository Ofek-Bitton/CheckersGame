using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class CheckersBoard
    {
        private readonly int m_Size;
        private readonly CheckersPiece[,] m_Board;

        public CheckersBoard(int i_Size)
        {
            m_Size = i_Size; // גודל הלוח
            m_Board = new CheckersPiece[m_Size, m_Size];
        }

        public void InitializeBoard()
        {
            // הצבת כלים של שחקן Computer (למעלה)
            for (int i = 0; i < m_Size / 2 - 1; i++) // שורות העליונות
            {
                for (int j = 0; j < m_Size; j++)
                {
                    // תאים שחורים בלבד (מחושבים לפי סכום השורה והעמודה)
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = new CheckersPiece(PlayerType.Computer);
                        Console.WriteLine($"DEBUG: Placed Computer piece at ({i}, {j})");
                    }
                }
            }

            // הצבת כלים של שחקן Human (למטה)
            for (int i = m_Size - (m_Size / 2) + 1; i < m_Size; i++) // שורות התחתונות
            {
                for (int j = 0; j < m_Size; j++)
                {
                    // תאים שחורים בלבד (מחושבים לפי סכום השורה והעמודה)
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = new CheckersPiece(PlayerType.Human);
                        Console.WriteLine($"DEBUG: Placed Human piece at ({i}, {j})");
                    }
                }
            }
        }

        public void DisplayBoard()
        {
            Console.Write("   ");
            for (int col = 0; col < m_Size; col++)
            {
                Console.Write((char)('A' + col) + "   ");
            }
            Console.WriteLine();
            Console.WriteLine(" " + new string('=', m_Size * 4));

            for (int i = 0; i < m_Size; i++)
            {
                Console.Write((char)('a' + i) + "|");
                for (int j = 0; j < m_Size; j++)
                {
                    if (m_Board[i, j] == null)
                    {
                        Console.Write("   ");
                    }
                    else if (m_Board[i, j].Owner == PlayerType.Human)
                    {
                        Console.Write(m_Board[i, j].isKing ? " K " : " X "); // חייל אנושי
                    }
                    else
                    {
                        Console.Write(m_Board[i, j].isKing ? " U " : " O "); // חייל מחשב
                    }
                    Console.Write('|');
                }
                Console.WriteLine();
                Console.WriteLine(" " + new string('=', m_Size * 4));
            }
        }
        public void DisplayBoardDebug()
        {
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    var piece = m_Board[i, j];
                    string content = piece == null ? "empty" : (piece.Owner == PlayerType.Human ? "Human" : "Computer");
                    Console.Write($"({i},{j}): {content}  ");
                }
                Console.WriteLine();
            }
        }


        // פונקציה שבודקת אם תא מסוים פנוי
        public bool IsPositionEmpty(int row, int col)
        {
            return m_Board[row, col] == null;
        }

        // פונקציה שמחזירה את החייל שנמצא בתא מסוים
        public CheckersPiece GetPieceAt(int row, int col)
        {
            Console.WriteLine($"DEBUG: Checking piece at ({row}, {col})");
            return m_Board[row, col];
        }


        // פונקציה שמעדכנת את החייל בתא מסוים
        public void SetPieceAt(int row, int col, CheckersPiece piece)
        {
            m_Board[row, col] = piece;
        }

        // פונקציה שמחזירה את גודל הלוח
        public int GetSize()
        {
            return m_Size;
        }

    }
}
