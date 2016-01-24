﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class HomeGrid : Grid
    {
        protected Ship[] _ships;
        protected TileContent[,] homeTiles = new TileContent[10, 10];
        protected static Dictionary<TileContent, ConsoleColor> tileColorTranslation =
            new Dictionary<TileContent, ConsoleColor>() 
            {
                {TileContent.empty, ConsoleColor.Cyan },
                {TileContent.hitShip, ConsoleColor.Red},
                {TileContent.shell, ConsoleColor.Blue},
                {TileContent.ship, ConsoleColor.Gray},
                {TileContent.sunkShip, ConsoleColor.White}
            };

        public void DeployFleet(Ship[] ships)
        { // platziert alle schiffe der Flotte  an zufälligen Orten
            Random rnd = new Random(DateTime.Now.Ticks.GetHashCode());
            bool[] used = new bool[10];
            foreach (var ship in ships)
            {
                Coord coord = new Coord();              //freien platz finden
                coord.Col = rnd.Next(9 - ship.Size);

                do //verhindern, dass  zwei schiffe in die gleiche Zeile gepackt werden.
                {
                    coord.Row = rnd.Next(10);
                } while (used[coord.Row]);
                used[coord.Row] = true;

                Coord[] coords = ship.Move(coord, false);
                this.RefreshTiles(coords, TileContent.ship);
            }
            _ships = ships;
            this.Redraw();
        }
        public HomeGrid(Playertype playertype)
            : base(playertype, 50)
        {
           
        }

        public void ReceiveShot(AimAndFireEventArgs e)
        { //erhält eine Kordinate, wo hingeschossen wurde
            //und fragt jedes seine Schiffe, ob es an dieser Stelle getroffen wurde

            Impact result = new Impact();
            for (int i = 0; i < _ships.Length; i++)
            {
                result = _ships[i].TryToGetHit(e.Coord);
                if (result.HitDetection != HitDetection.Miss) //wenn ein Schiff getroffen wurde, 
                {                                             //  müssen wir nicht mehr prüfen, 
                    break;                                    //ob ein anderes Schiff getroffen wurde
                }
            }
            if (result.HitDetection == HitDetection.Sink)   //wenn das getroffene schiff versenkt wurde,
            {                                               //dann prüfen ob dieses Versenken das Spiel entschieden hat
                bool defeat = false;
                for (int i = 0; i < _ships.Length; i++)
                {
                    defeat = _ships[i].Destroyed;
                    if (!defeat) break;
                }
                if (defeat)
                {
                    Program.Winner = Program.DetermineWinner(this._PLAYERTYPE);
                }
            }
            if (result.HitDetection == HitDetection.Miss) //wenn kein Schiff getroffen wurde,  
            {                                           // kann auch kein schuff sagen wo es getroffen wurde
                Coord[] coords = new Coord[1] { e.Coord }; //also müssen wir die impact-Koordinate selbst angeben
                result.Coords = coords;
            }
            this.RefreshTiles(result.Coords, result.HitDetection);
            this.Redraw();

            e.Impact = result;
        }

        private void RefreshTiles(Coord[] coords, TileContent toType)
        { //ändert die Felder an den Angegebenen Koordinaten in den angegebenen typ                                                        
            for (int i = 0; i < coords.Length; i++)
            {
                homeTiles[coords[i].Row, coords[i].Col] = toType;
            }

        }
        private void RefreshTiles(Coord[] coords, HitDetection toTypeByHit)
        {
            for (int i = 0; i < coords.Length; i++)
            {
                homeTiles[coords[i].Row, coords[i].Col] = HitDetectionToTileContent(toTypeByHit);
            }
        }



        protected override ConsoleColor TileAsColor(int i, int j)
        { // gibt  die Farbe einer Feldkoordinate zurück
            //switch (this.homeTiles[i, j])
            //{
            //    case TileContent.empty: return ConsoleColor.Cyan;
            //    case TileContent.ship: return ConsoleColor.DarkGray;
            //    case TileContent.hitShip: return ConsoleColor.Red;
            //    case TileContent.sunkShip: return ConsoleColor.White;
            //    case TileContent.shell: return ConsoleColor.Blue;
            //    default:
            //        break;
            //}
            //return ConsoleColor.Black; //dummy

            return tileColorTranslation[this.homeTiles[i, j]];
        }



        private TileContent HitDetectionToTileContent(HitDetection hitdetection)
        {
            TileContent result = TileContent.empty;
            switch (hitdetection)
            {
                case HitDetection.Hit: result = TileContent.hitShip;
                    break;
                case HitDetection.Miss: result = TileContent.shell;
                    break;
                case HitDetection.Sink: result = TileContent.sunkShip;
                    break;
                default:
                    break;
            }
            return result;
        }
    }



    enum TileContent
    {
        empty,
        ship,
        hitShip,
        sunkShip,
        shell

    }

    enum Playertype {Human, AI }
}