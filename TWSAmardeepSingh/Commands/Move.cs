using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh.Commands
{
    public class Move : ICommand
    {
        public Result Execute(Position currentPos, Direction currentDir)
        {
            Position pos = new Position(-1, -1);

            switch (currentDir)
            {
                case Direction.N:
                    pos = currentPos.Up;
                    break;

                case Direction.S:
                    pos = currentPos.Down;
                    break;

                case Direction.W:
                    pos = currentPos.Left;
                    break;

                case Direction.E:
                    pos = currentPos.Right;
                    break;
            }

            return new Result(pos, currentDir);
        }
    }
}
