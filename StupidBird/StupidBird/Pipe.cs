using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace StupidBird
{
    class Pipe
    {
        public bool Active;
        public Vector2 Position;
        public float SpacePosition;
        public bool CanScore;

        public Pipe()
        {
            CanScore = true;
        }
    }
}
