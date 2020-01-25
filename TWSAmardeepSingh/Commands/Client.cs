using System;
using System.Collections.Generic;
using System.Linq;
using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh.Commands
{
    public class Client : IDisposable
    {
        List<IController> _connections = new List<IController>();

        public IController Connect(IConnection connection, Point point)
        {
            var existngConn = _connections
                .Find(c => c.Position.CompareTo(connection.InitialPosition) == 0);

            if (existngConn != null)
                return existngConn;

            var x = connection.InitialPosition.X;
            var y = connection.InitialPosition.Y;
            var dir = connection.Direction;

            IController ctrl;

            switch (connection.Entity)
            {
                case ConnectionTypes.Rover:
                    ctrl = new Rover(new Position(x, y), dir, point);
                    _connections.Add(ctrl);
                    break;
                default:
                    throw new Exception();
            }

            return ctrl;
        }

        public void Dispose()
        {
            _connections = null;
        }

        public IController GetController(Guid id)
        {
            return _connections.FirstOrDefault(c => c.Id == id);
        }

    }

    public interface ICommand
    {
        Result Execute(Position currentPos, Direction currentDir);
    }
}
