using System;
using System.Linq;
using System.Security.Cryptography;

namespace Bridge.Unique.Profile.System.Helpers
{
    public static class RandomHelper
    {
        public static byte[] GetRandomNumbers(short size)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return randomNumber;
        }

        public static string GetRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}