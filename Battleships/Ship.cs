using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Ship
    {
        #region classes dict
        //public const Dictionary<int,string> SHIPCLASS = new Dictionary<int, string>()
        //{
        //    {5, "aircraft carrier"},
        //    {4, "Battleship"},
        //    {3, "Cruiser"},
        //    {2, "Destroyer"},
        //    {1, "Submarine"}
        //};
        #endregion
        private readonly int _size;
        private Bulkhead[] bulkheads;// private int[][] coords = new int[5][];
        private bool _destroyed;

        

        public bool Destroyed
        {
            get { return _destroyed; }
            set { ; }
        }

        public int Size 
        {
            get
            {
                return this._size;
            }
            
        }
        public Coord[] Move(Coord BowCoords, bool directionIsNorthSouth)
        /// <summary> Platziert das schiff an den angegebenen Koordinaten und gibt einen Array aller koordinaten, die das Schiff einnimmt, zurück </summary>    
        /// <param name="BowCoords">Position des Bugs</param>
        /// <param name="directionIsNorthSouth">in welcher Ausrichtung das Schiff platziert werden soll. 
        /// wenn true, wird das schiff so gesdreht, dass das Heck südlich des Bugs ist. (Coord.Row wird entlang des Schiffes erhöht)
        /// if false, the stern of the ship will be east of the ship. (Coord.Col wird entlang des Schiffes erhöht)</param>
        /// <returns>An array of all the coordinates occupied by this ship</returns>
        {
            Coord[] result = new Coord[this._size];
            switch (directionIsNorthSouth)
            {
                case false:
                    {
                        for(int i = 0; i < this.Size; i++)
                        {
                            
                            this.bulkheads[i].Coord = BowCoords;
                            BowCoords.Col++;
                        }
                        break;
                    }
              
                default:                                                            //vertikale richtung  noch nicht implementiert
                    break;
            } // Koordinaten aller Bulkheads für die Rückgabe vorbereiten
            result = this.GetCoords;
            return result;
        }
        
        public Coord[] GetCoords
        {
            /// gibt alle Felder die von diesem Schiff eingenommen werden zurück
            get
            {
                Coord[] result = new Coord[this._size];
                for (int i = 0; i < this._size; i++)
                {
                    Coord bulkheadCoord = this.bulkheads[i].Coord;
                    result[i] = bulkheadCoord;
                }
                return result;
            }
        }

        public Ship(int size)
        {
            _size= size;
            bulkheads = new Bulkhead[_size];
            for (int i = 0; i < this._size; i++)
            {
                this.bulkheads[i] = new Bulkhead();
            }
        }


        public Impact TryToGetHit(Coord coord)
        { // ermittelt anhand einer Koordinate, ob ein treffer an dieser Koordinate Das schiff trifft, verfehlt, oder versenkt.
            Impact impactResult = new Impact();
            impactResult.HitDetection = HitDetection.Miss;

            for (int i = 0; i < this._size; i++)
            {
                if (Coord.Equals(this.bulkheads[i].Coord, coord)) //Bulkhead wurde getroffen
                {
                    this.bulkheads[i].ReceiveHit();
                    bool sunk=false;
                    for (int j = 0; j < this._size; j++)  //prüfen, ob dieser Treffer das Schiff versenkt
                    {
                        sunk = this.bulkheads[j].Damaged;
                        if (!sunk) break;
                    }
                    if (sunk)                               //wurde das Schiff versenkt, geben wir die Koordinaten zurück
                    {                                                   //und stellen das schiff auf versenkt
                        impactResult.HitDetection = HitDetection.Sink;
                        impactResult.Coords = this.GetCoords;
                        this._destroyed = true;
                        return impactResult;
                    }
                    else 
                    {
                        impactResult.HitDetection = HitDetection.Hit;
                        Coord[] coords = new Coord[1];
                        coords[0] = coord;
                        impactResult.Coords = coords;
                        return impactResult;
                    }
                }
            }                                                //wenn nach Durchlaufen aller Bulkheads keiner getroffen wurde
            impactResult.HitDetection = HitDetection.Miss;   //  glauben wir, dass dieses schiff nicht getroffen wurde
            
            return impactResult;
        }
    }

    
}
