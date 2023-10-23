using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPart : MonoBehaviour{

   [SerializeField] public float partValue;
    public string enemyType;
    public int despawnTimer = 150;

    public void Awake(){
        Destroy(gameObject, despawnTimer);
    }

    public void setValues(int partValue, string enemyType){
        this.partValue = partValue;
        this.enemyType = enemyType;
    }
}
