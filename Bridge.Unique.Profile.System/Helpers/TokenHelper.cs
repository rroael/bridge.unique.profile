using System;

namespace Bridge.Unique.Profile.System.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateBupToken()
        {
            var randomNumbers = RandomHelper.GetRandomNumbers(40);

            return Convert.ToBase64String(randomNumbers);
        }
    }
}