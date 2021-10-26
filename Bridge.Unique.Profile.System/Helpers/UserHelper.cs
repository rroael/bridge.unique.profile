using System;

namespace Bridge.Unique.Profile.System.Helpers
{
    public static class UserHelper
    {
        public static int GenerateSmsValidationCode()
        {
            const int min = 1000, max = 9999;
            var random = new Random();

            return random.Next(min, max);
        }

        public static string GenerateNewPassword(int size)
        {
            const string keys = "EFQRxyz12pqrMNOP678klmnoCDYZabcdest90STUVWfghiXAB345GHIJKLjuvw";
            var password = string.Empty;
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var position = random.Next(0, keys.Length - 1);
                password += keys[position];
            }

            return password;
        }

        public static string GenerateNewUserName()
        {
            var randomNumbers = RandomHelper.GetRandomNumbers(24);

            return Convert.ToBase64String(randomNumbers);
        }
    }
}