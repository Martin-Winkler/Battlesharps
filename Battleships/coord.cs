using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    struct Coord
    {
        int row;
        int col;
        

        public int Row 
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }

        public Coord(int row, int col)
        {
            this.col =col;
            this.row = row;
        }

        public Coord(Coord coord)
        {
            this = coord;
        }

        public static bool Equals(Coord Operand1, Coord Operand2)
        {// überprüft, ob bei zwei übergebenen Coord-Variablen beide Werte gleich sind
            bool result = (Operand1.Col == Operand2.Col && Operand1.Row == Operand2.Row);
            return result;

        }
    }
}