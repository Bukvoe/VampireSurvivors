using System;
using Fusion;

namespace _VampireSurvivors.CodeBase.Common.Extensions
{
    public static class StartGameResultExtensions
    {
        public static string ToErrorMessage(this StartGameResult startGameResult)
        {
            return $"{startGameResult.ShutdownReason}{Environment.NewLine}{startGameResult.ErrorMessage}";
        }
    }
}
