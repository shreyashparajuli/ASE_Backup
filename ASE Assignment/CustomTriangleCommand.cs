using BOOSE;

namespace ASE_Assignment
{
    internal class CustomTriangleCommand : CommandTwoParameters
    {
        public override void Execute()
        {
            base.Execute();

            if (Paramsint.Length < 2 || Paramsint[0] <= 0 || Paramsint[1] <= 0)
            {
                throw new RestrictionException($"Invalid dimensions for triangle: {Paramsint[0]}, {Paramsint[1]}");
            }

            Canvas?.Tri(Paramsint[0], Paramsint[1]); 
        }

        public override void CheckParameters(string[] parameterList)
        {
            if (parameterList.Length < 2)
            {
                throw new CommandException("Triangle command requires two parameters: width and height.");
            }
        }
    }
}
