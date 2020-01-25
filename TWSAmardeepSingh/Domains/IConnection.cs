using System;
namespace TWSAmardeepSingh.Domains
{
    public interface IConnection
    {
        ConnectionTypes Entity { get; }
        Position InitialPosition { get; }
        Direction Direction { get; }
    }

    public enum ConnectionTypes
    {
        Rover
    }

    public enum Direction
    {
        Unknown,
        N,
        S,
        W,
        E
    }
}
