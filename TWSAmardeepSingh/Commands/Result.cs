using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh.Commands
{
    public class Result
    {
        public Position Position { get; }
        public Direction Direction { get; }

        public Result(Position pos, Direction dir)
        {
            Position = pos;
            Direction = dir;
        }
    }
}
