using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generator{
    public class DoorGenerator : MonoBehaviour{
        
        private DataHolder data;
        [SerializeField] private string direction;
        [SerializeField] private float doorCheckRange = 0.25f;
        [SerializeField] private LayerMask doorMask;

        public void spawnNext(){
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            int generateRoom = Random.Range(0, 3);
            if (generateRoom == 0){
                Instantiate(data.wallTile, transform.position, Quaternion.identity, gameObject.transform.parent);
                Destroy(gameObject);
            }else if (generateRoom > 0){
                Instantiate(data.floorTile, transform.position, Quaternion.identity, gameObject.transform.parent);
                Vector2 roomLocation = transform.position;
                GameObject room = data.rooms[0];
                GameObject nextRoom = Instantiate(room, transform.position, Quaternion.identity);
                Transform removalDoor = null;
                switch (direction){
                    case "north":
                        //nextRoom.transform.position.y += room.GetComponent<BoxCollider2D>().size.y / 2;
                        nextRoom.transform.position = nextRoom.transform.position + new Vector3(0, Mathf.Ceil(nextRoom.GetComponent<BoxCollider2D>().size.y / 2) + 1);
                        removalDoor = nextRoom.GetComponent<RoomData>().south.transform;
                        Destroy(nextRoom.GetComponent<RoomData>().south);
                        break;
                    case "south":
                        //nextRoom.transform.position.y -= room.GetComponent<BoxCollider2D>().size.y / 2;
                        nextRoom.transform.position = nextRoom.transform.position - new Vector3(0, Mathf.Ceil(nextRoom.GetComponent<BoxCollider2D>().size.y / 2) + 1);
                        removalDoor = nextRoom.GetComponent<RoomData>().north.transform;
                        Destroy(nextRoom.GetComponent<RoomData>().north);
                        break;
                    case "east":
                        //nextRoom.transform.position.x += room.GetComponent<BoxCollider2D>().size.x / 2;
                        nextRoom.transform.position = nextRoom.transform.position + new Vector3(Mathf.Ceil(nextRoom.GetComponent<BoxCollider2D>().size.x / 2) + 1, 0);
                        removalDoor = nextRoom.GetComponent<RoomData>().west.transform;
                        Destroy(nextRoom.GetComponent<RoomData>().west);
                        break;
                    case "west":
                        //nextRoom.transform.position.x -= room.GetComponent<BoxCollider2D>().size.x / 2;
                        nextRoom.transform.position = nextRoom.transform.position - new Vector3(Mathf.Ceil(nextRoom.GetComponent<BoxCollider2D>().size.x / 2) + 1, 0);
                        removalDoor = nextRoom.GetComponent<RoomData>().east.transform;
                        Destroy(nextRoom.GetComponent<RoomData>().east);
                        break;
                }
                nextRoom.GetComponent<RoomActivator>().doorway = Instantiate(data.floorTile, removalDoor.position, Quaternion.identity, removalDoor.parent);
                Destroy(gameObject);
            }
        }
    }
}