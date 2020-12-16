using System;
using Xunit;

namespace RobotRover.Tests
{
    public class WhenMovingRover
    {
        [Fact]
        public void Valid_Command_Sequence_Movement_Is_Successful()
        {
            //Arrange
            Rover r = new Rover(new Plateau { Height = 30, Width = 40 });
            string commands = "R1R3L2L1";
            PositionVector expectedResultantPosition = new PositionVector { FacingDirection = Direction.North, XCoordinate = 13, YCoordinate = 8 };
            //Act
            r.Move(commands);
            //Assert
            Assert.True(r.CurrentPosition.Equals(expectedResultantPosition));
        }

        [Fact]
        public void Impermissible_Command_Sequence_Causes_Fall_Off_Plateau()
        {
            //Arrange
            Rover r = new Rover(new Plateau { Height = 30, Width = 40 });
            string commands = "R31";
            PositionVector expectedResultantPosition = null;
            //Act
            r.Move(commands);
            //Assert
            Assert.True(r.CurrentPosition==expectedResultantPosition);

        }
    }
}
