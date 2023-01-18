using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class LevelGenerator : MonoBehaviour{

        public int seed;
        public Transform[] tiles;
        private DataHolder data;

        // Start is called before the first frame update
        void Start(){
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            generate();
        }

        public void generate(){
            System.Random prng = new System.Random(seed);
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            foreach (Transform loc in tiles){
                float noise = Mathf.PerlinNoise((loc.position.x + offsetX) * 5, (loc.position.y + offsetY) * 5);
                if (noise > 0.6f){
                    Instantiate(data.floorTiles[2], loc.position, Quaternion.identity, transform);
                }
                if (noise > 0.4f){
                    Instantiate(data.floorTiles[1], loc.position, Quaternion.identity, transform);
                }else{
                    Instantiate(data.floorTiles[0], loc.position, Quaternion.identity, transform);
                }
            }
        }
    }
}