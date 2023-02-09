using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Only variables that concern the whole project are put here
public class Config
{
  
    //VARIABLES
    public static Dictionary<ColorType, Color32> VAR_COLORS = new Dictionary<ColorType, Color32>() { { ColorType.Red, Color.red }, { ColorType.Green, Color.green }, { ColorType.Blue, Color.blue } };
    public static int VAR_LEVELNUMBER;
    public static Item[] VAR_ITEMPREFABS;
    public static int VAR_REMAININGMOVE = 0;
    public static int VAR_CURRENTLINEDESTROYCOUNT = 0;
    public const int CONST_NEEDEDLINEDESTROY = 5;
    public const int CONST_TOTALMOVE = 30;
    public const int CONST_LINEDESTROYADDEDMOVE = 10;

    //PLAYERPREFS
    public const string PREF_LEVELNUMBER = "LevelNumber";

    //ACTIONS

    public static Action OnLevelStarted;
    public static Action OnLevelFailed;
    public static Action OnLevelCompleted;
    public static Action OnLevelCreationEnded;
    public static Action<Item> OnItemPlaced;


}
public enum ColorType
{
    Red,
    Green,
    Blue,
}
//Interface example
public interface IPlaceable
{
    bool Place(Item _item);
}
