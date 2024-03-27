using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public class NormalRoom : RoomBase
{

    [Header("Monster")]
    public MonsterPool monsterPool;

    [Header("Bonus")]
    [SerializeField] GameObject bonusPool;

    private void Start()
    {
        if (monsterPool == null)
            isCleaned = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DungeonManager.instance.currRoom != this)
            {
                //destroy dead monsters
                DungeonManager.instance.currRoom.GetComponent<NormalRoom>()?.monsterPool?.OnDisablePool();
                
                //update current room
                DungeonManager.instance.currRoom = this;
                
                //move player
                enterRoom();

                //update monster pool
                if(monsterPool == null)
                {
                    openDoors();
                }
                else if (isCleaned == false)
                {     
                    monsterPool.OnEnablePool();
                }
                
                //move camera
                if (CameraController.instance != null)
                    CameraController.instance.changeTarget(this.transform);
                if (MiniMapController.instance != null)
                    MiniMapController.instance.changeTarget(this.transform);
            }
        }
    }

    public override void initRoom(int nDoor, List<bool> hDoor)
    {
        numDoor = nDoor;
        hasDoor = hDoor;
        setUpDoors();
    }

    public override void beCleaned()
    {
        isCleaned = true;
        if (bonusPool != null)
            bonusPool.SetActive(true);
        openDoors();
    }

}
