using System;
using System.Collections.Generic;
using System.Text;

namespace RoverTheExplorer.Model
{
    public class Direction
    {
        public char Key { get; set; }

        public int Value { get; set; }

        public Coordinate MovementVector { get; set; }
    }
}
