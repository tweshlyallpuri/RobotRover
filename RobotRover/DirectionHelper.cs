using System;

namespace RobotRover
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    static public class DirectionExtensions
    {
        static public Direction GetChangedDirection(this Direction d, char directionCode)
        {
            if (directionCode == 'L')
            {
                if (d != Direction.North)
                    return d - 1;
                else
                    return  Direction.West;
            }
            else if (directionCode == 'R')
            {
                if (d != Direction.West)
                    return  d + 1;
                else
                    return  Direction.North;
            }
            throw new Exception("Unknown DirectionChangeCode!");
        }
    }
}
