using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class Pathfinder
    {
        private readonly char[][] _map;
        private readonly Tile _startTile;
        private readonly Tile _endTile;
        private readonly List<Tile> _route = new List<Tile>();
        private readonly int _width;
        private readonly int _height;

        public Pathfinder(char[][] map)
        {
            _map = map;
            _startTile = FindTile('S');
            _endTile = FindTile('D');

            _route.Add(_startTile);

            _width = _map[0].Length;
            _height = _map.Length;
        }

        private int MaxX => _map.Length - 1;
        private int MaxY => _map[0].Length - 1;

        public char[][] Travel()
        {
            var foundDestination = false;
            var currentTile = _startTile;

            while (!foundDestination)
            {
                var possibleTiles = GetPossbileTiles(currentTile);

                if (possibleTiles.Length == 0)
                {
                    throw new Exception("No possible moves");
                }

                Tile nextBestMove = null;

                foreach (var possibleTile in possibleTiles)
                {
                    if (possibleTile.Equals(_endTile))
                    {
                        foundDestination = true;
                        break;
                    }
                    else
                    {
                        if (nextBestMove == null ||
                         possibleTile.DistanceTo(_endTile) + possibleTile.Cost < nextBestMove.DistanceTo(_endTile) + nextBestMove.Cost)
                        {
                            nextBestMove = possibleTile;
                        }
                    }
                }

                currentTile = nextBestMove;

                if (!foundDestination)
                {
                    _route.Add(nextBestMove);
                }
            }

            return DrawResult();
        }

        private char[][] DrawResult()
        {
            List<char[]> result = new List<char[]>();
            for (int rowIndex = 0; rowIndex < _height; rowIndex++)
            {
                var row = new List<char>();

                for (int colIndex = 0; colIndex < _width; colIndex++)
                {
                    var currentCoordinate = new Coordinate(rowIndex, colIndex);

                    if (!_startTile.Location.Equals(currentCoordinate) &&  _route.Any(x => x.Location.Equals(currentCoordinate)))
                    {
                        row.Add('X');
                    }
                    else
                    {
                        row.Add(_map[rowIndex][colIndex]);
                    }
                }

                result.Add(row.ToArray());
            }

            return result.ToArray();
        }

        // public Tile[] GetPossibleMoves()
        // {
        //     var startingCoordinate = FindPoint('S');
        //     return GetPossibleMovesFromCoordinate(new Tile(_map[startingCoordinate.X][startingCoordinate.Y], startingCoordinate));
        // }

        public Tile[] GetPossbileTiles(Tile startingTile)
        {
            var possibleMoves = new List<Tile>();

            for (int rowIndex = startingTile.Location.X - 1; rowIndex <= startingTile.Location.X + 1; rowIndex++)
            {
                if (rowIndex < 0)
                {
                    continue;
                }

                for (int colIndex = startingTile.Location.Y - 1; colIndex <= startingTile.Location.Y + 1; colIndex++)
                {
                    if (colIndex < 0)
                    {
                        continue;
                    }

                    var possibleCoordinate = new Coordinate(rowIndex, colIndex);

                    if (!possibleCoordinate.Equals(startingTile.Location) && possibleCoordinate.X <= MaxX && possibleCoordinate.Y <= MaxY)
                    {
                        var possibleMove = new Tile(_map[rowIndex][colIndex], possibleCoordinate);

                        if (!_route.Contains(possibleMove) && possibleMove.IsPassable())
                        {
                            possibleMoves.Add(possibleMove);
                        }
                    }
                }
            }

            return possibleMoves.ToArray();
        }

        private Tile FindTile(char tileType)
        {
            for (int rowIndex = 0; rowIndex < _map.Length; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex < _map[rowIndex].Length; cellIndex++)
                {
                    if (_map[rowIndex][cellIndex] == tileType)
                    {
                        return new Tile(tileType, new Coordinate(rowIndex, cellIndex));
                    }
                }
            }

            throw new Exception("Can't find tile");
        }
    }
}
