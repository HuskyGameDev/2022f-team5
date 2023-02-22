using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class ChunkGenerator : MonoBehaviour{
        private GameObject structure;
        private ChunkController controller;
        private int chunkWidth;
        private int chunkHeight;
        private int textureWidth;
        private int textureHeight;
        private float offsetX;
        private float offsetY;
        private float scale;
        private Texture2D background;

        private void initialize(){
            background = new Texture2D(chunkWidth * textureWidth, chunkHeight * textureHeight);
            background.filterMode = FilterMode.Point;
            generateImage();
        }

        public void setVariables(int chunkWidth, int chunkHeight, int textureWidth, int textureHeight, float offsetX, float offsetY, float scale, ChunkController controller){
            this.chunkWidth = chunkWidth;
            this.chunkHeight = chunkHeight;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.scale = scale;
            this.controller = controller;
            initialize();
        }

        public void move(Vector3 newLoc){
            if (structure != null) Destroy(structure);
            transform.position = newLoc;
            generateImage();
        }

        public void generateImage(){
            for (int i = 0; i < chunkWidth; i++){
                for (int j = 0; j < chunkHeight; j++){
                    float noise = Mathf.PerlinNoise(((transform.position.x + (i - (int) (chunkWidth / 2))) + offsetX) / scale, ((transform.position.y + (j - (int) (chunkHeight / 2))) + offsetY) / scale);
                    Texture2D texture = getSprite(noise, (int) (transform.position.x + (i - (int) (chunkWidth / 2))), (int) (transform.position.y + (j - (int) (chunkHeight / 2)))).texture;
                    for (int k = 0; k < textureWidth; k++){
                        for (int l = 0; l < textureHeight; l++){
                            background.SetPixel(k + (textureWidth * i), l + (textureHeight * j), texture.GetPixel(k, l));
                        }
                    }
                }
            }
            background.Apply();
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f), textureWidth);
            structureGenerator((int) transform.position.x, (int) transform.position.y);
        }

        private Sprite getSprite(float value, int x, int y){
            Sprite sprite;
            Biome biome = controller.getBiome(x, y);
            if (value > 0.8f){
                sprite = biome.pathTiles[Random.Range(0, biome.pathTiles.Length - 1)];
            }else if (value > 0.6f){
                sprite = biome.secondaryTiles[Random.Range(0, biome.secondaryTiles.Length - 1)];
            }else{
                sprite = biome.grassTiles[Random.Range(0, biome.grassTiles.Length - 1)];
            }
            return sprite;
        }

        private void structureGenerator(int x, int y){
            float num = 5 * Mathf.Cos(x + offsetX) + 2 * Mathf.Tan(y + offsetY); //Mathf.Tan(Mathf.Pow(y + offsetY, 2) - Mathf.Pow(x + offsetX, 2));
            if (num != 0 && num < controller.structureFrequency / 20 && num > -controller.structureFrequency / 20){
                System.Random prng = new System.Random((int) (num * 10));
                Biome biome = controller.getBiome(x, y);
                int totalWeight = 0;
                foreach (int weight in biome.structureWeights) totalWeight += weight;
                int weightTarget = prng.Next(0, totalWeight);
                int currentWeight = 0;
                int i;
                for (i = 0; i < biome.structureWeights.Length; i++){
                    currentWeight += biome.structureWeights[i];
                    if (weightTarget < currentWeight) break;
                }
                structure = Instantiate(biome.structures[i], transform.position, Quaternion.identity, transform.parent);
            }
        }
    }
}