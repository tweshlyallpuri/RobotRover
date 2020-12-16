using System;
using System.Diagnostics.CodeAnalysis;

namespace RobotRover
{    
    public class PositionVector : IEquatable<PositionVector>
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public Direction FacingDirection { get; set; }

        public bool Equals([AllowNull] PositionVector other)
        {
            return other != null && XCoordinate == other.XCoordinate && YCoordinate == other.YCoordinate && this.FacingDirection == other.FacingDirection;
        }

        public (int,int) GetResultantCoordinates(int displacementInFacingDirection)
        {
            int x = XCoordinate;
            int y = YCoordinate;
            switch(FacingDirection)
            {
                case Direction.North:
                    y = YCoordinate + displacementInFacingDirection;
                    break;
                case Direction.South:
                    y = YCoordinate - displacementInFacingDirection;
                    break;
                case Direction.East:
                    x = XCoordinate + displacementInFacingDirection;
                    break;
                case Direction.West:
                    x = XCoordinate - displacementInFacingDirection;
                    break;
            }
            return (x,y);
        }
        public override string ToString()
        {
            //return $"({XCoordinate},{YCoordinate})| Facing {Enum.GetName(typeof(Direction), FacingDirection)}";
            return $"[{XCoordinate},{YCoordinate},{Enum.GetName(typeof(Direction), FacingDirection)}]";
        }
    }
}
