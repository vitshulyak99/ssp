using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SSP
{
    public class Machine
    {
        private readonly byte[] _key;
        private byte[] _hmac;
        public Machine(int keyLength = 128)
        {
            _key = new byte[keyLength];
        }

        public string Hmac => _hmac.ToStringX();

        public string HmacKey => _key.ToStringX();

        private void GenerateKey()
        {
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_key);
        }

        private static byte[] HmacGen(byte[] bytes1, byte[] bytes2)
        {
            using var hmac = new HMACSHA256(bytes1);
            return hmac.ComputeHash(bytes2);
        }

        public int Move(IEnumerable<string> choices)
        {
            var step = new Random().Next(0, choices.Count() - 1);
            var bytes = Encoding.UTF8.GetBytes(choices.ElementAt(step));
            GenerateKey();
            _hmac = HmacGen(_key, bytes);
            return step;
        }
    }

    public static class BytesToStringXExtension
    {
        public static string ToStringX(this IEnumerable<byte> @this) => string.Join("", @this.Select(b => b.ToString("X")));
    }
}
