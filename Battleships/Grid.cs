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

        protected virtual void Redraw()
        { //gibt das Radar als Ascii-Art aus
            if (_PLAYERTYPE == Playertype.Human)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorLeft = CursorLeftStart;

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

        protected abstract ConsoleColor TileAsColor(int col, int row);

        public Grid(Playertype PLAYERTYPE, int cursorLeftStart)
        {
            this._PLAYERTYPE = PLAYERTYPE;
            this.CursorLeftStart = cursorLeftStart;
        }

        protected abstract class TileTranslation
        {
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
