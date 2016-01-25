using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    /// <summary>
    /// the abstract Grid class serves as the base class for both the Home Grid, in which the players' own ships are displayed
    /// as well as the Enemy grid, in which a players past shots and known positions of enemy ships are displayed
    /// </summary>
    abstract class Grid
    {
        protected readonly Playertype _PLAYERTYPE;
        public int MyProperty { get; set; }
        protected const string horizontalLine = "  ---+---+---+---+---+---+---+---+---+---+";
        //protected TileContent[,] homeTiles = new TileContent[10, 10];
        protected readonly int CursorLeftStart;
        public delegate ConsoleColor TranslateTile();

        /// <summary>
        /// Draws any Grid as ascii-art. (polymorphic)
        /// </summary>
        protected virtual void Redraw()
        { 
            if (_PLAYERTYPE == Playertype.Human)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorLeft = CursorLeftStart; // it's good knowing that calling this polymorphic function can uses vatriables that have their values in derived classes

                Console.WriteLine(" | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |");
                Console.CursorLeft = CursorLeftStart;
                Console.WriteLine(horizontalLine);
                for (int i = 0; i < 10; i++)
                {
                    Console.CursorLeft = CursorLeftStart;
                    Console.Write("{0}", i);
                    Console.Write("|");
                    for (int j = 0; j < 10; j++)
                    {
                        Console.BackgroundColor = TileAsColor(i, j);
                        Console.Write("   ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("|");
                    }
                    Console.WriteLine();
                    Console.CursorLeft = CursorLeftStart;
                    Console.WriteLine(horizontalLine);
                }
            }
        }

        /// <summary>
        /// abstract function returning Grid color at a given coordinate. Derived classes must implement this function.
        /// </summary>
        /// <param name="col">the latitudinal coordinate on the grid for which you want the color. 0-Indexed and starting from the top border</param>
        /// <param name="row">the longitudinal coordinate on the grid for which you want the color. 0-Indexed and starting from the left border</param>
        /// <returns></returns>
        protected abstract ConsoleColor TileAsColor(int col, int row);

        /// <summary>
        /// constructor for the abstract grid class
        /// </summary>
        /// <param name="PLAYERTYPE">whether the grid to be created is for a human or nonhuman player</param>
        /// <param name="cursorLeftStart">where to position the grid</param>
        public Grid(Playertype PLAYERTYPE, int cursorLeftStart)
        {
            this._PLAYERTYPE = PLAYERTYPE;
            this.CursorLeftStart = cursorLeftStart;
        }
    }



    class AimAndFireEventArgs : System.EventArgs
    {
        Coord coord;
        Impact impact;

        public Coord Coord
        {
            get { return this.coord; }
            set { this.coord = value; }
        }
        public Impact Impact
        {
            get { return this.impact; }
            set { this.impact = value; }
        }
    }
}
