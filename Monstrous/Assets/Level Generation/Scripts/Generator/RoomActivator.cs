using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generator{
    public class RoomActivator : MonoBehaviour{

        public GameObject doorway = null;

        public void Awake(){
            Invoke("establish", 0.5f);
        }

        private void OnTriggerEnter2D(Collider2D collided){
            Debug.Log("Room Detected. Initiating Delete");
            DataHolder data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            Instantiate(data.wallTile, doorway.transform.position, Quaternion.identity, doorway.transform.parent);
            Destroy(doorway);
            Destroy(gameObject);
        }

        private void establish(){
            Destroy(this);
        }
    }
}