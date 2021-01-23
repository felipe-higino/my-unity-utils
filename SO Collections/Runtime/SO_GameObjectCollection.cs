using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Game Object Collection",
    menuName = "Utils/Collections/Game Object Collection",
    order = 0)]
public class SO_GameObjectCollection : ScriptableObject
{
    [SerializeField]
    private List<GameObject> gameObjects = default;

    public IEnumerable<GameObject> GameObjects => gameObjects;

}