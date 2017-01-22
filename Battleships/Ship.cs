using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public  class Ship
    {
        #region Properties

        public string Name { get; }
        public int Size { get; }
        /// <summary>
        /// Dictionary of squares on <see cref="Board"/> that the ship resides
        /// in and their current hit status
        /// </summary>
        public List<Position> Locations { get; set; }

        public bool isSunk => Locations.Select(location => location.Hit == true).ToList().Count == Size;

        #endregion

        #region Constructors

        public Ship(Tuple<string, int> shipDefinition)
        {
            Name = shipDefinition.Item1;
            Size = shipDefinition.Item2;
        }

        #endregion
    }
}
