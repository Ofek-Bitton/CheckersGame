using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class GameMove
    {
        public int StartRow { get; private set; }
        public int StartCol { get; private set; }
        public int EndRow { get; private set; }
        public int EndCol { get; private set; }
        public bool IsCapture { get; private set; }

        public GameMove(int i_StartRow, int i_StartCol, int i_EndRow, int i_EndCol)
        {
            StartRow = i_StartRow;
            StartCol = i_StartCol;
            EndRow = i_EndRow;
            EndCol = i_EndCol;
            IsCapture = false;
        }

        public bool IsValidMove(CheckersBoard board)
        {
            Console.WriteLine($"DEBUG: Checking move validity -> Start: ({StartRow}, {StartCol}), End: ({EndRow}, {EndCol})");

            CheckersPiece piece = board.GetPieceAt(StartRow, StartCol);
            if (piece == null)
            {
                Console.WriteLine($"DEBUG: No piece at start position ({StartRow}, {StartCol}).");
                return false;
            }

            if (!board.IsPositionEmpty(EndRow, EndCol))
            {
                Console.WriteLine($"DEBUG: End position ({EndRow}, {EndCol}) is not empty.");
                return false;
            }

            int rowDiff = EndRow - StartRow;
            int colDiff = Math.Abs(EndCol - StartCol);

            Console.WriteLine($"DEBUG: Row diff: {rowDiff}, Col diff: {colDiff}");

            if (HasMandatoryCapture(board, piece.Owner))
            {
                Console.WriteLine("DEBUG: Capture move is mandatory.");
                if (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 2)
                {
                    return IsCaptureMove(board);
                }
                return false;
            }

            if (!piece.isKing)
            {
                int direction = piece.Owner == PlayerType.Human ? -1 : 1;
                if (rowDiff == direction && colDiff == 1)
                {
                    return true;
                }
            }
            else
            {
                if (Math.Abs(rowDiff) == Math.Abs(colDiff))
                {
                    return true;
                }
            }

            return false;
        }


        public bool IsCaptureMove(CheckersBoard board)
        {
            bool isCapture = false;

            int middleRow = (StartRow + EndRow) / 2;
            int middleCol = (StartCol + EndCol) / 2;

            CheckersPiece middlePiece = board.GetPieceAt(middleRow, middleCol);
            CheckersPiece startPiece = board.GetPieceAt(StartRow, StartCol);

            if (middlePiece != null && middlePiece.Owner != startPiece.Owner)
            {
                IsCapture = true;
                isCapture = true;
            }

            return isCapture;
        }

        private bool HasMandatoryCapture(CheckersBoard board, PlayerType player)
        {
            for (int row = 0; row < board.GetSize(); row++)
            {
                for (int col = 0; col < board.GetSize(); col++)
                {
                    CheckersPiece piece = board.GetPieceAt(row, col);
                    if (piece != null && piece.Owner == player && CanCapture(board, row, col, piece))
                    {
                        Console.WriteLine($"DEBUG: Piece at ({row}, {col}) has a capture move.");
                        return true;
                    }
                }
            }
            return false;
        }


        private bool CanCapture(CheckersBoard board, int row, int col, CheckersPiece piece)
        {
            int[] directions = piece.isKing ? new int[] { -1, 1 } : new int[] { piece.Owner == PlayerType.Human ? -1 : 1 };
            foreach (int direction in directions)
            {
                int enemyRow = row + direction;
                int captureRow = row + 2 * direction;

                foreach (int colOffset in new int[] { -1, 1 })
                {
                    int enemyCol = col + colOffset;
                    int captureCol = col + 2 * colOffset;

                    if (IsInsideBoard(board, enemyRow, enemyCol) &&
                        IsInsideBoard(board, captureRow, captureCol) &&
                        board.GetPieceAt(enemyRow, enemyCol)?.Owner != piece.Owner &&
                        board.GetPieceAt(enemyRow, enemyCol) != null &&
                        board.IsPositionEmpty(captureRow, captureCol))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsInsideBoard(CheckersBoard board, int row, int col)
        {
            return row >= 0 && row < board.GetSize() && col >= 0 && col < board.GetSize();
        }
        public void ExecuteMove(CheckersBoard board)
        {
            Console.WriteLine($"DEBUG: Executing move from ({StartRow}, {StartCol}) to ({EndRow}, {EndCol})");

            CheckersPiece piece = board.GetPieceAt(StartRow, StartCol);

            // הזזת החייל לתא הסופי
            board.SetPieceAt(EndRow, EndCol, piece);
            board.SetPieceAt(StartRow, StartCol, null);

            // הסרת חייל יריב אם מדובר במהלך אכילה
            if (IsCapture)
            {
                int middleRow = (StartRow + EndRow) / 2;
                int middleCol = (StartCol + EndCol) / 2;
                Console.WriteLine($"DEBUG: Removing opponent piece at ({middleRow}, {middleCol})");
                board.SetPieceAt(middleRow, middleCol, null);
            }

            // קידום למלכה אם הגיע לקצה הלוח
            if (!piece.isKing && (EndRow == 0 || EndRow == board.GetSize() - 1))
            {
                Console.WriteLine($"DEBUG: Promoting piece at ({EndRow}, {EndCol}) to king");
                piece.PromoteToKing();
            }
        }

    }
}

