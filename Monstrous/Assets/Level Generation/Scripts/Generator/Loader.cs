using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.Generator{
    public class Loader : MonoBehaviour{
        public void OnTriggerEnter2D(Collider2D collided){
            if (collided.gameObject.tag == "Door"){
                collided.gameObject.GetComponent<DoorGenerator>().spawnNext();
            }
        }
    }
}