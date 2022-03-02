using PlutoRover.Api.Enums;
using PlutoRover.Api.Interface;
using System;
using System.Collections.Generic;

namespace PlutoRover.Api.Models.Rover
{
    public class Rover : IRover
    {
        private IDictionary<Direction, (Direction left, Direction right)> Directions { get; } =
             new Dictionary<Direction, (Direction left, Direction right)>
             {
                {Direction.North, (Direction.West, Direction.East)},
                {Direction.East, (Direction.North, Direction.South)},
                {Direction.South, (Direction.East, Direction.West)},
                {Direction.West, (Direction.South, Direction.North)},
             };

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Direction Direction { get; set; }

        public IMap Map { get; set; }

        public void Move(RoverCommand command)
        {
            switch (command)
            {
                case RoverCommand.F:
                    MoveForward();
                    break;
                case RoverCommand.B:
                    MoveBackwards();
                    break;
                case RoverCommand.L:
                    TurnLeft();
                    break;
                case RoverCommand.R:
                    TurnRight();
                    break;
                default:
                    throw new Exception($"{command} is not a valid robot command");
            }
        }

        private void MoveForward()
        {
            switch (Direction)
            {
                case Direction.North:
                    Y = LimitValues(++Y, Axis.Y);
                    break;
                case Direction.East:
                    X = LimitValues(++X, Axis.X);
                    break;
                case Direction.South:
                    Y = LimitValues(--Y, Axis.Y);
                    break;
                case Direction.West:
                    X = LimitValues(--X, Axis.X);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveBackwards()
        {
            switch (Direction)
            {
                case Direction.North:
                    Y = LimitValues(--Y, Axis.Y);
                    break;
                case Direction.East:
                    X = LimitValues(--X, Axis.X);
                    break;
                case Direction.South:
                    Y = LimitValues(++Y, Axis.Y);
                    break;
                case Direction.West:
                    X = LimitValues(++X, Axis.X);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TurnLeft()
        {
            Direction = Directions[Direction].left;
        }

        private void TurnRight()
        {
            Direction = Directions[Direction].right;
        }

        private int LimitValues(int value, Axis axis = Axis.X)
        {
            if (value < 0)
            {
                return 0;
            }

            return Math.Min(value, axis == Axis.X ? Map.MaxX : Map.MaxY);
        }
    }
}
