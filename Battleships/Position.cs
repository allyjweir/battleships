using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class Position
    {
        #region Properties

        public int XCoordinate { get; }
        public char YCoordinate { get; }
        public PositionStatus Status { get; set; }
        #endregion

        #region Constructors

        public Position(int x, char y)
        {
            XCoordinate = x;
            YCoordinate = y;
            Status = PositionStatus.Untargeted;
        }

        public Position(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = ToCharValue(y);
            Status = PositionStatus.Untargeted;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts human readable grid position (e.g A7) to interger Tuple
        /// for accessing associated position on board (from example tuple would
        /// equal 1,7)
        /// </summary>
        public Tuple<int, int> ToIntegerPosition()
        {
            var yCoordinateAsInteger = char.ToUpper(YCoordinate) -64;
            return new Tuple<int, int>(XCoordinate, yCoordinateAsInteger);
        }

        #endregion

        #region Private Methods

        private char ToCharValue(int coordinate)
            => Convert.ToChar(65 + (coordinate - 1));

        #endregion
    }
}
