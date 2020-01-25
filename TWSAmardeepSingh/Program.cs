using System;
using TWSAmardeepSingh.Commands;
using TWSAmardeepSingh.Domains;

namespace TWSAmardeepSingh
{
    class Program
    {
        static void Main(string[] args)
        {
            var positionInput = Position.StartingPoint();
            var point = new Point(positionInput.X, positionInput.Y);

            using (var client = new Client())
            {
                var whileContinue = true;
                while (whileContinue)
                {
                    var conn = Position.GetInput(validation: point.IsValidPosition);
                    var ctrl = client.Connect(conn, point);
                    var commands = Position.GetPositionPlan(ctrl.Id);

                    ctrl.Execute(commands);
                    Console.WriteLine(ctrl.Position.X + " " +ctrl.Position.Y  + " " + ctrl.Direction);

                    whileContinue = KeepContinue();
                }
            }

            Console.WriteLine("Complete!");
        }

        public static bool KeepContinue()
        {
            Console.WriteLine("Carry on? : Y/N");
            var answer = Console.ReadLine().Trim().ToLower();

            if (answer == "y")
                return true;
            else if (answer == "n")
                return false;
            else
                Console.WriteLine("Y or N input only. Try again.");
                return KeepContinue();
        }

    }
}
