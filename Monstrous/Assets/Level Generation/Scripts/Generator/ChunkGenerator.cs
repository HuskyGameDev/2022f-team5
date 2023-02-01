using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class ChunkGenerator : MonoBehaviour{

        private int chunkWidth;
        private int chunkHeight;
        private int textureWidth;
        private int textureHeight;
        private float offsetX;
        private float offsetY;
        private float scale;
        private DataHolder data;
        private Texture2D background;

        private void initialize(){
            background = new Texture2D(chunkWidth * textureWidth, chunkHeight * textureHeight);
            background.filterMode = FilterMode.Point;
            generateImage();
        }

        public void setVariables(int chunkWidth, int chunkHeight, int textureWidth, int textureHeight, float offsetX, float offsetY, float scale, DataHolder data){
            this.chunkWidth = chunkWidth;
            this.chunkHeight = chunkHeight;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.scale = scale;
            this.data = data;
            initialize();
        }

        public void generateImage(){
            for (int i = 0; i < chunkWidth; i++){
                for (int j = 0; j < chunkHeight; j++){
                    float noise = Mathf.PerlinNoise(((transform.position.x + (i - (int) (chunkWidth / 2))) + offsetX) / scale, ((transform.position.y + (j - (int) (chunkHeight / 2))) + offsetY) / scale);
                    Texture2D texture = getSprite(noise).texture;
                    for (int k = 0; k < textureWidth; k++){
                        for (int l = 0; l < textureHeight; l++){
                            background.SetPixel(k + (textureWidth * i), l + (textureHeight * j), texture.GetPixel(k, l));
                        }
                    }
                }
            }
            background.Apply();
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f), textureWidth);
        }

        private Sprite getSprite(float value){
            Sprite sprite;
            if (value > 0.8f){
                sprite = data.pathTiles[Random.Range(0, data.pathTiles.Length - 1)];
            }else if (value > 0.6f){
                sprite = data.floorTiles[1];
            }else{
                sprite = data.grassTiles[Random.Range(0, data.grassTiles.Length - 1)];
            }
            return sprite;
        }
    }
}