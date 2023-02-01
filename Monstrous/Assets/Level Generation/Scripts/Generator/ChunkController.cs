using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class ChunkController : MonoBehaviour
    {
        public GameObject chunk;
        public int bgWidth = 4;
        public int bgHeight = 3;
        public int chunkWidth = 16;
        public int chunkHeight = 16;
        public int textureWidth = 8;
        public int textureHeight = 8;
        public int seed;
        public float scale = 3;
        private DataHolder data;
        private Player player;
        private System.Random prng;
        private float offsetX;
        private float offsetY;
        [SerializeField] Vector2 movement = new Vector2(0, 0);

        private int leftIndex = 0;
        private int rightIndex;
        private int topIndex;
        private int bottomIndex = 0;
        private GameObject[,] nodes;

        public void Start(){
            nodes = new GameObject[bgWidth, bgHeight];
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            //Creates a psudo-random number generator based on the seed
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
                    nodes[i, j].GetComponent<ChunkGenerator>().setVariables(chunkWidth, chunkHeight, textureWidth, textureHeight, offsetX, offsetY, scale, data);
                }
            }
        }

        public void FixedUpdate(){
            movement += player.movement * player.moveSpeed * Time.fixedDeltaTime;
            while (movement.x > chunkWidth){
                for(int i = 0; i < bgHeight; i++){
                    GameObject node = nodes[leftIndex, i];
                    node.transform.position = new Vector3(node.transform.position.x + (bgWidth * chunkWidth), node.transform.position.y, 0);
                    node.GetComponent<ChunkGenerator>().generateImage();
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
                    GameObject node = nodes[rightIndex, i];
                    node.transform.position = new Vector3(node.transform.position.x - (bgWidth * chunkWidth), node.transform.position.y, 0);
                    node.GetComponent<ChunkGenerator>().generateImage();
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
                    GameObject node = nodes[i, bottomIndex];
                    node.transform.position = new Vector3(node.transform.position.x, node.transform.position.y + (bgHeight * chunkHeight), 0);
                    node.GetComponent<ChunkGenerator>().generateImage();
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
                    GameObject node = nodes[i, topIndex];
                    node.transform.position = new Vector3(node.transform.position.x, node.transform.position.y - (bgHeight * chunkHeight), 0);
                    node.GetComponent<ChunkGenerator>().generateImage();
                }
                movement.y += chunkHeight;
                bottomIndex -= 1;
                topIndex -= 1;
                if (bottomIndex < 0) bottomIndex = bgHeight - 1;
                if (topIndex < 0) topIndex = bgHeight - 1;
            //     foreach (GameObject loc in bgData.rows[topIndex].row){
            //         loc.transform.position = new Vector3(loc.transform.position.x, loc.transform.position.y - bgData.height, 0);
            //     }
            //     movement.y += 1;
            //     generate(bgData.rows[topIndex].row);
            //     bottomIndex += 1;
            //     topIndex += 1;
            //     if (bottomIndex >= bgData.height) bottomIndex = 0;
            //     if (topIndex >= bgData.height) topIndex = 0;
            }
        }
    }
}