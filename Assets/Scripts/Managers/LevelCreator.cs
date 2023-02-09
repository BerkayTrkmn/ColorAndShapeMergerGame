using HelloScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Level[] LevelDataArray;
    [SerializeField]private Tile tilePrefab;
    [SerializeField]private float tileLengthX =1f;
    [SerializeField]private float tileLengthY =1f;
    [SerializeField]private float spaceLength =0.25f;
    private Level currentLevel;
    private static Tile[,] TileSet;
    [SerializeField] private float spawnTime = 0.2f;
    private void Awake()
    {
        Config.VAR_LEVELNUMBER = PlayerPrefs.GetInt(Config.PREF_LEVELNUMBER, 1);
        currentLevel = LevelDataArray[(Config.VAR_LEVELNUMBER - 1) % LevelDataArray.Length];
        StartCoroutine(CreateLevel());
    }
    private void OnEnable()
    {
        Config.OnItemPlaced+= OnItemPlaced;
    }

    private void OnDisable()
    {
        Config.OnItemPlaced -= OnItemPlaced;
    }
    //Dynamic Level Creation
    //TODO: Camera Moves With Level Size 
    private IEnumerator CreateLevel()
    {
        float _totalWidth = tileLengthX * currentLevel.Width + spaceLength * (currentLevel.Width - 1);
        float _totalHeight = tileLengthY * currentLevel.Height + spaceLength * (currentLevel.Height - 1);
        float _startPointX = (_totalWidth / 2) - (tileLengthX / 2);
        float _startPointY = (_totalHeight / 2) - (tileLengthY / 2);
        TileSet = new Tile[currentLevel.Width, currentLevel.Height];

        for (int y = 0; y < currentLevel.Height; y++)
        {
            for (int x = 0; x < currentLevel.Width; x++)
            {
                Tile _currentTile = Instantiate(tilePrefab);
                _currentTile.transform.parent = transform;
                _currentTile.transform.position = new Vector3(-_startPointX + (x * (tileLengthX + spaceLength)),0f, (y * (tileLengthY + spaceLength)));
                _currentTile.StartingAnimation(_currentTile.transform.position, _currentTile.transform.localScale.y);
                TileSet[x, y] = _currentTile;
                _currentTile.x = x;
                _currentTile.y = y;
                yield return spawnTime.GetWait();
            }
        }

        Config.OnLevelCreationEnded?.Invoke();

    }
    private void OnItemPlaced(Item _item)
    {
        if (CheckLine(_item))
        {
            Config.VAR_REMAININGMOVE += Config.CONST_LINEDESTROYADDEDMOVE;
            Config.VAR_CURRENTLINEDESTROYCOUNT++;
            ClearLine(_item.PlacedTile.y);
            if (Config.VAR_CURRENTLINEDESTROYCOUNT == Config.CONST_NEEDEDLINEDESTROY)
                Config.OnLevelCompleted.Invoke();
        }
       //TODO: Check Fail
       //TODO:Check Complete
    }
    private bool CheckLine(Item _item)
    {
        int currentLine = _item.PlacedTile.y;
        int currentColumn = _item.PlacedTile.x;

       
        for (int x = 0; x < TileSet.GetLength(0) ; x++)
        {
            Item currentItem = TileSet[x, currentLine].placedItem;
            if (!ReferenceEquals(currentItem, null))
            {
               if(!currentItem.CheckEquality(_item))return false;
            }
            else
                return false;
        }
        return true;
    }
    private void ClearLine(int line)
    {
        for (int x = 0; x < TileSet.GetLength(0); x++)
        {
            TileSet[x, line].ClearTile();
        }
    }

}
