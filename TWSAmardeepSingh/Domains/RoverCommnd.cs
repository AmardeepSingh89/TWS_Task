using System;
using System.Collections.Generic;
using System.Threading;
using TWSAmardeepSingh.Commands;

namespace TWSAmardeepSingh.Domains
{
    public class RoverCommand : IConnection
    {
        public ConnectionTypes Entity { get; } = ConnectionTypes.Rover;
        public Position InitialPosition { get; }
        public Direction Direction { get; }

        public RoverCommand(Position initialPos, Direction direction)
        {
            InitialPosition = initialPos;
            Direction = direction;
        }
    }
}
