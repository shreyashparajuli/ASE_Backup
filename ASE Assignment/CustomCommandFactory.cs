using System;
using BOOSE;

namespace ASE_Assignment
{
    public class CustomCommandFactory : CommandFactory
    {
        public override ICommand MakeCommand(string commandType)
        {
            commandType = commandType.ToLower().Trim();

            if (commandType == "triangle")
                return new CustomTriangleCommand();
            if (commandType == "text")
                return new CustomWriteCommand();

            return base.MakeCommand(commandType);
        }
    }
}
