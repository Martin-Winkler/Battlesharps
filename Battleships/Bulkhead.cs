using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    class Bulkhead
    {
        // der Bulkhead ist ein teil des Schiffes der ein Feld einnimmt und getroffen werden kann.

        Coord _coord;
        bool damaged;
        public Coord Coord
        {
            get{ return this._coord; }
            set { this._coord = value; }
        }
        public bool Damaged { get { return this.damaged; } }
        public delegate void ReceiveHitEventHandler(object sender, EventArgs e);


        public void ReceiveHit()
        {
            this.damaged = true;
        }

        public Bulkhead()
        {

        }
    }
}
