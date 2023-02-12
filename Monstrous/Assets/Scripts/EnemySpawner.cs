
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Generation;
using Monstrous.Data;


public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemyTemp;
    
    public GameObject zombieFab;

    public GameObject spidFab;
    public Biome biome;
    public ChunkController chunk;

    //used for later enemy types
    //[SerializeField]
    //private GameObject skeleFab;
    //[SerializeField]
    //private GameObject vampFab;

    //how long between enemySpawns
    private float spawnInterval = 3.5f;

    private GameObject[] enemies;
    private int num;

    void Start()
    {
        //removed enemyTemp as a possible spawn because it didn't have the enemy class methods needed for collision
        enemies = new GameObject[2] {zombieFab, spidFab};

        biome = chunk.getBiome((int)player.position.x,(int)player.position.y);
      
        StartCoroutine(spawnEnemy(spawnInterval, biome.enemies[Random.Range(0,biome.enemies.Length)]));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        biome = chunk.getBiome((int)player.position.x,(int)player.position.y);

        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(player.position.x-15, player.position.x+15), Random.Range(player.position.y-10, player.position.y+10), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(spawnInterval, biome.enemies[Random.Range(0,biome.enemies.Length)]));
    }
    
}