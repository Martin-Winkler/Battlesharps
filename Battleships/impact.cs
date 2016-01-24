using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    class Impact
    { //speichert einen Einschlag von Beschuss, und ob dieser Einschlag ein schiff getroffen oder versenkt hat
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
