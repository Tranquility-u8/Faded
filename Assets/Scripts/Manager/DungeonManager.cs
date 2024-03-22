using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NavMeshPlus.Components;


public class DungeonManager : MonoBehaviour
{

    public static DungeonManager instance;
    
    [Header("Current Room")]
    public RoomBase currRoom;
    public NavMeshSurface NavMeshSurface; 

    private void Awake()
    {
        if (instance != null)
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
        
    }

    void Update()
    {
        testDoor();
        testHurt();
    }

    void testDoor()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currRoom.openDoors();
        }
    }

    void testHurt()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.instance.player.TakeDamage(10);
        }
    }

    public void UpdateNavMesh()
    {
        NavMeshSurface.UpdateNavMesh(NavMeshSurface.navMeshData);
    }

    public void downStair()
    {
        Debug.Log("down stair...");
    }

    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

}
