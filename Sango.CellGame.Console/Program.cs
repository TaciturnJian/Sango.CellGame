
using Sango.CellGame.Core;

var map = new CellGameMap();
map.InitializeEmptyGameMap(GetSize());
InputLoop(map);

static void InputLoop(CellGameMap map)
{
    var frames = new List<bool[,]>();
    while (true)
    {
        map.Print();
        string? input = null;
        while (input == null)
        {
            "\n>>> ".Print();
            input = Console.ReadLine();
        }

        var lowerInput = input.Trim().ToLower();
        switch (lowerInput)
        {
            case "reset size":
                map.InitializeEmptyGameMap(GetSize());
                frames.Clear();
                break;

            case "clear":
                map.ClearMap();
                break;

            case "turn":
                TurnPositionValue(map);
                break;

            case "next":
                frames.Add(map.CopyMap());
                map.SetFrame(map.GameMapSize, map.CalculateNextFrame());
                break;

            case "last":
                map.SetFrame(map.GameMapSize, frames[^1]);
                frames.RemoveAt(frames.Count - 1);
                break;

            case "exit":
            case "quit":
                return;
        }
    }
}

static int InputSizeParameter(string name)
{
    var param = 0;
    while (param <= 0)
    {
        param = ParseInt($"map {name}(>0)");
        if (param <= 0)
        {
            $"Invalid {name}({param}),try again".Print();
        }
    }
    return param;
}

static MapSize GetSize() => new(InputSizeParameter("width"), InputSizeParameter("height"));

static int InputAxisParameter(string name, int top)
{
    var param = -1;
    while (param < 0)
    {
        param = ParseInt($"value {name}({top}>value>=0)");
        if (param < 0 || param >= top)
        {
            $"Invalid {name}({param}),try again".Print();
        }
    }
    return param;
}

static MapPoint GetPosition(MapSize size) => new(InputAxisParameter("x", size.Width), InputAxisParameter("y", size.Height));


static void TurnPositionValue(CellGameMap map)
{
    var position = GetPosition(map.GameMapSize);
    map[position] = !map[position];
}

static int ParseInt(string valueName)
{
    $"Request input for int value({valueName})\n>>> ".Print();
    string? input = null;
    var result = 0;
    while (input == null)
    {
        input = Console.ReadLine();
        while (!int.TryParse(input, out result))
        {
            $"Error when parsing input({input}) for value({valueName})\n please try again\n>>> ".Print();
        }
    }

    return result;
}

public static class PrintExtension
{
    public static void Print(this string message)
    {
        Console.Write(message);
    }

    public static void Print(this CellGameMap map)
    {
        for (var j = 0; j < map.GameMapSize.Height; j++)
        {
            for (var i = 0; i < map.GameMapSize.Width; i++)
            {
                $"{(map[i, j] ? 1: 0)}".Print();
            }
            "\n".Print();
        }
    }
}
