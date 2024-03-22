using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Dungeon;

public class RoomGenerator : MonoBehaviour
{

    public static RoomGenerator instance;

    [Header("RoomPrefabs")]
    public List<GameObject> startRoomTemplates;

    // "1111" -> "LDRU"
    public List<GameObject> normalRoomTemplates_0000;
    public List<GameObject> normalRoomTemplates_0001;
    public List<GameObject> normalRoomTemplates_0010;
    public List<GameObject> normalRoomTemplates_0011;
    public List<GameObject> normalRoomTemplates_0100;
    public List<GameObject> normalRoomTemplates_0101;
    public List<GameObject> normalRoomTemplates_0110;
    public List<GameObject> normalRoomTemplates_0111;
    public List<GameObject> normalRoomTemplates_1000;
    public List<GameObject> normalRoomTemplates_1001;
    public List<GameObject> normalRoomTemplates_1010;
    public List<GameObject> normalRoomTemplates_1011;
    public List<GameObject> normalRoomTemplates_1100;
    public List<GameObject> normalRoomTemplates_1101;
    public List<GameObject> normalRoomTemplates_1110;
    public List<GameObject> normalRoomTemplates_1111;

    private List<List<GameObject>> normalRoomTemplates;

    public List<GameObject> shopTemplates;
    public List<GameObject> templeTemplates;
    public List<GameObject> bossRoomTemplates;

    [Header("CurrentFloor")]
    public int totalRoomNum; 
    public Layout layout;

    public RoomBase startRoom;
    public RoomBase bossRoom;
    public List<RoomBase> rooms;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            normalRoomTemplates = new List<List<GameObject>>()
            {
                normalRoomTemplates_0000,
                normalRoomTemplates_0001,
                normalRoomTemplates_0010,
                normalRoomTemplates_0011,
                normalRoomTemplates_0100,
                normalRoomTemplates_0101,
                normalRoomTemplates_0110,
                normalRoomTemplates_0111,
                normalRoomTemplates_1000,
                normalRoomTemplates_1001,
                normalRoomTemplates_1010,
                normalRoomTemplates_1011,
                normalRoomTemplates_1100,
                normalRoomTemplates_1101,
                normalRoomTemplates_1110,
                normalRoomTemplates_1111,
            };
        }
    }

    void Start()
    {
        OnSceneLoad();
        DungeonManager.instance.currRoom = startRoom;
    }

    void OnSceneLoad()
    {
        layout = new Layout(totalRoomNum);
        layout.generateLayout();
        generateRooms();
    }

    void Update()
    {
        //testGen();
    }

    void testGen()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void generateRooms()
    {
        
        foreach(Tile tile in layout.rooms)
        {

            if (!tile.isRoom) continue;

            float x = (tile.c - Constants.ROOMS_X_NUM /2) * Constants.ROOM_WIDTH;
            float y = (tile.r - Constants.ROOMS_Y_NUM / 2) * Constants.ROOM_HEIGHT;

            if (tile.t == RoomType.NormalRoom)
            {
                int code = tile.getDoorCode();
                int nroom = normalRoomTemplates[code].Count;

                RoomBase normalRoom = Instantiate(normalRoomTemplates[code][Random.Range(0, nroom)], new Vector3(x, y, 0), Quaternion.identity).GetComponent<RoomBase>();
                normalRoom.initRoom(tile.nDoor, tile.hasDoor);

            }
            else if (tile.t == RoomType.StartRoom)
            {
                int sroom = startRoomTemplates.Count;
                    
                startRoom = Instantiate(startRoomTemplates[Random.Range(0, sroom)], new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RoomBase>();
                startRoom.initRoom(tile.nDoor, tile.hasDoor);

            }else if (tile.t == RoomType.BossRoom)
            {
                int broom = bossRoomTemplates.Count;

                bossRoom = Instantiate(bossRoomTemplates[Random.Range(0, broom)], new Vector3(x, y, 0), Quaternion.identity).GetComponent<RoomBase>();
                bossRoom.initRoom(tile.nDoor, tile.hasDoor);
                    
            }
            else
            {

            }
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
