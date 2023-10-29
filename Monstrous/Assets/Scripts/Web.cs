using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.AI;

public class Web : MonoBehaviour{
    [SerializeField] private float slowdownModifier = 0.5f;
    [SerializeField] private string[] exceptions;

    public void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Player"){
            collided.GetComponent<Player>().speedDebuff += slowdownModifier;
        }else if (collided.tag == "Enemy"){
            EnemyBase enemy = collided.GetComponent<EnemyBase>();
            bool exempt = false;
            foreach (string id in exceptions){
                if (enemy.enemyID == id){
                    exempt = true;
                    break;
                }
            }
            if (!exempt){
                enemy.speedDebuff += slowdownModifier;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collided){
        if (collided.tag == "Player"){
            collided.GetComponent<Player>().speedDebuff -= slowdownModifier;
        }else if (collided.tag == "Enemy"){
            EnemyBase enemy = collided.GetComponent<EnemyBase>();
            bool exempt = false;
            foreach (string id in exceptions){
                if (enemy.enemyID == id){
                    exempt = true;
                    break;
                }
            }
            if (!exempt){
                enemy.speedDebuff -= slowdownModifier;
            }
        }
    }
}
