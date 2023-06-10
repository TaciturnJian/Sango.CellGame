namespace Sango.CellGame.Core;

public readonly struct MapPoint
{
    public readonly int X;
    public readonly int Y;

    public MapPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool IsInMap(MapSize map)
    {
        return (X >= 0 && X < map.Width) && (Y >= 0 && Y < map.Height);
    }
}