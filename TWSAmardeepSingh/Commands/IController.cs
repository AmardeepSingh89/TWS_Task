using System;
using System.Collections.Generic;
using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh.Commands
{
    public interface IController
    {
        Guid Id { get; }
        Position Position { get; set; }
        Direction Direction { get; set; }

        Point Point { get; set; }

        void Execute(Queue<ICommand> commands);
    }
}
