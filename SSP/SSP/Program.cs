using System;
using System.Linq;

namespace SSP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
          
            if (CheckArgs(args))
            {
                Console.WriteLine("Invalid arguments. Args count must be unique, odd and more or equals 3 ");
                return;
            }
            var game = new Game(args, new Machine());
            game.Start();
        }

        private static bool CheckArgs(string[] args) => args.Length < 3 || args.Length % 2 == 0 || args.GroupBy(x => x).Any(x => x.Count() > 1);
    }
}
