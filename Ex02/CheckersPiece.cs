using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class CheckersPiece
    {
        public PlayerType Owner { get; private set; }
        public bool isKing { get; private set; }

        public CheckersPiece(PlayerType i_Owner)
        {
            Owner = i_Owner;
            isKing = false;
        }
        public void PromoteToKing()
        {
            isKing = true;
        }

        public override string ToString()
        {
            return isKing ? "King" : "Regular";
        }
    }
}
