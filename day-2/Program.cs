// See https://aka.ms/new-console-template for more information

const string RED = "red";
const string BLUE = "blue";
const string GREEN = "green";
var colors = new[] { RED, BLUE, GREEN };

var input = File.ReadAllLines("input.txt");

var sumPossibleGameId = 0;
var sumPowerOfMinSet = 0;
var criteria = new Dictionary<string, int>()
{
    { RED, 12 },
    { BLUE, 14 },
    { GREEN, 13 }
};
foreach (var game in input)
{
    var possible = true;
    var gameSplit = game.Split(":");
    var gameNum = Int32.Parse(gameSplit[0].Split(" ")[1]);
    var sets = gameSplit[1].Trim().Split(";");

    var minGameScore = new Dictionary<string, int>()
    {
        { RED, 0 },
        { BLUE, 0 },
        { GREEN, 0 }
    };

    foreach (var set in sets)
    {
        var setScore = new Dictionary<string, int>()
        {
            { RED, 0 },
            { BLUE, 0 },
            { GREEN, 0 }
        };
        var turns = set.Split(",");

        //Calculate score
        foreach (var turn in turns)
        {
            var turnSplit = turn.Trim().Split(" ");
            var num = Int32.Parse(turnSplit[0]);
            var color = turnSplit[1];
            setScore[color] += num;
            
            minGameScore[color] = Math.Max(minGameScore[color], setScore[color]);
        }

        //Check validity
        foreach (var color in colors)
        {
            if (setScore[color] > criteria[color])
                possible = false;
        }
    }
    
    var setPower = minGameScore.Values.Aggregate(1, (current, next) => current * next);
    sumPowerOfMinSet += setPower;

    if (possible)
        sumPossibleGameId += gameNum;
}

Console.WriteLine("{0}", sumPossibleGameId);
Console.WriteLine("{0}", sumPowerOfMinSet);
