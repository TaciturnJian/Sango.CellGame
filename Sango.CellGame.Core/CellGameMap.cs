namespace Sango.CellGame.Core
{
    public class CellGameMap
    {
        /// <summary>
        /// 没有边界的尺寸（边界尺寸为1）
        /// </summary>
        public MapSize GameMapSize { get; private set; }

        public MapSize ActualGameMapSize { get; private set; }

        private bool[,] Map;

        public void ClearMap()
        {
            for (var i = 0; i < ActualGameMapSize.Width; i++)
            {
                for (var j = 0; j < ActualGameMapSize.Height; j++)
                {
                    Map[i, j] = false;
                }
            }
        }

        private void ClearBoundary()
        {
            for (var i = 0; i < ActualGameMapSize.Height; i++)
            {
                Map[0, i] = false;
                Map[ActualGameMapSize.Width - 1, i] = false;
            }

            for (var j = 0; j < ActualGameMapSize.Width; j++)
            {
                Map[j, 0] = false;
                Map[j, ActualGameMapSize.Height - 1] = false;
            }
        }

        public void SetFrame(MapSize sizeWithoutBoundary, bool[,] frame)
        {
            GameMapSize = sizeWithoutBoundary;
            ActualGameMapSize = new MapSize(sizeWithoutBoundary.Width + 2, sizeWithoutBoundary.Height + 2);
            Map = frame;
            ClearBoundary();
        }

        public void InitializeEmptyGameMap(MapSize sizeWithoutBoundary) => SetFrame(sizeWithoutBoundary, new bool[sizeWithoutBoundary.Width + 2, sizeWithoutBoundary.Height + 2]);


        private static readonly int[] DistanceAround = { -1, 0, 1 };

        public int CalculateCellCountAround(MapPoint position)
        {

            var actualPosition = TransformToActualPoint(position);
            if (actualPosition == null)
            {
                return 0;
            }

            var p = (MapPoint)actualPosition;

            return (from i in DistanceAround
                from j in DistanceAround
                where !(i == 0 && j == 0)
                where Map[p.X + i, p.Y + j]
                select i).Count();
        }

        private MapPoint? TransformToActualPoint(MapPoint mapPoint)
        {
            if (!mapPoint.IsInMap(GameMapSize))
            {
                return null;
            }

            return new MapPoint(mapPoint.X + 1, mapPoint.Y + 1);
        }

        public bool[,] CalculateNextFrame()
        {
            var next = new bool[ActualGameMapSize.Width, ActualGameMapSize.Height];
            for (var x = 0; x < GameMapSize.Width; x++)
            {
                for (var y = 0; y < GameMapSize.Height; y++)
                {
                    next[x + 1, y + 1] = GetIsAliveInNextFrame(new MapPoint(x, y));
                }
            }

            return next;
        }

        public bool GetIsAliveInNextFrame(MapPoint position)
        {
            if (!position.IsInMap(GameMapSize))
            {
                return false;
            }

            var count = CalculateCellCountAround(position);
            if (count != 0)
            {
                Console.WriteLine($"({position.X},{position.Y}): {count}");
            }

            return CalculateCellCountAround(position) switch
            {
                3 => true,
                2 => Map[position.X + 1, position.Y + 1],
                _ => false,
            };
        }

        public bool this[int x, int y]
        {
            get => Map[x + 1, y + 1];
            set => Map[x + 1, y + 1] = value;
        }

        public bool this[MapPoint p]
        {
            get => Map[p.X + 1, p.Y + 1];
            set => Map[p.X + 1, p.Y + 1] = value;
        }

        public bool[,] CopyMap()
        {
            var map = new bool[ActualGameMapSize.Width, ActualGameMapSize.Height];
            for (var x = 0; x < GameMapSize.Width; x++)
            {
                for (var y = 0; y < GameMapSize.Height; y++)
                {
                    map[x + 1, y + 1] = Map[x + 1, y + 1];
                }
            }
            return map;
        }
    }
}