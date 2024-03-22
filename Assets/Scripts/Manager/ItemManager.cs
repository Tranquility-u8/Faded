using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public Player player;
    public List<GameObject> totalItems;
    public Dictionary<int, int> availableItems;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        player = GameManager.instance.player;
        availableItems = new Dictionary<int, int>();
        for (int i = 0; i <= Constants.itemDictionary.Count; i++)
        {
            availableItems.Add(i, i);
        }
    }

    public GameObject getItemRandomly()
    {
        int key = Random.Range(0, availableItems.Count);
        int id = availableItems[key];
        availableItems.Remove(key);
        return totalItems[id];
    }

    public GameObject getItemById(int num)
    {
        if (num < totalItems.Count)
            return totalItems[num];
        else
        {
            Debug.Log("itemId out of range");
            return null;
        }
    }

    private void OnApplicationQuit()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
    }

}
