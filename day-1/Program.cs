// See https://aka.ms/new-console-template for more information
//URL: https://adventofcode.com/2023/day/1

var input = File.ReadAllLines("input.txt");
HashSet<char> numbers = new() { '1', '2', '3', '4', '5', '6', '7', '8', '9'};
(string Text, int Value)[] numbersAsString =
{
    ("one", 1), ("two", 2), ("three", 3), ("four", 4), ("five", 5), ("six", 6), ("seven", 7), ("eight", 8), ("nine", 9),
};
HashSet<char> potentialNumber = numbersAsString.Select(x => x.Text[0]).ToHashSet();

var sum = 0;
foreach (var line in input)
{
    int? first = null;
    int? last = null;
    var span = line.AsSpan();
    var lineLength = span.Length;
    for (var i = 0; i < span.Length; i++)
    {
        var character = span[i];
        if (numbers.Contains(character))
        {
            if (first.HasValue)
                last = Int32.Parse(character.ToString());
            else
                first = Int32.Parse(character.ToString());
        }
        else if (potentialNumber.Contains(character))
        {
            foreach (var number in numbersAsString)
            {
                if (number.Text.AsSpan().SequenceEqual(span.Slice(i, Math.Min(lineLength - i, number.Text.Length))))
                {
                    if (first.HasValue)
                        last = number.Value;
                    else
                        first = number.Value;
                    break;
                }
            }
        }
    }

    sum += 10 * first.Value + (last ?? first.Value);
}

Console.WriteLine(sum);