using System;
using System.Collections.Generic;
using System.Text;

namespace RobotRover
{
    public class Rover
    {
        public Plateau CurrentPlateau { get; set; }
        public PositionVector CurrentPosition { get => _currentPosition; set => _currentPosition = value; }
        public Rover(Plateau p)
        {
            CurrentPlateau = p;
            SetPosition(10, 10, "N");
        }

        #region Required Methods
        public void SetPosition(int x, int y, string direction)
        {
            _currentPosition = new PositionVector { XCoordinate = x, YCoordinate = y, FacingDirection = GetDirectionFromString(direction) };
        }
        public void Move(string commands)
        {
            commands = commands.ToUpperInvariant();
            try
            {
                int cursor = 0;
                //iterate through all commands until rover is on the plateau
                while (cursor < commands.Length && _currentPosition != null)
                {
                    char directionCode = commands[cursor];
                    cursor++;//move to next to get displacementValue
                    string displacementString = commands[cursor].ToString();
                    //find out all subsequent integer values for displacement calculation
                    int indexOfPossibleIntValue = cursor + 1;
                    while ((indexOfPossibleIntValue < commands.Length) && commands[indexOfPossibleIntValue] <= '9' && commands[indexOfPossibleIntValue] >= '0')
                    {
                        displacementString += commands[indexOfPossibleIntValue];
                        indexOfPossibleIntValue++;

                    }
                    if (int.TryParse(displacementString, out var displacementValue))
                    {
                        _currentPosition.FacingDirection = _currentPosition.FacingDirection.GetChangedDirection(directionCode);
                        var (x,y) = _currentPosition.GetResultantCoordinates(displacementValue);

                        if (x > CurrentPlateau.Width || y > CurrentPlateau.Height || x < 0 || y < 0)
                            _currentPosition = null;
                        else
                        {
                            _currentPosition.XCoordinate = x;
                            _currentPosition.YCoordinate = y;
                        }
                    }
                    else
                        throw new Exception("Incorrect command string");
                    cursor = indexOfPossibleIntValue;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occured : " + e.Message);
            }
        }
        #endregion
        private Direction GetDirectionFromString(string direction)
        {
            direction = direction.Trim().ToUpperInvariant();
            switch (direction)
            {
                case "NORTH":
                case "N":
                    return Direction.North;
                case "EAST":
                case "E":
                    return Direction.East;
                case "SOUTH":
                case "S":
                    return Direction.South;
                case "WEST":
                case "W":
                    return Direction.West;
            }
            throw new Exception("Please provide valid direction string");
        }
        private PositionVector _currentPosition;
    }
}
