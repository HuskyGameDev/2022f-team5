using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class LevelGenerator : MonoBehaviour{

        //General Variables
        public bool imageGenerationMode = true;
        public int seed;
        public float scale = 3;
        private DataHolder data;
        private BackgroundData bgData;
        private Player player;
        private System.Random prng;
        private float offsetX;
        private float offsetY;
        [SerializeField] Vector2 movement = new Vector2();

        //Node Mode Variables
        private int leftIndex = 0;
        private int rightIndex;
        private int topIndex = 0;
        private int bottomIndex;

        //Image Generation Variables
        public int textureWidth = 8;
        public int textureHeight = 8;
        public int bgWidth;
        public int bgHeight;
        private Texture2D background;
        public SpriteRenderer render;

        void Awake(){
            //Creates a psudo-random number generator based on the seed
            prng = new System.Random(seed);
            //Chooses the offsets in perlin noise
            offsetX = prng.Next(-100000, 100000);
            offsetY = prng.Next(-100000, 100000);
            //Gets needed components
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            bgData = gameObject.GetComponent<BackgroundData>();
            if (imageGenerationMode){
                background = new Texture2D(bgWidth * textureWidth, bgHeight * textureHeight);
                background.filterMode = FilterMode.Point;
                generateImage();
            }else{
                //Sets scrolling variables
                rightIndex = bgData.width - 1;
                bottomIndex = bgData.height - 1;
                //Generates the initial map
                generateAll();
            }
        }

        //Updates and moves parts of the map depending on player movement
        void FixedUpdate(){
            //Gets the movement from the player
            movement += player.movement * player.moveSpeed * Time.fixedDeltaTime;
            //Moves the leftmost column to the right and regenerates the tiles while the player is moving to the right
            if (imageGenerationMode){

            }else{
                while (movement.x > 1){
                    foreach (GameObject loc in bgData.columns[leftIndex].row){
                        loc.transform.position = new Vector3(loc.transform.position.x + bgData.width, loc.transform.position.y, 0);
                    }
                    movement.x -= 1;
                    generate(bgData.columns[leftIndex].row);
                    rightIndex += 1;
                    leftIndex += 1;
                    if (rightIndex >= bgData.width) rightIndex = 0;
                    if (leftIndex >= bgData.width) leftIndex = 0;
                }
                //Moves the rightmost column to the left and regenerates the tiles while the player is moving to the left
                while (movement.x < -1){
                    foreach (GameObject loc in bgData.columns[rightIndex].row){
                        loc.transform.position = new Vector3(loc.transform.position.x - bgData.width, loc.transform.position.y, 0);
                    }
                    movement.x += 1;
                    generate(bgData.columns[rightIndex].row);
                    rightIndex -= 1;
                    leftIndex -= 1;
                    if (rightIndex < 0) rightIndex = bgData.width - 1;
                    if (leftIndex < 0) leftIndex = bgData.width - 1;
                }
                //Moves the bottommost row to the top and regenerates the tiles while the player is moving up
                while (movement.y > 1){
                    foreach (GameObject loc in bgData.rows[bottomIndex].row){
                        loc.transform.position = new Vector3(loc.transform.position.x, loc.transform.position.y + bgData.height, 0);
                    }
                    movement.y -= 1;
                    generate(bgData.rows[bottomIndex].row);
                    bottomIndex -= 1;
                    topIndex -= 1;
                    if (bottomIndex < 0) bottomIndex = bgData.height - 1;
                    if (topIndex < 0) topIndex = bgData.height - 1;
                }
                //Moves the topmost row to the bottom and regenerates the tiles while the player is moving down
                while (movement.y < -1){
                    foreach (GameObject loc in bgData.rows[topIndex].row){
                        loc.transform.position = new Vector3(loc.transform.position.x, loc.transform.position.y - bgData.height, 0);
                    }
                    movement.y += 1;
                    generate(bgData.rows[topIndex].row);
                    bottomIndex += 1;
                    topIndex += 1;
                    if (bottomIndex >= bgData.height) bottomIndex = 0;
                    if (topIndex >= bgData.height) topIndex = 0;
                }
            }
        }

        //Generates sprites on every node in the render area
        public void generateAll(){
            foreach (Transform loc in transform){
                float noise = Mathf.PerlinNoise((loc.position.x + offsetX) / scale, (loc.position.y + offsetY) / scale);
                //Sets the sprite of the node from the getSprite method
                loc.gameObject.GetComponent<SpriteRenderer>().sprite = getSprite(noise);
            }
        }

        //Generates sprites on a specific array of nodes
        public void generate(GameObject[] nodes){
            foreach (GameObject node in nodes){
                Transform loc = node.transform;
                //Gets the noise value at the current node location
                float noise = Mathf.PerlinNoise((loc.position.x + offsetX) / scale, (loc.position.y + offsetY) / scale);
                //Sets the sprite of the node from the getSprite method
                loc.gameObject.GetComponent<SpriteRenderer>().sprite = getSprite(noise);
            }
        }

        //Selects the needed sprite from the noise value
        private Sprite getSprite(float value){
            Sprite sprite;
            if (value > 0.8f){
                sprite = data.floorTiles[0];
            }else if (value > 0.6f){
                sprite = data.floorTiles[1];
            }else{
                sprite = data.floorTiles[2];
            }
            return sprite;
        }

        private void generateImage(){
            for (int i = 0; i < bgWidth; i++){
                for (int j = 0; j < bgHeight; j++){
                    float noise = Mathf.PerlinNoise(((player.transform.position.x + (i - (bgWidth / 2))) + offsetX) / scale, ((player.transform.position.y + (j - (bgHeight / 2))) + offsetY) / scale);
                    Texture2D texture = getSprite(noise).texture;
                    for (int k = 0; k < textureWidth; k++){
                        for (int l = 0; l < textureHeight; l++){
                            background.SetPixel(k + i, l + j, texture.GetPixel(k, l));
                        }
                    }
                }
            }
            background.Apply();
            render.sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f), textureWidth);
        }
    }
}