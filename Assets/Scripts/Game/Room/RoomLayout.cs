using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Dungeon;

//Help to generate the floor and the miniMap
public class Tile
{
    public bool isRoom = false;
    public RoomType t = RoomType.NormalRoom;

    public int nDoor = 0;
    public List<bool> hasDoor = new List<bool>() { false, false, false, false };
   
    public int distance = 0;

    public int r = -1;
    public int c = -1;

    public int getDoorCode()
    {
        int up = hasDoor[0] ? 1 : 0;
        int right = hasDoor[1] ? 1 : 0;
        int down = hasDoor[2] ? 1 : 0;
        int left = hasDoor[3] ? 1 : 0;
        return (up + right * 2 + down * 4 + left * 8);
    }
}


public class Layout
{
    public int roomTotalNum;
    private int roomCurrNum = 0;

    private int row = Constants.ROOMS_Y_NUM / 2;
    private int col = Constants.ROOMS_X_NUM / 2;
    public Direction currDir;

    public List<Tile> rooms = new List<Tile>();
    private List<Tile> sortRooms = new List<Tile>();
    private List<List<Tile>> tiles = new List<List<Tile>>();

    public Layout(int num = 12)
    {
        roomTotalNum = Mathf.Max(num, 10);
        for (int i = 0; i < Constants.ROOMS_Y_NUM; i++)
        {
            List<Tile> row = new List<Tile>();
            for (int j = 0; j < Constants.ROOMS_X_NUM; j++)
            {
                row.Add(new Tile());
            }
            tiles.Add(row);
        }
        Tile tmp = tiles[row][col];
        tmp.isRoom = true;
        tmp.t = RoomType.StartRoom;
        tmp.r = row;
        tmp.c = col;
        rooms.Add(tmp);
        roomCurrNum++;
    }

    public void updateTileDoor(Tile tile)
    {
        int num = 0, i = tile.r, j = tile.c;
        if (i > 0 && tiles[i - 1][j].isRoom)
        {
            num++;
            tiles[i][j].hasDoor[2] = true;
        }
        if (i < Constants.ROOMS_Y_NUM && tiles[i + 1][j].isRoom)
        {
            num++;
            tiles[i][j].hasDoor[0] = true;
        }
        if (j > 0 && tiles[i][j - 1].isRoom)
        {
            num++;
            tiles[i][j].hasDoor[3] = true;
        }
        if (j < Constants.ROOMS_X_NUM && tiles[i][j + 1].isRoom)
        {
            num++;
            tiles[i][j].hasDoor[1] = true;
        }
        tiles[i][j].nDoor = num;
    }

    public void updateTilesDoor()
    {
        foreach(var room in rooms)
        {
            updateTileDoor(room);
        }
    }

    public void updateTileDistance(Tile tile)
    {
        tile.distance = Mathf.Abs(tile.r - Constants.ROOMS_Y_NUM / 2) + Mathf.Abs(tile.c - Constants.ROOMS_X_NUM / 2);
    }

    public void updateTilesDistance()
    {
        foreach(var room in rooms)
        {
            updateTileDistance(room);
        }
    }


    /* 
     * 逻辑：
     * 1.确定普通房间 -> 
     * 2.更新房屋信息 -> 
     * 3.确定特殊房间 -> 
     * 4.更新房屋信息 -> 
     * 5.确定房间布局 
     */

    public void generateLayout()
    {
        // 1:
        setUpNormalRoom();
        // 2:
        updateTilesDoor();
        updateTilesDistance();
        // 3:
        setUpBossRoom();
        setUpShop();
        setUpTemple();
        // 4:
        updateTilesDoor();
    }


    public void setUpNormalRoom()
    {
        templateSetUpNormalRoom();
        while (roomCurrNum < roomTotalNum)
        {
            randomSetUpNormalRoom();
        }

    }

    public void templateSetUpNormalRoom()
    {
        int templateId = Random.Range(0, Constants.TEMPLATES_NUM);
        int rotateNum = Random.Range(0, 4);
        foreach (var dir in Constants.roomTemplates[templateId])
        {
            currDir = (Direction)(((int)dir + rotateNum) % 4);
            moveSpawner();
            if (row < 0 || row >= Constants.ROOMS_Y_NUM || col < 0 || col >= Constants.ROOMS_X_NUM) return;
            Tile tmp = tiles[row][col];
            if (tmp.isRoom) continue;
            roomCurrNum++;
            tmp.isRoom = true;
            tmp.r = row;
            tmp.c = col;
            rooms.Add(tmp);
        }
    }

    public void randomSetUpNormalRoom()
    {
        Tile tmp;
        do
        {
            currDir = (Direction)Random.Range(0, 4);
            moveSpawner();
            if (row < 0 || row >= Constants.ROOMS_Y_NUM || col < 0 || col >= Constants.ROOMS_X_NUM) return;
            tmp = tiles[row][col];
        } while (tmp.isRoom);
        tmp.isRoom = true;
        tmp.r = row;
        tmp.c = col;
        rooms.Add(tmp);
        roomCurrNum++;
    }

    public void setUpBossRoom()
    {
        sortRooms = rooms.OrderByDescending(t => t.distance).ToList();
        foreach(var tile in sortRooms)
        {
            if(tile.nDoor == 1)
            {
                tile.t = RoomType.BossRoom;
                return;
            }
            for(int i = 0; i < 4; i++)
            {
                if (!tile.hasDoor[i])
                {
                    int r_ = tile.r + (int)Constants.DIRECTIONS_VEC2[i].y, c_ = tile.c + (int)Constants.DIRECTIONS_VEC2[i].x;
                    if (r_ < 0 || r_ >= Constants.ROOMS_Y_NUM || c_ < 0 || c_ >= Constants.ROOMS_X_NUM) continue;

                    Tile tmp = tiles[r_][c_];
                    if (tmp.isRoom) continue;

                    //Debug.Log(tile.distance);
                    
                    tmp.isRoom = true;
                    tmp.t = RoomType.BossRoom;
                    tmp.r = r_;
                    tmp.c = c_;
                    rooms.Add(tmp);
                    roomCurrNum++;
                    return;
                }
            }
        }
        
        Debug.Log("Fail to generate bossroom");
    }

    public void setUpShop()
    {
        //TO DO:


    }

    public void setUpTemple()
    {
        //TO DO:

    }

    public void moveSpawner()
    {
        switch (currDir)
        {
            case Direction.Up:
                row++;
                break;
            case Direction.Down:
                row--;
                break;
            case Direction.Left:
                col--;
                break;
            case Direction.Right:
                col++;
                break;
        }
    }
}

