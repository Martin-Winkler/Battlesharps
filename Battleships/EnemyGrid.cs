using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class EnemyGrid : Grid
    {
        //Enemy Grid stellt das Radar (bekannte Treffer auf Gegnerschiffe) dar
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
        private RadarTile HitDetectionAsRadarTile(HitDetection hitDetection)
        { //gibt für einen Einschlagstyp den dazugehörigen Feldtyp zurück.
            switch (hitDetection)
            {
                case HitDetection.Hit: return RadarTile.ship;

                case HitDetection.Miss: return RadarTile.water;

                case HitDetection.Sink: return RadarTile.debris;
                default: return RadarTile.fog;
            }
        }

        public EnemyGrid(Playertype Playertype)
            : base(Playertype, 0)
        { // erzeugt das radar mit FOW überall

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
        protected sealed override ConsoleColor TileAsColor(int row, int col)
        {//gibt zu einer Koordinaten die dazugehörige Farbe zurück
            return tileColorTranslation[this.radartile[row, col]];
        }

        public bool AimAndFire()
        { //gibt einen Schuss auf das übergebene Spielfeld ab.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            this.Redraw();

            ConsoleKeyInfo myKey;
            do
            {   //lass den Spieler den Schussort bestimmen
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
                        //Impact impact = targetGrid.ReceiveShot(cursorPosition);
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
        private void RefreshTiles(Coord[] coords, RadarTile toType)  // ändert den Feldtyp der angegebenen Felder 
        {                                                              // auf dem Radar in den angegebenen Typ.
            for (int i = 0; i < coords.Length; i++)
            {
                Coord coord = coords[i];
                radartile[coord.Row, coord.Col] = toType;
            }
            this.Redraw();
        }
    }
	enum RadarTile 
	{
		fog,
		ship,
		water,
        debris
	}


}

