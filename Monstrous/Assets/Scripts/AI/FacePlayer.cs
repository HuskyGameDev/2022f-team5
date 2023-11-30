using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class FacePlayer : MonoBehaviour{
        [SerializeField] private EnemyBase enemyBase;
        [SerializeField] private bool invert = false;
        [SerializeField] private GameObject[] flipX;
        private bool dir = false;
        private bool oldDir = false;

        void Start(){
            if (invert) enemyBase.renderer.flipX = true;
        }

        void FixedUpdate(){
            oldDir = dir;
            if(enemyBase.playerLoc.position.x > transform.position.x){
                dir = false;
            }else{
                dir = true;
            }
            if (dir != oldDir){
                enemyBase.renderer.flipX = !enemyBase.renderer.flipX;
                foreach (GameObject obj in flipX){
                    obj.transform.localPosition = new Vector3(-obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z);
                }
            }
        }
    }
}