using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[CreateAssetMenu(fileName = "WorldStorage", menuName = "ScriptableObject/Storage/WorldStorage")]

public class WorldStorage : ScriptableObject
{
    [SerializeField] private PlayerWorldStorage playerWorldStorage;
    [SerializeField] private ElementsWorldStorage elementsWorldStorage;

    public PlayerWorldStorage PlayerWorldStorage => playerWorldStorage;
    public ElementsWorldStorage ElementsWorldStorage => elementsWorldStorage;
}