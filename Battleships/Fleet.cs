using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Fleet
    {
        public Ship[] Ships
        {
            get { return this.ships; }
        }

        private Ship[] ships = new Ship[7];

		public int ShipCount 
		{  //gibt die Anzahl der Schiffe in der Flotte zurück
			get
			{
				return this.ships.Length;
			}
		}
        public Ship Select(int index)
        {
            return this.ships[index];
        }

        /// <summary>
        /// Constructor for the Fleet class. Creates a Fleet of ships in the right size and the right amounts.
        /// </summary>
        public Fleet()
        { 
            for (int i = 0; i <= ships.Length-1; i++)
            {
                if (i<3)
                {
                    ships[i] = new Ship(5-i);
                }
                else if(i <5 && i>2)
                {
                    ships[i] = new Ship(2);
                }
                else if (i>4)
                {
                    ships[i] = new Ship(1);
                } 
            }
        }
    }
}
