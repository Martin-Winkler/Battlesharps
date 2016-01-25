using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{

    /// <summary>
    /// represents a  shot impact and its location, and whether that impact hit or sunk a ship
    /// </summary>
    class Impact
    { 
        HitDetection _hitDetection;
        Coord[] coords;

        public HitDetection HitDetection { get { return this._hitDetection; } set { this._hitDetection = value; } }
        
        public Coord[] Coords 
        { 
            get { return this.coords; } 
            set { this.coords = value; } 
        }


    }
    enum HitDetection
    {
        Hit,
        Miss,
        Sink
    }
}
