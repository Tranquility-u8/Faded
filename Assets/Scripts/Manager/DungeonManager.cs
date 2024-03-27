using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //testGame();
    }

    void testGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void testDoor()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currRoom.openDoors();
        }
    }

    public void UpdateNavMesh()
    {
        NavMeshSurface.UpdateNavMesh(NavMeshSurface.navMeshData);
    }

    public void downStair()
    {
        Debug.Log("down stair...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

}
