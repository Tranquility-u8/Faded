using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public static class UtilsFunc
{
        
    public static int getKeyboardDir()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return 0;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) return 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) return 2;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) return 3;
        else return -1;
    }

}
