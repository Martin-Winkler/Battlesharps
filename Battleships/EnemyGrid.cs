using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    /// <summary>
    /// the enemyGrid Constitutes the radar which shows the known positions of enemy ships and positions where the player has shot at
    /// </summary>
    class EnemyGrid : Grid
    {
        public delegate void AimAndFireEventHandler(AimAndFireEventArgs e);
        public event AimAndFireEventHandler AimFireEvent;
        protected Coord cursorPosition;
        protected RadarTile[,] radartile = new RadarTile[10, 10];
        protected static Dictionary<RadarTile, ConsoleColor> tileColorTranslation =
            new Dictionary<RadarTile, ConsoleColor>() 
                {
                    {RadarTile.fog, ConsoleColor.DarkGray },
                    {RadarTile.ship, ConsoleColor.Red},
                    {RadarTile.water, ConsoleColor.Cyan},
                    {RadarTile.debris, ConsoleColor.White},
                };


        /// <summary>
        /// returns the Battleships.RadarTile that corresponds to the passed Battleships.HitDetection
        /// </summary>
        /// <param name="hitDetection">the Battleships.Hitdetection that you want to convert to a Battleships.RadarTile</param>
        /// <returns>the RadarTile that corresponds to the HitDetection</returns>
        private RadarTile HitDetectionAsRadarTile(HitDetection hitDetection)
        { 
            switch (hitDetection)
            {
                case HitDetection.Hit: return RadarTile.ship;

                case HitDetection.Miss: return RadarTile.water;

                case HitDetection.Sink: return RadarTile.debris;
                default: return RadarTile.fog;
            }
        }

        /// <summary>
        /// Constructor method for the EnemyGrid class. 
        /// Creates an EnemyGrid object and initializes with RadarTile.fog in every RadarTile
        /// to emulate the fog of war.
        /// </summary>
        /// <param name="Playertype">whether you want to create the Radartile for the player or the AI opponent.</param>
        public EnemyGrid(Playertype Playertype)
            : base(Playertype, 0)
        {

            this.cursorPosition.Col = this.cursorPosition.Row = 5;
            for (int i = 0; i < radartile.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < radartile.GetLength(1); j++)
                {
                    radartile[i, j] = RadarTile.fog;
                }
            }
            this.Redraw();
        }


        /// <summary>
        /// Returns the color that the tile on the radar should have. The two values passed as parameters constitute the 2-dimensional coordinate that corresponds to a RadarTile.
        /// </summary>
        /// <param name="row">the latitudinal coordinate on the grid for which you want the color. 0-Indexed and starting from the top border</param>
        /// <param name="col">the longitudinal coordinate on the grid for which you want the color. 0-Indexed and starting from the left border</param>
        /// <returns></returns>
        protected sealed override ConsoleColor TileAsColor(int row, int col)
        {//gibt zu einer Koordinaten die dazugehörige Farbe zurück
            return tileColorTranslation[this.radartile[row, col]];
        }

        /// <summary>
        /// controls an interface where the player may choose where to attempt a shot or forfeit the game to quit early
        /// </summary>
        /// <returns>returns true if the player forfeited the game</returns>
        public bool AimAndFire()
        { //gibt einen Schuss auf das übergebene Spielfeld ab.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            this.Redraw();

            ConsoleKeyInfo myKey;
            do
            {   //let the player choose a coordinate for taking a shot
                Console.SetCursorPosition(cursorPosition.Col * 4 + 3, cursorPosition.Row * 2 + 2);
                myKey = Console.ReadKey(true);
                switch (myKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (cursorPosition.Row - 1 >= 0) cursorPosition.Row--;
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cursorPosition.Row + 1 < 10) cursorPosition.Row++;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            if (cursorPosition.Col - 1 >= 0) cursorPosition.Col--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            if (cursorPosition.Col + 1 <= 9) cursorPosition.Col++;
                        }
                        break;
                    default:
                        break;
                }
            }
            while (myKey.Key != ConsoleKey.Spacebar && myKey.Key != ConsoleKey.X);
            switch (myKey.Key)
            {
                case ConsoleKey.Spacebar:
                    {
                        AimAndFireEventArgs e = new AimAndFireEventArgs();
                        e.Coord = cursorPosition;
                        AimFireEvent(e);
                        Impact impactResult = e.Impact;
                        this.RefreshTiles(impactResult.Coords, this.HitDetectionAsRadarTile(impactResult.HitDetection));
                        break;
                    }
                case ConsoleKey.X: return true;

                default:
                    break;
            }
            return false;
        }


        /// <summary>
        /// Function for the AI taking a shot
        /// </summary>
        /// <param name="coord">coords where the AI wants to take a shot</param>
        public void AimAndFire(Coord coord)
        { //Feuerfunktion für die KI.

            AimAndFireEventArgs e = new AimAndFireEventArgs();
            e.Coord = coord;
            //targetGrid.ReceiveShot(e); //deprecated due to transition to events
            AimFireEvent(e);
            Impact impactResult = e.Impact;
            if (impactResult.HitDetection == HitDetection.Miss)
            {
                Coord[] coords = new Coord[1];
                impactResult.Coords = coords;
                impactResult.Coords[0] = new Coord(coord);
            }
            this.RefreshTiles(impactResult.Coords, this.HitDetectionAsRadarTile(impactResult.HitDetection));
        }


        /// <summary>
        /// changes a group of tiles to another Type
        /// </summary>
        /// <param name="coords">an array of Battleships.Coord, expressing which coordinates should change</param>
        /// <param name="toType">the Battleships.RadarTile to change the Tiles at the given coords to</param>
        private void RefreshTiles(Coord[] coords, RadarTile toType)  
        {                                                            
            for (int i = 0; i < coords.Length; i++)
            {
                Coord coord = coords[i];
                radartile[coord.Row, coord.Col] = toType;
            }
            this.Redraw();
        }
    }

    /// <summary>
    /// all possible states that a RadarTile of a Battleships.EnemyGrid can be in
    /// </summary>
	enum RadarTile 
	{
		fog,
		ship,
		water,
        debris
	}
}

