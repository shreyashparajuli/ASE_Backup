using System;
using System.Diagnostics;
using BOOSE;

namespace ASE_Assignment
{
    /// <summary>
    /// A custom command factory extending <see cref="CommandFactory"/> to provide additional commands 
    /// (e.g., "tri" and "write"). Unrecognized commands are passed to the base factory or result in an exception.
    /// </summary>
    public class CustomCommandFactory : CommandFactory
    {
        /// <summary>
        /// Creates a command object based on the specified <paramref name="commandType"/>.
        /// Supports "tri" and "write" commands as custom commands, and defers unknown commands
        /// to the base factory.
        /// </summary>
        /// <param name="commandType">The string representing the type of command to create.</param>
        /// <returns>An <see cref="ICommand"/> instance corresponding to the requested command.</returns>
        /// <exception cref="FactoryException">Thrown when the command type is not recognized 
        /// by either this factory or the base factory.</exception>
        public override ICommand MakeCommand(string commandType)
        {
            commandType = commandType.ToLower().Trim();

            try
            {
                if (commandType == "tri")
                {
                    return new CustomTriangleCommand();
                }

                if (commandType == "write")
                {
                    return new CustomWriteCommand();
                }

                // If not recognized by this custom factory, defer to the base factory.
                return base.MakeCommand(commandType);
            }
            catch (FactoryException ex)
            {
                Debug.WriteLine($"FactoryException caught: {ex.Message}");
                throw new FactoryException($"No such command '{commandType}'. " +
                                           "Please check the syntax.");
            }
        }
    }
}
