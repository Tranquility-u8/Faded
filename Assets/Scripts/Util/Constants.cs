using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public static class Constants
{
    //Direction
    public static List<Vector2> DIRECTIONS_VEC2 = new List<Vector2>
    {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(0, -1),
        new Vector2(-1, 0),
    };

    //Room 
    public const float ROOM_WIDTH = 28;
    public const float ROOM_HEIGHT = 18;

    public const int ROOMS_X_NUM = 25;
    public const int ROOMS_Y_NUM = 25;

    public const int TEMPLATES_NUM = 5;
    public static List<List<Direction>> roomTemplates = new List<List<Direction>>(){
        // "Line"
        new List<Direction>() { Direction.Down, Direction.Down, Direction.Up, Direction.Up, Direction.Up, Direction.Up}, 
        // "Stair"
        new List<Direction>() { Direction.Right, Direction.Up, Direction.Right, Direction.Up},
        // "Cross"
        new List<Direction>() { Direction.Left, Direction.Right, Direction.Right, Direction.Left, Direction.Down, Direction.Up, Direction.Up }, 
        // " T "
        new List<Direction>() { Direction.Left, Direction.Right, Direction.Down, Direction.Down, Direction.Up, Direction.Up, Direction.Right}, 
        // " Z "
        new List<Direction>() { Direction.Down, Direction.Right, Direction.Left, Direction.Up, Direction.Up, Direction.Left},
    };

    //Coin
    public static List<int> COIN_VALUES = new List<int> { 1, 5, 10 };

    //Item
    public static Dictionary<string, int> itemDictionary = new Dictionary<string, int>()
        {
            {"MyopiaGlasses", 0},
            {"HyperopiaGlasses", 1 },
        };

    //Buff
    public static Dictionary<string, int> buffDictionary = new Dictionary<string, int>()
        {
            {"AbnormalVision", 0},
            {"Quadricolorist", 1 },
            {"Blindness", 2 },
            {"Daltonism", 3 },
            {"Aphantasia", 4 },
            {"Photodynia", 5 },
            {"Myopia", 6 },
            {"Hyperopia", 7 },
        };

    //Keyboard
    public const KeyCode OPEN_CHEST = KeyCode.Return;
    public const KeyCode JUMP_TRAP = KeyCode.Return;
}


