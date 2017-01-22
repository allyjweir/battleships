using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class Board
    {
        #region Properties

        /// <summary>
        /// Values used to seed default <see cref="Ship"/> objects associated with a board
        /// </summary>
        private readonly List<Tuple<string, int>> _defaultShipsDefinitions = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("Carrier", 5),
            new Tuple<string, int>("Battleship", 4),
            new Tuple<string, int>("Cruiser", 3),
            new Tuple<string, int>("Submarine", 3),
            new Tuple<string, int>("Destroyer", 2)
        };

        /// <summary>
        /// Provides default X dimension to be used to construct the board's layout.
        /// 
        /// Battleships is played on a square grid, typically 10x10. Use of 
        /// default value offers simpler constructor for use in 
        /// <see cref="Game"/> while allowing alternative flexible constructors
        /// to define alternative board dimensions.
        /// </summary>
        private readonly int _defaultXDimension = 10;
        private readonly int _defaultYDimension = 10;

        public int XDimension { get; }
        public int YDimension { get; }

        /// <summary>
        /// List of currently positioned <see cref="Ship"/> on the board
        /// </summary>
        public List<Ship> Fleet { get; }

        /// <summary>
        /// Have all ships in the Fleet been sunk, indicating the end of the
        /// game?
        /// 
        /// The win condition as defined in the README is that 17 successful
        /// hits have been made on an enemy's fleet. This, using the default 
        /// ships, equates to all ships having been sunk completely. This 
        /// property 
        /// </summary>
        public bool IsFleetSunk
            => Fleet.Select(ship => ship.isSunk).ToList().Count == Fleet.Count;

        /// <summary>
        /// List of all positions an opponent <see cref="Player"/> has targeted
        /// on the Board and the status of their hit.
        /// 
        /// While hit status is also tracked in <see cref="Ship"/> object, this
        /// list is used to track both successful and failed hits that the
        /// opposing player has made. It can then be used to render the opposing
        /// player's 'tracking' board
        /// </summary>
        public List<Position> TargetedPositions { get; }

        public List<Position> OccupiedPositions
            => Fleet.SelectMany(ship => ship.Locations).ToList();

        #endregion

        #region Constructors

        /// <summary>
        /// Zero-parameter constructor which automatically positions the
        /// default ships on a board with default grid dimensions
        /// </summary>
        public Board()
        {
            XDimension = _defaultXDimension;
            YDimension = _defaultYDimension;
            _defaultShipsDefinitions.ForEach(definition => Fleet.Add(new Ship(definition)));
            Fleet.ForEach(ship => ship.Locations = RandomlyPositionShip(ship.Size));
        }

        #endregion

        #region Public Rendering Methods

        internal void RenderBoard()
        {
            Console.WriteLine(ConstructTopGridMarkers());
            for (int i = 0; i < YDimension; i++)
            {
                Console.WriteLine(ConstructGameBoardRow(i, OccupiedPositions));
            }
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// Returns a List defining a Ship's position on the board randomly
        /// chosen while avoiding already positioned ships.
        /// 
        /// The base version of this game chooses the position of each player's
        /// ships.
        /// 
        /// Ships may be vertically or horizontally aligned within the board's 
        /// grid dimensions
        /// </summary>
        /// <param name="size">Size of ship to be placed.</param>
        private List<Position> RandomlyPositionShip(int size)
        {
            var futureLocation = new List<Position>();
            var random = new Random();
            var row = random.Next(1, YDimension);  // TODO: Make this flexible to position horizontally as well.

            for (int i = 0; i < size; i++)
            {
                futureLocation.Add(new Position(random.Next(1, XDimension), row));
            }
            
            if(isValidPosition(futureLocation))
            {
                return RandomlyPositionShip(size);
            }

            return futureLocation;
        }

        /// <summary>
        /// Check that a List of positions doesn't collide with exisitng
        /// occupied positions
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        private bool isValidPosition(List<Position> locations)
            => !locations.Any(position => OccupiedPositions.Contains(position)); 

        private string ConstructTopGridMarkers()
        {
            var markers = "";
            for (int i = 0; i < XDimension; i++)
            {
                markers += $"{i + 1}\t";
            }
            return markers.Substring(0, markers.Length - 1);  //Remove trailing \t char from string
        }

        /// <summary>
        /// Renders a row of game board.
        /// 
        /// By taking a list of arbitrary positions, this function can render
        /// either the tracking board or a player's own gameboard.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        private string ConstructGameBoardRow(int rowNumber, List<Position> positionsOfInterest)
        {
            var gameRow = "";
            for (int i = 0; i < XDimension; i++)
            {
                // If the position is Untargeted. See PositionStatus enum
                if (!positionsOfInterest.Any(position => position.XCoordinate == i))
                {
                    gameRow += "-\t";
                }
                else
                {
                    var thisPosition = positionsOfInterest.Single(position => position.XCoordinate == i);
                    switch (thisPosition.Status)
                    {
                        case PositionStatus.Hit:
                            gameRow += "H\t";
                            break;
                        case PositionStatus.Missed:
                            gameRow += "M\t";
                            break;
                        default:
                            throw new Exception("Unexpected PositionStatus value when rendering cell on grid.");
                    }
                }
            }
            return gameRow.Substring(0, gameRow.Length - 1);  // Remove trailing \t character
        }

        #endregion

    }
}
