using BOOSE;

namespace ASE_Assignment
{
    /// <summary>
    /// A custom command for writing text on the canvas. Inherits from 
    /// <see cref="CommandOneParameter"/> to handle a single parameter (the text to write).
    /// </summary>
    internal class CustomWriteCommand : CommandOneParameter
    {
        /// <summary>
        /// Executes the text-writing command using the single parameter 
        /// provided (i.e., the text to be drawn on the canvas).
        /// </summary>
        public override void Execute()
        {
            // Calls the base class Execute() to parse and store the parameter.
            base.Execute();

            if (Canvas != null)
            {
                // Writes the first parameter (text) onto the canvas.
                base.Canvas.WriteText(base.Parameters[0]);
            }
        }

        /// <summary>
        /// Checks that at least one parameter (the text) is provided.
        /// </summary>
        /// <param name="parameterList">An array of parameters; 
        /// the first parameter is the text to write.</param>
        /// <exception cref="CommandException">
        /// Thrown if no parameters are provided.
        /// </exception>
        public override void CheckParameters(string[] parameterList)
        {
            if (parameterList.Length < 1)
            {
                throw new CommandException("Text command requires at least one parameter.");
            }
        }
    }
}
