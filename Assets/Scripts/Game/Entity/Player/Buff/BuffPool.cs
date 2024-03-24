using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffPool : MonoBehaviour
{
    private Player player;
    private Dictionary<string, bool> buffs;
    
    void Awake()
    {
        player = GameManager.instance.player;
        for(int i = 0; i < Constants.buffDictionary.Count; i++)
        {
            buffs.Add(Constants.buffDictionary[i], false);
        }
    }

    public bool checkBuff(string name)
    {
        if (buffs.ContainsKey(name))
            return buffs[name];
        else
        {
            Debug.Log("no such buff");
            return false;
        }
    }

    private void OnUpdateBattle()
    {
        player.calculateDamage();
        player.calculateDefense();
    }

    private void OnUpdateEffect()
    {
        
    }

    public void addBuff(string name)
    {
        if (buffs.ContainsKey(name))
        {
            buffs[name] = true;
            OnUpdateBattle();
            OnUpdateEffect();
        }
        else
        {
            Debug.Log("Invalid buffName");
        }
    }

    public void removeBuff(string name)
    {
        if (buffs.ContainsKey(name))
        {
            buffs[name] = false;
            OnUpdateBattle();
            OnUpdateEffect();
        }
        else
        {
            Debug.Log("Invalid buffName");
        }
    }
}
