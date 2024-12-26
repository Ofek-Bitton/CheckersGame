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
                    if ((i + j) % 2 == 1) // תאים שחורים בלבד
                    {
                        m_Board[i, j] = new CheckersPiece(PlayerType.Computer);
                    }
                }
            }

            // הצבת כלים של שחקן Human (למטה)
            for (int i = m_Size - (m_Size / 2) + 1; i < m_Size; i++) // שורות התחתונות
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i + j) % 2 == 1) // תאים שחורים בלבד
                    {
                        m_Board[i, j] = new CheckersPiece(PlayerType.Human);
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
            Console.WriteLine(" " + new string('=' , ((int)Math.Round(m_Size * 4.125))));


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
                        Console.Write(m_Board[i, j].isKing ? " K" : " X "); // כלי שחקן אנושי
                    }
                    else
                    {
                        Console.Write(m_Board[i, j].isKing ? " U" : " O "); // כלי שחקן מחשב
                    }
                    Console.Write('|');
                }
                Console.WriteLine("");
                Console.WriteLine(" " + new string('=', ((int)Math.Round(m_Size * 4.125))));
            }
        }

    }
}
