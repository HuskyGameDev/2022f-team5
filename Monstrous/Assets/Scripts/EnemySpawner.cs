
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemyTemp;

    public GameObject zombieFab;

    public GameObject spidFab;

    //used for later enemy types
    //[SerializeField]
    //private GameObject skeleFab;
    //[SerializeField]
    //private GameObject vampFab;

    //how long between enemySpawns
    private float spawnInterval = 3.5f;

    private GameObject[] enemies;


    void Start()
    {
        //removed enemyTemp as a possible spawn because it didn't have the enemy class methods needed for collision
        enemies = new GameObject[2] {zombieFab, spidFab};
        StartCoroutine(spawnEnemy(spawnInterval, enemies[Random.Range(0, 2)]));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-5f, 5), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(spawnInterval, enemies[Random.Range(0, 2)]));
    }
    
}