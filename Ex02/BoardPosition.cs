using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class BoardPosition
    {
        // מאפיינים
        public int Row { get; set; }  // שורה בלוח
        public int Col { get; set; }  // עמודה בלוח

        // קונסטרוקטור
        public BoardPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }

        // השוואת מיקומים
        public override bool Equals(object obj)
        {
            if (obj is BoardPosition other)
            {
                return Row == other.Row && Col == other.Col;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Col.GetHashCode();
        }

        // הצגת המיקום כטקסט
        public override string ToString()
        {
            return $"({Row}, {Col})";
        }
    }
}
