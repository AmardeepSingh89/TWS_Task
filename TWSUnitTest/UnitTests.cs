using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TWSAmardeepSingh.Commands;
using TWSAmardeepSingh.Domains;

namespace TWSUnitTest
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
        public void DefaultTest()
        {
            var startingInput = "5 5";
            var startingPositon = Position.ValidCoordinateInput(startingInput);
            var point = new Point(startingPositon.X, startingPositon.Y);

            var directionInput = "1 2 N";
            var moveIput = "LMLMLMLMM";

            using (var client = new Client())
            {
                var direction = Position.ValidDirectionInput(directionInput);
                var ctrl = client.Connect(direction, point);
                var commands = Position.CheckMovementPlan(moveIput);
                ctrl.Execute(commands);

                Assert.AreEqual(ctrl.Position.X, 1);
                Assert.AreEqual(ctrl.Position.Y, 3);
                Assert.AreEqual(ctrl.Direction, Direction.N);
            }
        }

        [TestMethod]
        public void DefaultTest2()
        {
            var startingInput = "5 5";
            var startingPositon = Position.ValidCoordinateInput(startingInput);
            var point = new Point(startingPositon.X, startingPositon.Y);

            var directionInput = "3 3 E";
            var moveIput = "MMRMMRMRRM";

            using (var client = new Client())
            {
                var direction = Position.ValidDirectionInput(directionInput);
                var ctrl = client.Connect(direction, point);
                var commands = Position.CheckMovementPlan(moveIput);
                ctrl.Execute(commands);
 
                Assert.AreEqual(ctrl.Position.X, 5);
                Assert.AreEqual(ctrl.Position.Y, 1);
                Assert.AreEqual(ctrl.Direction, Direction.E);
            }
        }

        [TestMethod]
        public void InvalidStartingPoint()
        {
            AssertThrowException("5 6 7", ValidCheck.StartingPoint);
        }

        [TestMethod]
        public void InvalidDirectInput()
        {
            AssertThrowException("1 2 D", ValidCheck.Direction);
        }

        [TestMethod]
        public void InvalidMovementInput()
        {
            AssertThrowException("FFFGGGFF", ValidCheck.Movement);
        }

        private void AssertThrowException(string input, ValidCheck check)
        {
            try
            {
                switch (check)
                {
                    case ValidCheck.StartingPoint:
                        Position.ValidCoordinateInput(input);
                        break;
                    case ValidCheck.Direction:
                        Position.ValidDirectionInput(input);
                        break;
                    case ValidCheck.Movement:
                        Position.CheckMovementPlan(input);
                        break;
                }

                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }

    public enum ValidCheck
    {
        Direction,
        Movement,
        StartingPoint
    }
}
