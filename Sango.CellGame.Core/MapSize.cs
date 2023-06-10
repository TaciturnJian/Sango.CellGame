namespace Sango.CellGame.Core;

public readonly struct MapSize
{
    public readonly int Width;
    public readonly int Height;

    public MapSize(int width, int height)
    {
        Width = width;
        Height = height;
    }
}