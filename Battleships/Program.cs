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
            //Prepare Operating-system independent environment and initialize some variables
            _winner = Winner.undetermined;
<<<<<<< HEAD
            if (PlatformID.Win32NT == Environment.OSVersion.Platform) Console.WindowWidth = 100;
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;
=======
            if (PlatformID.Win32NT == Environment.OSVersion.Platform) 
            {
                Console.WindowWidth = 100;
            } 
            else
            {
                Console.WriteLine("Please make sure that your Terminal Window is at least 100 characters wide");
                Console.WriteLine("Press any key to cotinue when ready...");
                Console.ReadKey();
            }
	        Console.ForegroundColor = ConsoleColor.Gray;
>>>>>>> refs/remotes/origin/master
            
            //Create and initialize playing grids in which each player can see their own ships
            HomeGrid playerHomeGrid = new HomeGrid(Playertype.Human);
            HomeGrid aiHomeGrid = new HomeGrid(Playertype.AI);

            //Create and initialize radar Grids in which each player can 
            EnemyGrid playerEnemyGrid = new EnemyGrid(Playertype.Human);
            EnemyGrid AIEnemyGrid = new EnemyGrid(Playertype.AI);

            //fleet deployment (currently automatic)
			Fleet playerFleet = new Fleet();
            Fleet aiFleet = new Fleet();
            playerHomeGrid.DeployFleet(playerFleet);
            aiHomeGrid.DeployFleet(aiFleet);
            bool forfeit=false;

            // subscribe to fire-exchange events
            playerEnemyGrid.AimFireEvent += aiHomeGrid.ReceiveShot;
            AIEnemyGrid.AimFireEvent += playerHomeGrid.ReceiveShot;

            //Main game phase
            do
            {
                forfeit = playerEnemyGrid.AimAndFire();  
                if (Winner == Winner.undetermined && !forfeit) AI.Turn(AIEnemyGrid);
  
            } while (Winner == Winner.undetermined && !forfeit);

            //End of the game
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
        /// <summary>
        /// Determines the winner by being told who has lost
        /// </summary>
        /// <param name="loser">The player who lost THE GAME</param>
        /// <returns></returns>
        public static Winner DetermineWinner(Playertype loser)
        { 
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
