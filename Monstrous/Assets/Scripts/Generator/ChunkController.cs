using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class ChunkController : MonoBehaviour
    {
        public GameObject chunk;
        [Header("Dimensions")]
        public int bgWidth = 4;
        public int bgHeight = 3;
        public int chunkWidth = 16;
        public int chunkHeight = 16;
        public int textureWidth = 8;
        public int textureHeight = 8;
        [Header("Generation Settings")]
        public bool randomizeSeed = true;
        public int seed;
        public float scale = 3;
        public float biomeScale = 50;
        public float structureFrequency = 10;
        private DataHolder data;
        private Player player;
        private System.Random prng;
        private float offsetX;
        private float offsetY;
        private Vector2 movement = new Vector2(0, 0);

        private int leftIndex = 0;
        private int rightIndex;
        private int topIndex;
        private int bottomIndex = 0;
        private GameObject[,] nodes;

        public void Awake(){
            nodes = new GameObject[bgWidth, bgHeight];
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            //Creates a psudo-random number generator based on the seed
            if (randomizeSeed) seed = Random.Range(-2147483648, 2147483647);
            prng = new System.Random(seed);
            //Chooses the offsets in perlin noise
            offsetX = prng.Next(-100000, 100000);
            offsetY = prng.Next(-100000, 100000);
            //Sets scrolling variables
            rightIndex = bgWidth - 1;
            topIndex = bgHeight - 1;
            for (int i = 0; i < bgWidth; i++){
                for (int j = 0; j < bgHeight; j++){
                    nodes[i, j] = Instantiate(chunk, new Vector3(chunkWidth * (i - (bgWidth / 2)), chunkHeight * (j - (bgHeight / 2)), 0), Quaternion.identity, transform);
                    nodes[i, j].GetComponent<ChunkGenerator>().setVariables(chunkWidth, chunkHeight, textureWidth, textureHeight, offsetX, offsetY, scale, this);
                }
            }
        }

        public Biome getBiome(int x, int y){
            float noise = Mathf.PerlinNoise((x + offsetX) / biomeScale, (y + offsetY) / biomeScale);
            int index = (int) (noise * data.biomes.Length);
            index = Mathf.Clamp(index, 0, data.biomes.Length - 1);
            return data.biomes[index];
        }
        

        public void FixedUpdate(){
            movement += player.movement * player.moveSpeed * Time.fixedDeltaTime;
            while (movement.x > chunkWidth){
                for(int i = 0; i < bgHeight; i++){
                    ChunkGenerator node = nodes[leftIndex, i].GetComponent<ChunkGenerator>();
                    node.move(new Vector3(node.transform.position.x + (bgWidth * chunkWidth), node.transform.position.y, 0));
                }
                movement.x -= chunkWidth;
                rightIndex += 1;
                leftIndex += 1;
                if (rightIndex >= bgWidth) rightIndex = 0;
                if (leftIndex >= bgWidth) leftIndex = 0;
            }
            //Moves the rightmost column to the left and regenerates the tiles while the player is moving to the left
            while (movement.x < -chunkWidth){
                for(int i = 0; i < bgHeight; i++){
                    ChunkGenerator node = nodes[rightIndex, i].GetComponent<ChunkGenerator>();
                    node.move(new Vector3(node.transform.position.x - (bgWidth * chunkWidth), node.transform.position.y, 0));
                }
                movement.x += chunkWidth;
                rightIndex -= 1;
                leftIndex -= 1;
                if (rightIndex < 0) rightIndex = bgWidth - 1;
                if (leftIndex < 0) leftIndex = bgWidth - 1;
            }
            //Moves the bottommost row to the top and regenerates the tiles while the player is moving up
            while (movement.y > chunkHeight){
                for(int i = 0; i < bgWidth; i++){
                    ChunkGenerator node = nodes[i, bottomIndex].GetComponent<ChunkGenerator>();
                    node.move(new Vector3(node.transform.position.x, node.transform.position.y + (bgHeight * chunkHeight), 0));
                }
                movement.y -= chunkHeight;
                bottomIndex += 1;
                topIndex += 1;
                if (bottomIndex >= bgHeight) bottomIndex = 0;
                if (topIndex >= bgHeight) topIndex = 0;
            }
            //Moves the topmost row to the bottom and regenerates the tiles while the player is moving down
            while (movement.y < -chunkHeight){
                for(int i = 0; i < bgWidth; i++){
                    ChunkGenerator node = nodes[i, topIndex].GetComponent<ChunkGenerator>();
                    node.move(new Vector3(node.transform.position.x, node.transform.position.y - (bgHeight * chunkHeight), 0));
                }
                movement.y += chunkHeight;
                bottomIndex -= 1;
                topIndex -= 1;
                if (bottomIndex < 0) bottomIndex = bgHeight - 1;
                if (topIndex < 0) topIndex = bgHeight - 1;
            }
        }
    }
}