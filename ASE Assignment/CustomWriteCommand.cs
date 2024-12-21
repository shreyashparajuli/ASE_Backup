using BOOSE;


namespace ASE_Assignment
{
    internal class CustomWriteCommand : CommandOneParameter
    {
        public override void Execute()
        {
            base.Execute();
            if (Canvas != null)
            {
                base.Canvas.WriteText(base.Parameters[0]); 
            }
        }

        public override void CheckParameters(string[] parameterList)
        {
            if (parameterList.Length < 1)
            {
                throw new CommandException("Text command requires at least one parameter.");
            }
        }
    }
}
