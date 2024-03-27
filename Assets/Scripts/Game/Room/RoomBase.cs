using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public abstract class RoomBase : MonoBehaviour
{
    [Header("Attribute")]
    public RoomType type;
    public RoomLayout layout;
    public bool isCleaned = false;
    public float disToStart = 0;

    [Header("Door")]
    public int numDoor;
    public List<bool> hasDoor = new List<bool> { false, false, false, false };  //up, right, down, left
    public List<GameObject> doors;
    public LayerMask roomLayerMask;

    public void setUpDoors()
    {
        for (int i = 0; i < 4; i++)
        {
            Door door = doors[i].GetComponent<Door>();
            if (hasDoor[i]) {
                door.dir = (Direction)i;
                door.setUpDoor();
            }
            else
            {
                door.type = DoorType.Wall;
                door.GetComponent<SpriteRenderer>().enabled = false;
                door.GetComponent<Collider2D>().enabled = true;
            }


        }
    }

    public abstract void initRoom(int nDoor, List<bool> hasDoor);

    public void enterRoom()
    {
        Player player = GameManager.instance.player;
        if (player.transform.position.x - transform.position.x > 4)
        {
            player.transform.position = new Vector3(player.transform.position.x - 5f, player.transform.position.y);
        }
        else if (player.transform.position.x - transform.position.x < -4)
        {
            player.transform.position = new Vector3(player.transform.position.x + 5f, player.transform.position.y);
        }
        else if (player.transform.position.y - transform.position.y > 4)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 3f);
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3f);
        }
        if (!isCleaned)
        {
            closeDoors();
        }


    }

    public abstract void beCleaned();

    public void openDoors()
    {
        foreach (var door in doors)
        {
            door.GetComponent<Door>().openDoor();

        }
        AudioManager.instance.inOpenDoor();
    }

    public void closeDoors()
    {
        foreach (var door in doors)
        {
            door.GetComponent<Door>().closeDoor();
        }
        AudioManager.instance.inCloseDoor();
    }
}
