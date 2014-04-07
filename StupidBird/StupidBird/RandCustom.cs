using System;


namespace StupidBird
{
    class RandCustom
    {
        public static Random rand = new Random();
        public static int randInt(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
