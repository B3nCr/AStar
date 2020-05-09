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
            _startPoint = FindStartPoint();
            _endPoint = FindEndPoint();
            _width = _map[0].Length;
            _height = _map.Length;
        }

        public char[][] Travel()
        {
            var foundDestination = false;
            var currentCoordinate = _startPoint;
            var solution = new List<char>();

            while (!foundDestination)
            {
                var possibleMoves = GetPossibleMovesFromCoordinate(currentCoordinate);

                Coordinate nextBestMove = default(Coordinate);

                foreach (var possibleMove in possibleMoves)
                {
                    if (possibleMove.Equals(_endPoint))
                    {
                        foundDestination = true;
                        break;
                    }
                    else
                    {
                        if (nextBestMove.Equals(default(Coordinate)) ||
                        possibleMove.DistanceTo(_endPoint) < nextBestMove.DistanceTo(_endPoint))
                        {
                            nextBestMove = possibleMove;
                        }
                    }
                }

                currentCoordinate = nextBestMove;

                if (!foundDestination)
                {
                    _route.Add(nextBestMove);
                    solution.Add('X');
                }
            }

            return DrawResult(solution);
        }

        private char[][] DrawResult(List<char> solution)
        {
            List<char[]> result = new List<char[]>();
            for (int rowIndex = 0; rowIndex < _height; rowIndex++)
            {
                var row = new List<char>();

                for (int colIndex = 0; colIndex < _width; colIndex++)
                {
                    var currentCoordinate = new Coordinate(rowIndex, colIndex);

                    if (currentCoordinate.Equals(_startPoint))
                    {
                        row.Add('S');
                    }
                    else if (currentCoordinate.Equals(_endPoint))
                    {
                        row.Add('D');
                    }
                    else if (_route.Contains(currentCoordinate))
                    {
                        row.Add('X');
                    }
                    else
                    {
                        row.Add('.');
                    }
                }

                result.Add(row.ToArray());
            }

            return result.ToArray();
        }

        private int MaxX => _map[0].Length - 1;
        private int MaxY => _map.Length - 1;

        public Coordinate[] GetPossibleMovesFromCoordinate(Coordinate startingCoordinate)
        {
            var possibleMoves = new List<Coordinate>();

            if (startingCoordinate.X < MaxX)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.X + 1, startingCoordinate.Y));
            }

            if (startingCoordinate.Y < MaxY)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.X, startingCoordinate.Y + 1));
            }

            if (startingCoordinate.X != 0)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.X - 1, startingCoordinate.Y));
            }

            if (startingCoordinate.Y != 0)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.X, startingCoordinate.Y - 1));
            }

            return possibleMoves.ToArray();
        }
        public Coordinate[] GetPossibleMoves()
        {
            var startingCoordinate = FindStartPoint();
            return GetPossibleMovesFromCoordinate(startingCoordinate);
        }

        private Coordinate FindEndPoint()
        {
            for (int rowIndex = 0; rowIndex < _map.Length; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex < _map.Length; cellIndex++)
                {
                    if (_map[rowIndex][cellIndex] == 'D')
                    {
                        return new Coordinate(rowIndex, cellIndex);
                    }
                }
            }

            throw new Exception("No end point");
        }

        private Coordinate FindStartPoint()
        {
            for (int rowIndex = 0; rowIndex < _map.Length; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex < _map[rowIndex].Length; cellIndex++)
                {
                    if (_map[rowIndex][cellIndex] == 'S')
                    {
                        return new Coordinate(rowIndex, cellIndex);
                    }
                }
            }

            throw new Exception("No starting point");
        }
    }
}
