using System;
using System.Collections.Generic;

namespace TWSAmardeepSingh.Domains
{
    public class Point
    {
        private List<Tile> _tiles = new List<Tile>();
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Point(int width, int height)
        {
            Width = width;
            Height = height;

            for (var x = 0; x <= width; x++)
            {
                for (var y = 0; y <= height; y++)
                {
                    _tiles.Add(new Tile(x, y));
                }
            }
        }

        public bool IsValidPosition(int x, int y)
        {
            return _tiles.Exists((t) =>
            {
                return t.Position.X == x && t.Position.Y == y;
            });
        }

        public bool IsSelfOrVacant(Guid id, int x, int y)
        {
            var target = _tiles.Find(t => t.Position.X == x && t.Position.Y == y);

            if (target == null)
                return false;

            return target.OccupantId == id || target.OccupantId == null;
        }

        public bool Move(Guid id, Position from, Position to)
        {
            var source = _tiles.Find(
                t => t.OccupantId == id && t.Position.X == from.Item1 && t.Position.Y == from.Item2
            );

            var dest = _tiles.Find(
                t => t.Position.X == to.Item1 && t.Position.Y == to.Item2 // && not occupied?
            );

            if (source == null || dest == null)
                return false;

            source.OccupantId = null;
            dest.OccupantId = id;
            return true;
        }

        public void SetOccupant(Position at, Guid id)
        {
            var tile = _tiles.Find(t => t.Position.X == at.X && t.Position.Y == at.Y);
            tile.OccupantId = id;
        }
    }
}
