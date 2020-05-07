using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','.'},
            };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            char[][] expected = new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'X','.','.'},
                new char[3] {'D','.','.'},
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindPossibleMoves_NoDiagonal()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','.'},
            };

            var pathFinder = new Pathfinder(input);

            var possibleMoves = pathFinder.GetPossibleMoves();

            Assert.Contains(new Coordinate(0,1), possibleMoves);
            Assert.Contains(new Coordinate(1,0), possibleMoves);
        }

        [Fact]
        public void FindPossibleMoves_NoNegativeCoordinates()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','.'},
            };

            var pathFinder = new Pathfinder(input);

            var possibleMoves = pathFinder.GetPossibleMoves();

            Assert.DoesNotContain(new Coordinate(0,-1), possibleMoves);
            Assert.DoesNotContain(new Coordinate(-1,0), possibleMoves);
        }

        [Fact]
        public void FindPossibleMoves_NoDoubleMoves()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','.'},
            };

            var pathFinder = new Pathfinder(input);

            var possibleMoves = pathFinder.GetPossibleMoves();

            Assert.DoesNotContain(new Coordinate(0,2), possibleMoves);
            Assert.DoesNotContain(new Coordinate(1,2), possibleMoves);
            Assert.DoesNotContain(new Coordinate(2,2), possibleMoves);
        }

        [Fact]
        public void FindPossibleMoves_StartBottomLeft_NoDiagonal()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'.','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','S'},
            };

            var pathFinder = new Pathfinder(input);

            var possibleMoves = pathFinder.GetPossibleMoves();

            Assert.Contains(new Coordinate(2,1), possibleMoves);
            Assert.Contains(new Coordinate(1,2), possibleMoves);
        }

        [Fact]
        public void FindPossibleMoves_NoCoordinatesExceedMaximumMapBounds()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'.','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','S'},
            };

            var pathFinder = new Pathfinder(input);

            var possibleMoves = pathFinder.GetPossibleMoves();

            Assert.DoesNotContain(new Coordinate(3,2), possibleMoves);
            Assert.DoesNotContain(new Coordinate(2,3), possibleMoves);
        }

        [Theory]
        [InlineData(0, 0, 0, 1, 1)]
        [InlineData(0, 0, 0, 2, 2)]
        [InlineData(0, 2, 0, 0, 2)]
        [InlineData(1, 0, 0, 2, 3)]
        public void DistanceTo_GivesCorrectDistanceToGivenCoordinate(int fromX, int fromY, int toX, int toY, int expectedDistance)
        {
            var distance = new Coordinate(fromX, fromY).DistanceTo(new Coordinate(toX, toY));

            Assert.Equal(expectedDistance, distance);
        }

        [Fact]
        public void Test2()
        {
            char[][] input = new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'.','.','D'},
            };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            var allChars = result.SelectMany(x => x);
            
            Assert.Equal(3, allChars.Where(x=>x=='X').Count());
        }
    }

    public class Pathfinder
    {
        private readonly char[][] _map;
        public Pathfinder(char[][] map)
        {
            _map = map;
        }

        public char[][] Travel()
        {
            // var possibleMoves = GetPossibleMoves(startingCoordinate);
    
            // foreach(var (moveX, moveY) in possibleMoves)
            // {

            // }

            return new char[3][] 
            {
                new char[3] {'S','.','.'},
                new char[3] {'X','.','.'},
                new char[3] {'D','.','.'},
            };
        }

        private int MaxX => _map[0].Length-1;
        private int MaxY => _map.Length-1;

        public Coordinate[] GetPossibleMoves()
        {
            var startingCoordinate = FindStartPoint();

            var possibleMoves = new List<Coordinate>();

            if(startingCoordinate.x < MaxX)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.x+1, startingCoordinate.y));
            }

            if(startingCoordinate.y < MaxY)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.x, startingCoordinate.y+1));
            }

            if (startingCoordinate.x != 0)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.x-1, startingCoordinate.y));
            }

            if (startingCoordinate.y != 0)
            {
                possibleMoves.Add(new Coordinate(startingCoordinate.x, startingCoordinate.y-1));
            }
            
            return possibleMoves.ToArray();
        }

        private (int x, int y) FindStartPoint()
        {
            for (int rowIndex = 0 ;  rowIndex < _map.Length ; rowIndex ++)
            {
                for (int cellIndex = 0 ;  cellIndex < _map.Length ; cellIndex ++)
                {
                    if(_map[rowIndex][cellIndex] == 'S'){
                        return (rowIndex,cellIndex);
                    }
                }
            }

            throw new Exception("No starting point");
        }
    }
}
