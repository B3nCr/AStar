using Xunit;
using System.Linq;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void TestBasicPath()
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
        public void UnpassableTerrain()
        {
            char[][] input = new char[3][]
                       {
                new char[3] {'S','.','.'},
                new char[3] {'#','#','.'},
                new char[3] {'D','.','.'},
                       };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            char[][] expected = new char[3][]
            {
                new char[3] {'S','X','.'},
                new char[3] {'#','#','X'},
                new char[3] {'D','X','.'},
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestBasicPathHasCorrectNumberOfPathNodes()
        {
            char[][] input = new char[3][]
            {
                new char[3] {'S','.','.'},
                new char[3] {'.','.','.'},
                new char[3] {'D','.','.'},
            };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            Assert.Equal(1, result.SelectMany(x => x).Where(x => x == 'X').Count());
        }

        // [Fact]
        // public void FindPossibleMoves_NoDiagonal()
        // {
        //     char[][] input = new char[3][]
        //     {
        //         new char[3] {'S','.','.'},
        //         new char[3] {'.','.','.'},
        //         new char[3] {'D','.','.'},
        //     };

        //     var pathFinder = new Pathfinder(input);

        //     var possibleMoves = pathFinder.GetPossibleMoves();

        //     Assert.Contains(new Coordinate(0, 1), possibleMoves);
        //     Assert.Contains(new Coordinate(1, 0), possibleMoves);
        // }

        [Fact]
        public void UseTileCostForRoute()
        {
            char[][] input = new char[2][]
            {
                new char[3] {'S','V', '.'},
                new char[3] {'.','.','D'}
            };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            char[][] expected = new char[2][]
            {
                new char[3] {'S', 'V', '.'},
                new char[3] {'.', 'X', 'D'}
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UseTileCostForRoute2()
        {
            char[][] input = new char[2][]
            {
                new char[3] {'S','.','.'},
                new char[3] {'V','.','D'}
            };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            char[][] expected = new char[2][]
            {
                new char[3] {'S','.','.'},
                new char[3] {'V','X','D'}
            };

            Assert.Equal(expected, result);
        }

        // [Fact]
        // public void FindPossibleMoves_NoNegativeCoordinates()
        // {
        //     char[][] input = new char[3][]
        //     {
        //         new char[3] {'S','.','.'},
        //         new char[3] {'.','.','.'},
        //         new char[3] {'D','.','.'},
        //     };

        //     var pathFinder = new Pathfinder(input);

        //     var possibleMoves = pathFinder.GetPossibleMoves();

        //     Assert.DoesNotContain(new Coordinate(0, -1), possibleMoves);
        //     Assert.DoesNotContain(new Coordinate(-1, 0), possibleMoves);
        // }

        // [Fact]
        // public void FindPossibleMoves_NoDoubleMoves()
        // {
        //     char[][] input = new char[3][]
        //     {
        //         new char[3] {'S','.','.'},
        //         new char[3] {'.','.','.'},
        //         new char[3] {'D','.','.'},
        //     };

        //     var pathFinder = new Pathfinder(input);

        //     var possibleMoves = pathFinder.GetPossibleMoves();

        //     Assert.DoesNotContain(new Coordinate(0, 2), possibleMoves);
        //     Assert.DoesNotContain(new Coordinate(1, 2), possibleMoves);
        //     Assert.DoesNotContain(new Coordinate(2, 2), possibleMoves);
        // }

        // [Fact]
        // public void FindPossibleMoves_StartBottomLeft_NoDiagonal()
        // {
        //     char[][] input = new char[3][]
        //     {
        //         new char[3] {'.','.','.'},
        //         new char[3] {'.','.','.'},
        //         new char[3] {'D','.','S'},
        //     };

        //     var pathFinder = new Pathfinder(input);

        //     var possibleMoves = pathFinder.GetPossibleMoves();

        //     Assert.Contains(new Coordinate(2, 1), possibleMoves);
        //     Assert.Contains(new Coordinate(1, 2), possibleMoves);
        // }

        // [Fact]
        // public void FindPossibleMoves_NoCoordinatesExceedMaximumMapBounds()
        // {
        //     char[][] input = new char[3][]
        //     {
        //         new char[3] {'.','.','.'},
        //         new char[3] {'.','.','.'},
        //         new char[3] {'D','.','S'},
        //     };

        //     var pathFinder = new Pathfinder(input);

        //     var possibleMoves = pathFinder.GetPossibleMoves();

        //     Assert.DoesNotContain(new Coordinate(3, 2), possibleMoves);
        //     Assert.DoesNotContain(new Coordinate(2, 3), possibleMoves);
        // }

        [Fact]
        public void OuputsCorrectGridForInput()
        {
            char[][] input = new char[1][]
            {
                new char[2] {'D','S'}
            };

            var pathFinder = new Pathfinder(input);

            var result = pathFinder.Travel();

            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData(0, 0, 0, 1, 1)]
        [InlineData(0, 0, 0, 2, 2)]
        [InlineData(0, 2, 0, 0, 2)]
        [InlineData(1, 0, 0, 2, 3)]
        public void DistanceTo_GivesCorrectDistanceToGivenCoordinate(int fromX, int fromY, int toX, int toY, int expectedDistance)
        {
            var distance = new Coordinate(fromX, fromY)
                .DistanceTo(new Coordinate(toX, toY));

            Assert.Equal(expectedDistance, distance);
        }

        [Fact]
        public void OutputsCorrectNumberOfXsForMoreComplexPath()
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

            Assert.Equal(1, allChars.Where(x => x == 'X').Count());
        }
    }
}
