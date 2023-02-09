using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Item[] spawningItems;
    private Item currentItem;
    
    void Awake()
    {
        spawningItems = Config.VAR_ITEMPREFABS = Resources.LoadAll<Item>("Prefabs/Items");
    }
    private void OnEnable()
    {
        Config.OnLevelStarted += OnLevelStarted;
        Config.OnItemPlaced += OnItemSpawned;
    }

    private void OnItemSpawned(Item obj)
    {
        RandomSpawnItem();
    }

    private void OnLevelStarted()
    {
        RandomSpawnItem();
    }

    private void OnDisable()
    {
        Config.OnLevelStarted -= OnLevelStarted;
        Config.OnItemPlaced -= OnItemSpawned;

    }
    //NO need for object pooling
    private void RandomSpawnItem()
    {
        int _itemType = Random.Range(0, spawningItems.Length);
        currentItem =  Instantiate(spawningItems[_itemType], transform.position,Quaternion.identity);
        currentItem.SetRandomColor();
    }
   
}
