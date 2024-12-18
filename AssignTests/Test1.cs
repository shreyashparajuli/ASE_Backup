using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASE_Assignment;
using BOOSE;

namespace AssignTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Moveto_ShouldUpdatePenPosition()
        {
            // Arrange
            var canvas = new CustomCanvas();

            // Act
            canvas.MoveTo(150, 250);

            // Assert
            Assert.AreEqual(150, canvas.Xpos, "Xpos was not updated correctly.");
            Assert.AreEqual(250, canvas.Ypos, "Ypos was not updated correctly.");
        }

        [TestMethod]
        public void DrawTo_ShouldUpdatePenPosition()
        {
            // Arrange
            var canvas = new CustomCanvas();
            canvas.Set(100, 100); // Start position

            // Act
            canvas.DrawTo(200, 300);

            // Assert
            Assert.AreEqual(200, canvas.Xpos, "Xpos was not updated correctly after DrawTo.");
            Assert.AreEqual(300, canvas.Ypos, "Ypos was not updated correctly after DrawTo.");
        }

        [TestMethod]
        public void MultilineProgram_ShouldExecuteAllCommandsCorrectly()
        {
            // Arrange
            var canvas = new CustomCanvas();
            var factory = new CommandFactory();
            var program = new StoredProgram(canvas);
            var parser = new Parser(factory, program);

            string script = "moveto 100,100\ndrawto 200,200\ncircle 50";

            // Act
            parser.ParseProgram(script);
            program.Run();

            // Assert
            Assert.AreEqual(200, canvas.Xpos, "Xpos was not updated correctly after program execution.");
            Assert.AreEqual(200, canvas.Ypos, "Ypos was not updated correctly after program execution.");
        }
    }
}
