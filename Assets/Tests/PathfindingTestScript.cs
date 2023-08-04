using System.Collections.Generic;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Services.Pathfinding;
using NUnit.Framework;

public class PathfindingTestScript
{
    private Pathfinder pathfinder;

    [SetUp]
    public void Setup()
    {
        pathfinder = new();
        List<Vector2Int> movePositions = new();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (x > 3 && x < 7 && y > 3 && y < 7)
                {
                    continue;
                }
                movePositions.Add(new Vector2Int(x, y));
            }
        }
        pathfinder.UpdateGridPositions(movePositions);
    }

    [Test]
    public void TestFindLinearPathUp()
    {
        //Arrange test
        var startGridPos = new Vector2Int(0, 0);
        var targetGridPos = new Vector2Int(0, 6);

        //Act test
        var path = pathfinder.FindPath(startGridPos, targetGridPos);

        //Assert test
        Assert.Greater(path.Count, 1);
    }

    [Test]
    public void TestFindLinearPathRight()
    {
        //Arrange test
        var startGridPos = new Vector2Int(0, 0);
        var targetGridPos = new Vector2Int(6, 0);

        //Act test
        var path = pathfinder.FindPath(startGridPos, targetGridPos);

        //Assert test
        Assert.Greater(path.Count, 1);
    }

    [Test]
    public void TestFindDifficultPath()
    {
        //Arrange test
        var startGridPos = new Vector2Int(3, 3);
        var targetGridPos = new Vector2Int(8, 8);

        //Act test
        var path = pathfinder.FindPath(startGridPos, targetGridPos);

        //Assert test
        Assert.Greater(path.Count, 1);
    }
}