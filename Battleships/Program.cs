using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Program
    {
        static Winner _winner;
        public static Winner Winner{ get { return _winner; } set { _winner = value; } }

        static void Main(string[] args)
        {
            //Umgebung vorbereiten
            _winner = Winner.undetermined;
            Console.WindowWidth = 100;
			Console.ForegroundColor = ConsoleColor.Gray;
            
            //Spielfelder auf denen die eigenen Schiffe dargestellt werden erzeugen
            HomeGrid playerHomeGrid = new HomeGrid(Playertype.Human);
            HomeGrid aiHomeGrid = new HomeGrid(Playertype.AI);

            //Radarfelder initialisieren
            EnemyGrid playerEnemyGrid = new EnemyGrid(Playertype.Human);
            EnemyGrid AIEnemyGrid = new EnemyGrid(Playertype.AI);

            //Schiffe  platzieren
			Fleet playerFleet = new Fleet();
            Fleet aiFleet = new Fleet();
            playerHomeGrid.DeployFleet(playerFleet.ships);
            aiHomeGrid.DeployFleet(aiFleet.ships);
            bool forfeit=false;

            // Schusswechsel-Events abonnieren
            playerEnemyGrid.AimFireEvent += aiHomeGrid.ReceiveShot;
            AIEnemyGrid.AimFireEvent += playerHomeGrid.ReceiveShot;

            //spielablauf
            do
            {
                forfeit = playerEnemyGrid.AimAndFire();  
                if (Winner == Winner.undetermined && !forfeit) AI.Turn(AIEnemyGrid);
  
            } while (Winner == Winner.undetermined && !forfeit);

            //spielende
            Console.SetCursorPosition(0, 23);

            if (forfeit)
            {
                
                Console.WriteLine("AI has won by player forfeiture");
            }
            else
            {
               
                Console.WriteLine("Game ends. {0} victorious!", _winner.ToString());
            }
            Console.ReadLine();
			
        }

        public static Winner DetermineWinner(Playertype loser)
        { //ermittelt den Gewinner anhand des Verlierers
            Winner result;
            if (loser==Playertype.Human)
            {
                result = Winner.AI;
            }
            else
            {
                result = Winner.player;
            }
            return result;
        }
    }

    enum Winner
    {
        undetermined,
        player,
        AI,
    }
}
