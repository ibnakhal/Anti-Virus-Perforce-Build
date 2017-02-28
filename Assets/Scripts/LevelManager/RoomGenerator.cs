using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ROOM GENERATOR SCRIPT
 * IN THE SCENE "Level 2" THERE IS A GAMEOBJECT CALLED "RoomManager" THAT HAS THE FOLLOWING PROPERTIES.
 * Rooms (Contains the types of room and allow designers to drop prefabs to the following Room types)
 *  - Boss Room
 *  - Enemies Room
 *  - Misc Room
 *  - Loot Room
 *  - Starter Room ( Will be spawned once but will randomzied all the prefabs in this category)
 * 
 * Amount (Can modifie the amount of each type of rooms)
 * 
 * Offset Space (The amount of space between each rooms. Default Value = 10)
 * 
 * Total Room (The total of room inputted in the "Amount" field.
 * 
 * **** WARNING *****
 * THIS ONLY WORKS FOR ROOMS WITH DOORS THAT IS LOCATED IN THE CENTER OF EACH WALL. SO PREFABS WITH DOORS LOCATED ON THE CORNERS WILL NOT WORK
*/


public class RoomGenerator : MonoBehaviour
{
    public GameObject[] starterRoom, lootRoom, bossRoom, enemiesRoom, miscRoom;

    private float Length = 40.0f; //The length of the Room 
    public float offsetSpace = 10.0f;

    public int lootRoomAmm, bossRoomAmm, enemiesRoomAmm, miscRoomAmm;
    public int totalRoom;
    public delegate void GRdel(Vector3 loc);
    public GRdel GRevent;

    public delegate void PathDel(Vector3 loc, Vector3 loc2);
    public PathDel PathEvent;

    private List<GameObject> currentRoom = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        totalRoom = lootRoomAmm + bossRoomAmm + enemiesRoomAmm + miscRoomAmm;
        currentRoom.Add(Instantiate(starterRoom[Randomizer(starterRoom)], Vector3.zero, Quaternion.identity));
        RoomManager();


    }

    void RoomManager()
    {
        for (int i = 0; i < totalRoom; i++)
        {
            if (currentRoom.Count == totalRoom + 1)
            {
                break;
            }
            RoomChecker(currentRoom[i], i);
        }

    }

    void RoomChecker(GameObject _currRoom, int roomNo)
    {
        TeleporterRoom[] doors = _currRoom.GetComponentsInChildren<TeleporterRoom>();
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].Deposit == null)
            {
                Vector3 pos = doors[i].ownDeposit.parent.transform.localPosition;
                Vector3 posCheck = posChecker(pos);
                Vector3 newPos = posCheck + currentRoom[roomNo].transform.localPosition;

                int value = CheckIfNoDuplicates(newPos);
                if (value < 0)
                {
                    GameObject currRoom = GenerateRoom(newPos);
                    doors[i].Deposit = TeleportConnector(currRoom, posCheck, doors[i].ownDeposit);
                    ConnectExistingTeleporter(currRoom);
                    if (PathEvent != null)
                    {
                        PathEvent(_currRoom.transform.position, newPos);
                    }

                }
            }
        }
    }



    void ConnectExistingTeleporter(GameObject _currRoom)
    {
        TeleporterRoom[] doors = _currRoom.GetComponentsInChildren<TeleporterRoom>();
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].Deposit == null)
            {
                Vector3 pos = doors[i].ownDeposit.parent.transform.localPosition;
                Vector3 posCheck = posChecker(pos);
                Vector3 newPos = posCheck + _currRoom.transform.localPosition;

                int value = CheckIfNoDuplicates(newPos);
                if (value > 0)
                {
                    doors[i].Deposit = TeleportConnector(currentRoom[value], posCheck, doors[i].ownDeposit);
                    if (PathEvent != null)
                    {
                        PathEvent(_currRoom.transform.position, newPos);
                    }
                }

            }
        }
    }

    int CheckIfNoDuplicates(Vector3 _checker)
    {
        for (int i = 0; i < currentRoom.Count; i++)
        {
            if ((currentRoom[i].transform.localPosition.x % 50) != 0)
            {
                if (currentRoom[i].transform.localPosition == _checker)
                {
                    return i;
                }
            }

            else
            {
                float maxX = currentRoom[i].transform.localPosition.x + 25;
                float maxY = currentRoom[i].transform.localPosition.y + 25;
                float minX = currentRoom[i].transform.localPosition.x - 25;
                float minY = currentRoom[i].transform.localPosition.y - 25;

                Vector3 vec1 = new Vector3(maxX, 0, maxY);
                Vector3 vec2 = new Vector3(maxX, 0, minY);
                Vector3 vec3 = new Vector3(minX, 0, maxY);
                Vector3 vec4 = new Vector3(minX, 0, minY);

                if (vec1 == _checker || vec2 == _checker || vec3 == _checker || vec4 == _checker)
                {
                    return i;
                }
            }
        }

        return -1;
    }
    Transform TeleportConnector(GameObject _currRoom, Vector3 checker, Transform teleport)
    {
        TeleporterRoom[] teles = _currRoom.GetComponentsInChildren<TeleporterRoom>();
        for (int i = 0; i < teles.Length; i++)
        {
            Vector3 pos = teles[i].ownDeposit.parent.transform.localPosition;
            if (posChecker(pos) == (-1 * checker))
            {
                teles[i].Deposit = teleport;
                return teles[i].ownDeposit;
            }
        }

        return null;

    }

    Vector3 posChecker(Vector3 pos)
    {
        float newValue = offsetSpace + Length;
        bool leftRight = (Mathf.Abs(pos.x) > Mathf.Abs(pos.z)) ? true : false;
        if (leftRight)
        {
            if (pos.x > 0) { return (new Vector3(newValue, 0, 0)); }
            else { return (new Vector3(-newValue, 0, 0)); }
        }
        else
        {
            if (pos.z > 0) { return (new Vector3(0, 0, newValue)); }
            else { return (new Vector3(0, 0, -newValue)); }
        }


    }

    GameObject GenerateRoom(Vector3 roomLoc)
    {
        int rand = Random.Range(0, 3);
        if (GRevent != null)
        {
            GRevent(roomLoc);
        }
        GameObject temp = null;
        if (rand == 0)
        {
            if (bossRoomAmm != 0)
                temp = Instantiate(bossRoom[Randomizer(bossRoom)], roomLoc, Quaternion.identity);
            else
                rand++;
        }

        if (rand == 1)
        {
            if (lootRoomAmm != 0)
                temp = Instantiate(lootRoom[Randomizer(lootRoom)], roomLoc, Quaternion.identity);
            else
                rand++;
        }

        if (rand == 2)
        {
            if (miscRoomAmm != 0)
                temp = Instantiate(miscRoom[Randomizer(miscRoom)], roomLoc, Quaternion.identity);
            else
                rand++;
        }

        if (rand == 3)
        {
            if (enemiesRoomAmm != 0)
                temp = Instantiate(enemiesRoom[Randomizer(enemiesRoom)], roomLoc, Quaternion.identity);
            else
                GenerateRoom(roomLoc);
        }


        currentRoom.Add(temp);
        return temp;

    }

    int Randomizer(GameObject[] MaxCount)
    {
        return Random.Range(0, MaxCount.Length);
    }
}
