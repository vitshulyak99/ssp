using System;
using System.Collections.Generic;
using System.Linq;

namespace SSP
{
    public class Game
    {
        private readonly List<string> _moves;
        private readonly Machine _machine;


        public Game(IEnumerable<string> moves, Machine machine)
        {
            _moves = new List<string>(moves);
            _machine = machine;
        }

        public void Start()
        {
            var isDraw = true;
            do
            {
                var (machineMove, hmacKey, hmak) = (_machine.Move(_moves), _machine.HmacKey, _machine.Hmac);
                Console.WriteLine($"HMAC:{hmak}\nChoice yuo move");
                _moves.ForEach(x => Console.WriteLine($"{_moves.IndexOf(x) + 1}: {x}"));
                Console.Write("You move: ");
                var move = 0;
                while (int.TryParse(Console.ReadLine(), out move) && move < 1 && move > _moves.Count)
                {
                    Console.WriteLine("Invalid move! Try again.");
                }
                move--;
                isDraw = move.Equals(machineMove);
                if (isDraw)
                {
                    Console.WriteLine("Draw!");
                }
                else
                {
                    Console.WriteLine(Check(machineMove, move) ? $"You win!" : $"You lose!");
                }
                Console.WriteLine($"Computer move is {_moves[machineMove]}\nHMAC key:{hmacKey}\ns");
            } while (isDraw);
        }

        private bool Check(int machineMove,int move)
        {
            var moves = _moves.ToArray();
            var d = moves.Length / 2;
            var winningMoves = machineMove + d < moves.Length ? moves[(machineMove + 1)..moves.Length] : moves[(machineMove + 1)..^1].Intersect(moves[..((machineMove + d + 1) - (moves.Length))]);
            return winningMoves.Any(x => x.Equals(_moves[move]));
        }
    }
}
