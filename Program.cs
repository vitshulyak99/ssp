using System;
using System.Linq;

namespace SSP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = args[0..];
            if (CheckArgs(a))
            {
                Console.WriteLine("Invalid arguments. Args count must be unique, odd and more or equals 3 ");
                return;
            }
            var game = new Game(a, new Machine());
            game.Start();
        }

        private static bool CheckArgs(string[] args) => args.Length < 3 || args.Length % 2 == 0 || args.GroupBy(x => x).Any(x => x.Count() > 1);
    }
}
