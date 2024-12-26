using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class CheckersPiece
    {
        public PlayerType Owner { get; private set; }
        public bool isKing { get; private set; }

        public CheckersPiece(PlayerType i_Owner)
        {
            Owner = i_Owner;
            isKing = false;
        }

        public override string ToString()
        {
            return isKing ? "King" : "Regular";
        }
    }
}
