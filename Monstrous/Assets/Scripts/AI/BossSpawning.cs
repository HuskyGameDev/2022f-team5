using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class BossSpawning : MonoBehaviour{
        [SerializeField] private AudioClip spawnSound;

        public void Awake(){
            AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        }
    }
}