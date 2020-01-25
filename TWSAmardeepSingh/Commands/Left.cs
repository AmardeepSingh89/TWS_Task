using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh.Commands
{
    public class Left : ICommand
    {
        public Result Execute(Position position, Direction direction)
        {
            var newDirection = Direction.Unknown;

            switch (direction)
            {
                case Direction.N:
                    newDirection = Direction.W;
                    break;
                case Direction.W:
                    newDirection = Direction.S;
                    break;
                case Direction.S:
                    newDirection = Direction.E;
                    break;
                case Direction.E:
                    newDirection = Direction.N;
                    break;
            }

            return new Result(position, newDirection);
        }
    }
}
