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

        public char XCoordinate { get; }
        public int YCoordinate { get; }
        public bool Hit { get; }
        #endregion

        #region Constructors

        public Position(char x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
            Hit = false;
        }

        public Position(int x, int y)
        {
            XCoordinate = ToCharValue(x);
            YCoordinate = y;
            Hit = false;
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
