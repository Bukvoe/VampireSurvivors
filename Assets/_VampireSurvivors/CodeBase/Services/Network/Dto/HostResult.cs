namespace _VampireSurvivors.CodeBase.Services.Network.Dto
{
    public class HostResult
    {
        public readonly bool Success;
        public readonly string ErrorMessage;

        public HostResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
