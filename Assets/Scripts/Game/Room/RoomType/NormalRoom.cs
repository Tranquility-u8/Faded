using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public class NormalRoom : RoomBase
{

    [Header("monster")]
    public MonsterPool monsterPool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DungeonManager.instance.currRoom != this)
            {
                DungeonManager.instance.currRoom = this;
                enterRoom();
                
                if (CameraController.instance != null)
                    CameraController.instance.changeTarget(this.transform);
                if (MiniMapController.instance != null)
                    MiniMapController.instance.changeTarget(this.transform);

                Debug.Log("You will see any Monster here");
            }
        }
    }

    public override void initRoom(int nDoor, List<bool> hDoor)
    {
        numDoor = nDoor;
        hasDoor = hDoor;
        setUpDoors();
        setUpTerrain();
    }

    private void setUpTerrain()
    {
        
    }

    public override void beCleaned()
    {
        isCleaned = true;
        openDoors();
        Destroy(monsterPool);
    }

}
