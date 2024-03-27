using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{

    public List<GameObject> monsters;
    private int num;
    public List<GameObject> FindChildrenWithComponent<T>() where T : Component
    {
        List<GameObject> resultList = new List<GameObject>();
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<T>() != null)
            {
                resultList.Add(child.gameObject);
            }
        }

        return resultList;
    }

    void Start()
    {
        monsters = FindChildrenWithComponent<MonsterBase>();
        num = monsters.Count;
        OnDisablePool();
    }

    void Update()
    {
        
    }
    
    public void OnEnablePool()
    {
        StartCoroutine(EnablePool());
    }

    IEnumerator EnablePool()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (var monster in monsters)
        {
            monster.SetActive(true);
        }
    }

    public void OnDisablePool()
    {
        foreach (var monster in monsters)
        {
            monster.SetActive(false);
        }
    }

    public void updatePool()
    {
        num--;
        if(num == 0)
        {
            DungeonManager.instance.currRoom.beCleaned();
        }
    }

}
