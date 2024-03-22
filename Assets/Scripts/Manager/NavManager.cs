using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NavMeshPlus.Components;


public class NavManager : MonoBehaviour
{
    
    public static NavManager instance;
    
    private NavMeshSurface navMeshSurface;

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
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        navMeshSurface.BuildNavMesh();
    }

    private void Update()
    {
        UpdateNavMesh();  
    }

    public void UpdateNavMesh()
    {
        if(transform.position != DungeonManager.instance.currRoom.transform.position)
        {
            transform.position = DungeonManager.instance.currRoom.transform.position;
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
        }
    }

    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

}
