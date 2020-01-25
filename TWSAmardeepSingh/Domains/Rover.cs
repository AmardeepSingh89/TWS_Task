using System;
using System.Collections.Generic;
using System.Threading;
using TWSAmardeepSingh.Commands;

namespace TWSAmardeepSingh.Domains
{
    public class Rover : IController
    {
        public Guid Id { get; private set; }
        public Direction Direction { get; set; }
        public Position Position { get; set; }
        public Point Point { get; set; }

        public Rover(Position initialPosition, Direction dir, Point env)
        {
            Id = Guid.NewGuid();
            Position = initialPosition;
            Direction = dir;
            Point = env;
            Point.SetOccupant(Position, Id);
        }

        public void Execute(Queue<ICommand> commands)
        {
            while (commands.Count > 0)
            {
                Thread.Sleep(100);
                var cmd = commands.Dequeue();
                var result = cmd.Execute(Position, Direction);

                var onMap = Point.IsValidPosition(result.Position.X, result.Position.Y);
                var available = Point.IsSelfOrVacant(Id, result.Position.X, result.Position.Y);

                if (onMap && available)
                {
                    Move(new Position(result.Position.X, result.Position.Y));
                    Rotate(result.Direction);
                }
                else
                {
                    Console.WriteLine("Not available");
                }
            }
        }

        private void Move(Position destination)
        {
            var from = Position;
            var to = destination;

            if (from.CompareTo(to) != 0) // different points
            {
                var success = Point.Move(Id, from, to);
                if (success)
                {
                    Position = to;
                }
                else
                {
                    Console.WriteLine("Unsuccessful");
                }

            }
        }

        private void Rotate(Direction dir)
        {
            if (Direction != dir)
            {
                Direction = dir;
            }
        }
    }
}
