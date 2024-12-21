using BOOSE;

namespace ASE_Assignment
{
    internal class CustomTriangleCommand : CommandTwoParameters
    {
        public override void Execute()
        {
            base.Execute();

            if (base.Paramsint.Length < 2 || base.Paramsint[0] <= 0 || base.Paramsint[1] <= 0)
            {
                throw new RestrictionException($"Invalid dimensions for triangle: {base.Paramsint[0]}, {base.Paramsint[1]}");
            }

            base.Canvas.Tri(base.Paramsint[0], base.Paramsint[1]); 
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
