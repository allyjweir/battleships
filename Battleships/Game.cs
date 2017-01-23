using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    /// <summary>
    /// Overarching class that controls the whole game.
    /// 
    /// The key aim of this class is to control the course of the game while
    /// keeping all rendering logic in this class rather than having it spread
    /// throughout all the classes. Keeping the implementation of game logic
    /// pure and then adding rendering on top. Separation of concerns.
    /// </summary>
    class Game
    {
        #region Properties

        private readonly int _defaultPlayerCount = 2;
        private readonly int _defaultShotsPerTurn = 1;
        public IList<Player> Players { get; }
        public int ShotsPerTurn { get; }

        #endregion

        #region Constructors

        public Game()
        {
            ShotsPerTurn = _defaultShotsPerTurn;
            DisplayIntroduction();
            AddPlayers(_defaultPlayerCount);
            Play();
        }
        #endregion

        #region Private Methods
        private void DisplayIntroduction()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Battleships!");
            Console.WriteLine("Let's get this game started. Press any key to start.");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerCount"></param>
        private void AddPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.Clear();
                Console.WriteLine($"Hi Player {i+1}. What's your name? (Type it then press Enter): ");
                var name = Console.ReadLine();
                Players.Add(new Player(name));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Play()
        {
            // While no one has been sunk
            while (!Players.Any(player => player.GameBoard.IsFleetSunk == false))
            {
                foreach (var player in Players)
                {
                    TakeTurn(player);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        private void TakeTurn(Player player, Player opponent)
        {
            Console.Clear();
            Console.WriteLine($"{player.Name}, it's your turn to strike.");
            Console.WriteLine($"Here is your tracking board (where you have struck in your enemies' territory so far):");
            player.GameBoard.RenderTrackingBoard();
            Console.WriteLine();
            Console.WriteLine($"Here is your board:");
            player.GameBoard.RenderBoard();
            Console.WriteLine();
            for (int i = 0; i < ShotsPerTurn; i++)
            {
                TakeShot(player.GameBoard, opponent);
            }

        }

        /// <param name="tries">Player gets limited chances to make valid shot</param>
        private void TakeShot(Board board, Player opponent, int tries = 3)
        {
            Console.WriteLine("What do you want to target now? (Use format: A7 for Row 1, column 7");
            var target = Console.ReadLine();

            if(board.isValidShot(target))
            {
                var validTarget = new Position(target.Substring(0, 1).ToCharArray()[0],
                                                        int.Parse(target.Substring(1, 1)));
                if (opponent.TakeFire(validTarget))
                {
                    validTarget.Status = PositionStatus.Hit;
                    Console.WriteLine("Successful hit!");
                } else
                {
                    Console.WriteLine("Miss!");
                }
                board.TargetedPositions.Add(validTarget);
            } else
            {
                Console.WriteLine("Invalid Target. Please take your shot again. ({tries} left)");
                if (tries > 1)
                {
                    TakeShot(board, opponent, tries - 1);
                }
                Console.WriteLine("Too many invalid attempts. You miss this turn!");
            }
        }

        #endregion
    }
}
