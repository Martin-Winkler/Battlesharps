using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    /// <summary>
    /// Class that implements all the behavior of the AI opponent
    /// </summary>
    static class AI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerHomeGrid"></param>
        /// <param name="aiEnemyGrid"></param>
        public static void Turn(HomeGrid playerHomeGrid, EnemyGrid aiEnemyGrid)
        {
            //führt den spielzug des Computergegners durch
            Coord coord = new Coord();
            Random rnd = new Random(DateTime.Now.GetHashCode());
            coord.Col = rnd.Next(10);
            coord.Row = rnd.Next(10);
            aiEnemyGrid.AimAndFire(coord);
        }
    }
}
