using System;
using NUnit.Framework;
using UnityEditor;
using System.Linq;
public class UIStatesTestScript
{
    private UIStatesStorage uIStatesStorage;

    [SetUp]
    public void Setup()
    {
        uIStatesStorage = (UIStatesStorage)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/UIStatesStorage.asset", typeof(UIStatesStorage));
    }

    [Test]
    public void TestAllStatesWithoutDuplicates()
    {
        //Arrange test
        var allStates = uIStatesStorage.GetAllStates();
        var GameStatesArray = (GameStates[])Enum.GetValues(typeof(GameStates));
        var anyDuplicate = false;

        //Act test
        anyDuplicate = GameStatesArray.GroupBy(x => x)
        .Any(g => g.Count() > 1);

        //Assert test
        Assert.AreEqual(false, anyDuplicate);
    }

    [Test]
    public void TestAllStatesExists()
    {
        //Arrange test
        var allStates = uIStatesStorage.GetAllStates();
        var GameStatesArray = (GameStates[])Enum.GetValues(typeof(GameStates));
        var result = false;

        //Act test
        result = GameStatesArray.All(state => allStates.Any(x => x.State == state));

        //Assert test
        Assert.AreEqual(true, result);
    }

    [Test]
    public void TestAllStatesHaveGoReference()
    {
        //Arrange test
        var allStates = uIStatesStorage.GetAllStates();
        var GameStatesArray = (GameStates[])Enum.GetValues(typeof(GameStates));
        var result = false;

        //Act test
        result = GameStatesArray.All(state =>
        {
            var firstState = allStates.FirstOrDefault(x => x.State == state);
            var isRefernceNotNull = firstState.Reference != null;
            return isRefernceNotNull;
        });

        //Assert test
        Assert.AreEqual(true, result);
    }
}