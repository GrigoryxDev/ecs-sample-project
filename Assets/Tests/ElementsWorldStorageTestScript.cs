using System;
using NUnit.Framework;
using UnityEditor;
using System.Linq;

public class ElementsWorldStorageTestScript
{
    private ElementsWorldStorage worldStorage;
    private IElementStaticService elementStaticService;

    [SetUp]
    public void Setup()
    {
        worldStorage = (ElementsWorldStorage)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/ElementsWorldStorage.asset", typeof(ElementsWorldStorage));
        elementStaticService = new ElementStaticService();
    }

    [Test]
    public void TestAllElementHaveSprites()
    {
        //Arrange test
        var allSprites = worldStorage.GetAllSprites();
        var allElements = elementStaticService.GetAllElementIds();
        bool result = false;
        bool isNotEmpties = false;

        //Act test
        isNotEmpties = allElements.Length > 0 && allSprites.Length > 0;
        result = isNotEmpties && allElements.Length == allSprites.Length;

        //Assert test
        Assert.AreEqual(true, result);
    }
}
