using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffPool : MonoBehaviour
{
    public Dictionary<int, bool> buffs;

    void Start()
    {
       for(int i = 0; i < Constants.buffDictionary.Count; i++)
        {
            buffs.Add(i, false);
        }
    }

    public void removeBuff(int i)
    {
        buffs[i] = false;
    }
}
