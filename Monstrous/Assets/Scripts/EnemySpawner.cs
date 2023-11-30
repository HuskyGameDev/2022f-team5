
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Generation;
using Monstrous.Data;
using Monstrous.AI;


public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public Biome biome;
    public ChunkController chunk;
    public float timeElapsed;
    public float diffScale=1;
    public AnimationCurve spawningScale;
    
    //how long between enemySpawns
    [SerializeField] private float spawnInterval = 3.75f;
    [SerializeField] private float bossSpawnInterval = 600f;

    private GameObject[] enemies;
    private int num;

    public AudioSource bgMusic;
    public AudioClip[] musicList;

    private bool musicChange;

    void Start()
    {
        //removed enemyTemp as a possible spawn because it didn't have the enemy class methods needed for collision
        musicChange = false;
        biome = chunk.getBiome((int)player.position.x,(int)player.position.y);
     
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnBoss());
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 10f) // increase difficulty every minute
        {
            diffScale *= 1.085f;
            //Debug.Log(diffScale);
            timeElapsed = 0f;
        }
    
    }

    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);

        Vector3 clusterHome = Vector3.zero;
        switch (Random.Range(1,4))
        {
            case 1://top edge
                {
                    //Debug.Log("1");
                    clusterHome.y = player.position.y + 13f;
                    clusterHome.x = player.position.x + (float) Random.Range(-20, 20);
                    break;
                }
            case 2://right edge
                {
                    //Debug.Log("2");
                    clusterHome.x = player.position.x + 17f;
                    clusterHome.y = player.position.y + (float)Random.Range(-15, 15);
                    break;
                }
            case 3://bottom edge
                {
                    //Debug.Log("3");
                    clusterHome.y = player.position.y - 13f;
                    clusterHome.x = player.position.x + (float)Random.Range(-20, 20);
                    break;
                }
            case 4://left edge
                {
                    //Debug.Log("4");
                    clusterHome.x = player.position.x - 17f;
                    clusterHome.y = player.position.y + (float)Random.Range(-15, 15);
                    break;
                }
        }

        Vector3 noise = Vector3.zero;
        biome = chunk.getBiome((int) clusterHome.x, (int) clusterHome.y);
        GameObject enemy = biome.enemies[Random.Range(0, biome.enemies.Length)];

        for (int i = 0; i < (int) spawningScale.Evaluate(diffScale) * Random.Range(2,4); i++){
            noise.x = Random.Range(-3, 3);
            noise.y = Random.Range(-3, 3);
            GameObject newEnemy = Instantiate(enemy, clusterHome - noise, Quaternion.identity);
            newEnemy.GetComponent<EnemyBase>().difficultyScale = diffScale;
        }
       
        StartCoroutine(spawnEnemy());
    }
    
    private IEnumerator spawnBoss(){
        biome = chunk.getBiome((int)player.position.x,(int)player.position.y);
        yield return new WaitForSeconds(bossSpawnInterval);

        if (!musicChange)
        {
            musicChange = true;
            bgMusic = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
            bgMusic.clip = musicList[3];
            bgMusic.Play();
        }

        Vector3 home = Vector3.zero;
        switch (Random.Range(1,4)){
            case 1://top edge
                home.y = player.position.y + 13f;
                home.x = player.position.x + (float) Random.Range(-20, 20);
                break;
            case 2://right edge
                home.x = player.position.x + 17f;
                home.y = player.position.y + (float)Random.Range(-15, 15);
                break;
            case 3://bottom edge
                home.y = player.position.y - 13f;
                home.x = player.position.x + (float)Random.Range(-20, 20);
                break;
            case 4://left edge
                home.x = player.position.x - 17f;
                home.y = player.position.y + (float)Random.Range(-15, 15);
                break;
        }
        biome = chunk.getBiome((int) home.x, (int) home.y);
        if (biome.bosses.Length > 0){
            GameObject newEnemy = Instantiate(biome.bosses[Random.Range(0, biome.bosses.Length)], home, Quaternion.identity);
            newEnemy.GetComponent<EnemyBase>().difficultyScale = diffScale;
        }
        StartCoroutine(spawnBoss());
    }

    public float getDiffScale(){
        return diffScale;
    }
    
}