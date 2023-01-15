using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generator{
    public class SpritePicker : MonoBehaviour{
        
        public string spriteType;
        private DataHolder data;
        private Sprite[] sprites;
        public int[] weights;
        private int weightTotal = 0;
        [SerializeField] private SpriteRenderer renderer;

        // Start is called before the first frame update
        void Start(){
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            foreach (int nextWeight in weights){
                weightTotal += nextWeight;
            }
            switch (spriteType){
                case "floor":
                    sprites = data.floorTiles;
                    break;
                case "wall":
                    sprites = data.wallTiles;
                    break;
            }
            int weight = Random.Range(0, weightTotal + 1);
            int index = 0;
            int currentWeight = 0;
            while (weight >= currentWeight && weight > currentWeight + weights[index] && weight <= weightTotal || weights[index] == 0){
                currentWeight += weights[index];
                if (index + 1 < weights.Length) index++;
            }
            renderer.sprite = sprites[index];
            Destroy(this);
        }
    }
}