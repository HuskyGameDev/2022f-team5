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
            foreach (Transform loc in tiles){
                Instantiate(data.floorTiles[Random.Range(0, data.floorTiles.Length)], loc.position, Quaternion.identity, transform);
            }
        }
    }
}