using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace three_n_plus_one
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the 3n+1 solver. You may enter a number e.g. '5', a range e.g. '1-10', or a comma separated list of numbers e.g. '1,3,5' to run through the program. Ctrl+C to quit.");
                var input = Console.ReadLine();
                var numbers = ParseInput(input);
                var results = GetResults(numbers);

                PrintResults(results);
            }
        }
        internal static IEnumerable<long> ParseInput(string input)
        {
            if (input == null)
            {
                return default;
            }
            if (input.Contains("-"))
            {
                var range = input.Split("-");
                //poor mans input validation: Let if fail
                var start = int.Parse(range[0]);
                var end = int.Parse(range[1]);
                //range will only support a range as big as int I guess
                return Enumerable.Repeat(start, (end - start) + 1).Select((a, b) => { return (long)a + b; });
            }
            if (input.Contains(","))
            {
                return input.Split(',')
                    .Select((p) => { return long.Parse(p); });
            }

            if (long.TryParse(input, out long val))
            {
                return new long[] { val };
            }
            throw new InvalidOperationException();
        }
        internal static IEnumerable<Result> GetResults(IEnumerable<long> numbers)
        {
            ConcurrentBag<Result> bag = new ConcurrentBag<Result>();
            Parallel.ForEach(numbers, (number) =>
            {
                var result = ThreeNPlu1(number);
                bag.Add(result);
            });

            return bag.ToList();
        }
        internal static Result ThreeNPlu1(long number)
        {

            //if hte input number is odd multiply by 3 and add one. 


            var result = new Result()
            {
                Subject = number
            };
            do
            {
                result.Hailstones.Add(number);

                number = (number % 2) switch
                {
                    0 => (number / 2),
                    _ => ((number * 3) + 1)
                };


                result.Steps++;

                if (number > result.Peak)
                {
                    result.Peak = number;
                }
            }
            while (number != 1);

            return result;
        }
        internal static void PrintResults(IEnumerable<Result> results)
        {
            foreach (var item in results)
            {
                Console.WriteLine($"Results for: {item.Subject:N0}:: Steps: {item.Steps:N0} Peak: {item.Peak:N0}");
            }
        }

    }

}
