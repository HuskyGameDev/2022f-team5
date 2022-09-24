using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyTemp;

    //used for later enemy types
    //[SerializeField]
    //private GameObject zombieFab;
    //[SerializeField]
    //private GameObject skeleFab;
    //[SerializeField]
    //private GameObject vampFab;
    //[SerializeField]
    //private GameObject spidFab;

    //how long between enemySpawns
    private float spawnInterval = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyTemp));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-5f, 5), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(spawnInterval, enemyTemp));
    }
}
