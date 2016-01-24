using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    /// <summary>
    /// a more human-readable way to representing a 2-dimensional coordinate instead of using x and y or individual variables
    /// </summary>
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
        /// <summary>
        /// checks if two passed coordinates are Equal
        /// </summary>
        /// <param name="Operand1">first Coord object to check</param>
        /// <param name="Operand2">second Coord object to check</param>
        /// <returns>true if the two Coords are the same, otherwise false</returns>
        public static bool Equals(Coord Operand1, Coord Operand2)
        {
            bool result = (Operand1.Col == Operand2.Col && Operand1.Row == Operand2.Row);
            return result;
        }
    }
}