using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TWSAmardeepSingh.Commands;

namespace TWSAmardeepSingh.Domains
{
    public class Position : Tuple<int, int>, IComparable<Position>
    {
        public int X { get { return Item1; } }
        public int Y { get { return Item2; } }

        public Position(int x, int y) : base(x, y)
        {
        }

        public int CompareTo(Position other)
        {
            if (X == other.X && Y == other.Y)
                return 0;

            return -1;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public Position Right { get { return new Position(X + 1, Y); } }

        public Position Left { get { return new Position(X - 1, Y); } }

        public Position Up { get { return new Position(X, Y + 1); } }

        public Position Down { get { return new Position(X, Y - 1); } }


        public static Position StartingPoint()
        {
            var newPosition = new Position(-1, -1);
            try
            {
                Console.WriteLine("Enter starting position:");
                var input = Console.ReadLine();
                newPosition = ValidCoordinateInput(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StartingPoint();
            }

            return newPosition;
        }

        public static Position ValidCoordinateInput(string input)
        {
            var regex = new Regex(@"^(?=.*\d)[\d\s]{3}$");

            if (!regex.IsMatch(input.Trim()))
                throw new Exception(
                    "Must only contain numbers with space between numbers. (i.e. 5 5)"
                );

            return new Position(input.Trim().First(), input.Trim().Last()) ;
        }

        public static IConnection GetInput(Func<int, int, bool> validation)
        {
            Console.Write("Enter direction:");
            var input = Console.ReadLine();

            IConnection connInfo;

            try
            {
                connInfo = ValidDirectionInput(input.Trim().ToLower());

                if (!validation(connInfo.InitialPosition.Item1, connInfo.InitialPosition.Item2))
                    throw new Exception("Location was not on the map.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return GetInput(validation);
            }

            return connInfo;
        }

        public static IConnection ValidDirectionInput(string input)
        {
            var lowerCaseInput = input.ToLower().Trim();
            var regex = new Regex(@"^[\d\s]{4}[nsew]$");

            if (!regex.IsMatch(lowerCaseInput))
                throw new Exception(
                    "First 2 inputs must be numbers with N, S, E, or W at end of input. (i.e. 4 5 N)"
                );

            Direction facing = Direction.Unknown;
            switch (lowerCaseInput.Last().ToString())
            {
                case "n":
                    facing = Direction.N;
                    break;
                case "s":
                    facing = Direction.S;
                    break;
                case "e":
                    facing = Direction.E;
                    break;
                case "w":
                    facing = Direction.W;
                    break;
            }

            if (facing == Direction.Unknown)
                Console.WriteLine("Cannot detect direction.");

            return new RoverCommand(new Position(int.Parse(input[0].ToString()), int.Parse(input[2].ToString())), facing);
        }

        public static Queue<ICommand> GetPositionPlan(Guid id)
        {
            Console.Write("Enter movement:");
            var input = Console.ReadLine();

            try
            {
                return CheckMovementPlan(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return GetPositionPlan(id);
            }
        }

        public static Queue<ICommand> CheckMovementPlan(string input)
        {
            var lowerInput = input.Trim().ToLower();

            string allowableLetters = "mrl";

            foreach (char c in lowerInput)
            {
                if (!allowableLetters.Contains(c.ToString()))
                    throw new Exception(
                        "Only contain L, R, and M. (i.e. LRMLRM). Please try again"
                    );
            }

            var commands = new Queue<ICommand>();

            foreach (var c in lowerInput)
            {
                switch (c)
                {
                    case 'l':
                        commands.Enqueue(new Left());
                        break;
                    case 'r':
                        commands.Enqueue(new Right());
                        break;
                    case 'm':
                        commands.Enqueue(new Move());
                        break;
                }
            }

            return commands;
        }
    }
}
