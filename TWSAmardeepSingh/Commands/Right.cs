
using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh.Commands
{
    public class Right : ICommand
    {
        public Result Execute(Position currentPos, Direction currentDir)
        {
            var newDirection = Direction.Unknown;

            switch (currentDir)
            {
                case Direction.N:
                    newDirection = Direction.E;
                    break;
                case Direction.W:
                    newDirection = Direction.N;
                    break;
                case Direction.S:
                    newDirection = Direction.W;
                    break;
                case Direction.E:
                    newDirection = Direction.S;
                    break;
            }

            return new Result(currentPos, newDirection);
        }
    }
}
