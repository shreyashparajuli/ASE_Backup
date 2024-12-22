using BOOSE;

namespace ASE_Assignment
{
    /// <summary>
    /// A custom command for drawing a triangle on the canvas.
    /// Inherits from <see cref="CommandTwoParameters"/> to handle width and height parameters.
    /// </summary>
    internal class CustomTriangleCommand : CommandTwoParameters
    {
        /// <summary>
        /// Executes the triangle-drawing command using the provided width and height parameters.
        /// </summary>
        /// <exception cref="RestrictionException">
        /// Thrown if the triangle dimensions (width or height) are invalid (e.g., non-positive).
        /// </exception>
        public override void Execute()
        {
            // Invoke the base class execution logic, which parses parameters.
            base.Execute();

            if (base.Paramsint.Length < 2 || base.Paramsint[0] <= 0 || base.Paramsint[1] <= 0)
            {
                throw new RestrictionException($"Invalid dimensions for triangle: {base.Paramsint[0]}, {base.Paramsint[1]}");
            }

            base.Canvas.Tri(base.Paramsint[0], base.Paramsint[1]);
        }

        /// <summary>
        /// Ensures the command parameters are valid for the triangle command.
        /// </summary>
        /// <param name="parameterList">An array containing the width and height as strings.</param>
        /// <exception cref="CommandException">
        /// Thrown if fewer than two parameters are provided.
        /// </exception>
        public override void CheckParameters(string[] parameterList)
        {
            if (parameterList.Length < 2)
            {
                throw new CommandException("Triangle command requires two parameters: width and height.");
            }
        }
    }
}
