using System;
using System.Diagnostics;
using BOOSE;

namespace ASE_Assignment
{
    public class CustomCommandFactory : CommandFactory
    {
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

                return base.MakeCommand(commandType);
            }
            catch (FactoryException ex)
            {
                
                Debug.WriteLine($"FactoryException caught: {ex.Message}");
                throw new FactoryException($"No such command '{commandType}'. " +
                                           $"Please check the syntax.");
            }
        }
    }
}

