﻿using System;

namespace Battleships
{
    public class Player
    {
        #region Properties

        public string Name { get; }
        public Board GameBoard { get; }

        #endregion

        #region Constructors

        public Player(string name)
        {
            Name = name;
            GameBoard = new Board();
        }

        #endregion
    }
}
