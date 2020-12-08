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
                Console.WriteLine("0: exit");
                Console.Write("You move: ");
                int move = 0;
                string input;
                while (!(int.TryParse(input = Console.ReadLine(), out move) && ((input  ?? string.Empty).All(char.IsDigit))) || (move < 1 && move > _moves.Count))
                {
                    Console.Write("Invalid move! Try again.\nYou move: ");
                }
                if (move == 0) return;
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
                Console.WriteLine($"Computer move is {_moves[machineMove]}\nHMAC key:{hmacKey}\n");
            } while (isDraw);
        }

        private bool Check(int machineMove,int move)
        {
            var moves = _moves.ToArray();
            var d = moves.Length / 2;
            
            var winningMoves = machineMove + d < moves.Length ? moves[(machineMove + 1)..moves.Length] : moves[(machineMove + 1)..^1].Intersect(moves[..((machineMove + d) - (moves.Length))]);
            return winningMoves.Any(x => x.Equals(_moves[move]));
        }
    }
}
