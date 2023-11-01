using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.AI;

public class Web : MonoBehaviour{
    [SerializeField] private float deploySpeed = 4f;
    [SerializeField] private float spinSpeed = 100f;
    [SerializeField] private float slowdownModifier = 0.5f;
    [SerializeField] private string[] exceptions;
    [SerializeField] private CircleCollider2D collider;
    public Vector3 targetLoc;
    private State state = State.FLYING;

    private enum State{
        FLYING,
        PLACED
    }

    private void Start(){
        collider.enabled = false;
    }

    private void FixedUpdate(){
        if (state == State.PLACED) return;
        transform.position = Vector2.MoveTowards(transform.position, targetLoc, deploySpeed * Time.fixedDeltaTime);
        transform.Rotate(0, 0, spinSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, targetLoc) < 0.05f){
            state = State.PLACED;
            collider.enabled = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collided){
        if (state == State.FLYING) return;
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
