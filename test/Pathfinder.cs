using System;
using System.Collections.Generic;

namespace test
{
    public class Pathfinder
    {
        private readonly char[][] _map;
        private readonly Coordinate _startPoint;
        private readonly Coordinate _endPoint;
        private readonly List<Coordinate> _route = new List<Coordinate>();
        private readonly int _width;
        private readonly int _height;

        public Pathfinder(char[][] map)
        {
            _map = map;
            _startPoint = FindPoint('S');
            _endPoint = FindPoint('D');

            _width = _map[0].Length;
            _height = _map.Length;
        }

        private int MaxX => _map.Length - 1;
        private int MaxY => _map[0].Length - 1;

        public char[][] Travel()
        {
            var foundDestination = false;
            var currentCoordinate = new Tile(_map[_startPoint.X][_startPoint.Y], _startPoint);

            while (!foundDestination)
            {
                var possibleMoves = GetPossibleMovesFromCoordinate(currentCoordinate);

                if (possibleMoves.Length == 0)
                {
                    throw new Exception("No possible moves");
                }

                Tile nextBestMove = null;

                foreach (var possibleMove in possibleMoves)
                {
                    // var tile = new Tile(_map[possibleMove.X][possibleMove.Y], possibleMove);

                    if (possibleMove.Location.Equals(_endPoint))
                    {
                        foundDestination = true;
                        break;
                    }
                    else
                    {
                        if (nextBestMove == null ||
                         possibleMove.Location.DistanceTo(_endPoint) + possibleMove.Cost < nextBestMove.Location.DistanceTo(_endPoint) + nextBestMove.Cost)
                        {
                            nextBestMove = possibleMove;
                        }
                    }
                }

                currentCoordinate = nextBestMove;

                if (!foundDestination)
                {
                    _route.Add(nextBestMove.Location);
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

                    if (_route.Contains(currentCoordinate))
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

        public Tile[] GetPossibleMovesFromCoordinate(Tile startingTile)
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

                        possibleMoves.Add(possibleMove);
                    }
                }
            }

            return possibleMoves.ToArray();
        }

        private Coordinate FindPoint(char tileType)
        {
            for (int rowIndex = 0; rowIndex < _map.Length; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex < _map[rowIndex].Length; cellIndex++)
                {
                    if (_map[rowIndex][cellIndex] == tileType)
                    {
                        return new Coordinate(rowIndex, cellIndex);
                    }
                }
            }

            throw new Exception("Can't find tile");
        }
    }
}
