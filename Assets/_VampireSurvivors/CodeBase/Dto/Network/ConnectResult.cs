namespace _VampireSurvivors.CodeBase.Dto.Network
{
    public class ConnectResult
    {
        public readonly bool Success;
        public readonly string ErrorMessage;

        public ConnectResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
