using System;

namespace Battleships
{
    public class Position
    {
        #region Properties

        public char XCoordinate { get; }
        public int YCoordinate { get; }
        public PositionStatus Status { get; set; }
        #endregion

        #region Constructors

        public Position(char x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
            Status = PositionStatus.Untargeted;
        }

        public Position(int x, int y)
        {
            XCoordinate = ToCharValue(x);
            YCoordinate = y;
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
            var xCoordinateAsInteger = char.ToUpper(XCoordinate) -64;
            return new Tuple<int, int>(xCoordinateAsInteger, YCoordinate);
        }

        #endregion

        #region Private Methods

        private char ToCharValue(int coordinate)
            => Convert.ToChar(65 + (coordinate - 1));

        #endregion
    }
}
