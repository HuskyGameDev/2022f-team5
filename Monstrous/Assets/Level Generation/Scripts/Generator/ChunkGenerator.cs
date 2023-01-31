using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class ChunkGenerator : MonoBehaviour
    {
        [Header("Dimensions")]
        public int bgWidth = 20;
        public int bgHeight = 20;
        public int textureWidth = 8;
        public int textureHeight = 8;
        [Header("Neigboring Chunks")]
        public GameObject north = null;
        public GameObject south = null;
        public GameObject east = null;
        public GameObject west = null;
        [Header("Extras")]
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private GameObject chunk;
        public int seed = 0;
        public int scale = 5;

        private DataHolder data;
        private float offsetX;
        private float offsetY;
        private Texture2D background;
        private System.Random prng;

        public void Start(){
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            //Creates a psudo-random number generator based on the seed
            prng = new System.Random(seed);
            //Chooses the offsets in perlin noise
            offsetX = prng.Next(-100000, 100000);
            offsetY = prng.Next(-100000, 100000);
            background = new Texture2D(bgWidth * textureWidth, bgHeight * textureHeight);
            background.filterMode = FilterMode.Point;
            generateImage();
        }

        private void generateImage(){
            for (int i = 0; i < bgWidth; i++){
                for (int j = 0; j < bgHeight; j++){
                    float noise = Mathf.PerlinNoise(((transform.position.x + (i - (int) (bgWidth / 2))) + offsetX) / scale, ((transform.position.y + (j - (int) (bgHeight / 2))) + offsetY) / scale);
                    Texture2D texture = getSprite(noise).texture;
                    for (int k = 0; k < textureWidth; k++){
                        for (int l = 0; l < textureHeight; l++){
                            background.SetPixel(k + (textureWidth * i), l + (textureHeight * j), texture.GetPixel(k, l));
                        }
                    }
                }
            }
            background.Apply();
            renderer.sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f), textureWidth);
        }

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

        private void OnTriggerEnter2D(Collider2D collided) {
            if (collided.gameObject.tag == "Loader"){
                renderer.enabled = true;
                if (north == null){
                    north = Instantiate(chunk, new Vector3(transform.position.x, transform.position.y + bgHeight), Quaternion.identity, transform.parent);
                    north.GetComponent<ChunkGenerator>().south = gameObject;
                }if (south == null){
                    south = Instantiate(chunk, new Vector3(transform.position.x, transform.position.y - bgHeight), Quaternion.identity, transform.parent);
                    south.GetComponent<ChunkGenerator>().north = gameObject;
                }if (east == null){
                    east = Instantiate(chunk, new Vector3(transform.position.x + bgWidth, transform.position.y), Quaternion.identity, transform.parent);
                    east.GetComponent<ChunkGenerator>().west = gameObject;
                }if (west == null){
                    west = Instantiate(chunk, new Vector3(transform.position.x - bgWidth, transform.position.y), Quaternion.identity, transform.parent);
                    west.GetComponent<ChunkGenerator>().east = gameObject;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collided) {
            if (collided.gameObject.tag == "Loader"){
                renderer.enabled = false;
            }
        }
    }
}