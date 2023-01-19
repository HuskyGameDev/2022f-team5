using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.Data;

namespace Monstrous.Generation{
    public class LevelGenerator : MonoBehaviour{

        public int seed;
        private DataHolder data;
        private Player player;
        private System.Random prng;
        private float offsetX;
        private float offsetY;
        [SerializeField] Vector2 movement = new Vector2();

        // Start is called before the first frame update
        void Start(){
            prng = new System.Random(seed);
            offsetX = prng.Next(-100000, 100000);
            offsetY = prng.Next(-100000, 100000);
            data = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            generate();
        }

        void FixedUpdate(){
            movement += player.movement * player.moveSpeed * Time.fixedDeltaTime;
            while (movement.x > 1){
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                movement.x -= 1;
                //generate();
            }if (movement.x < 1){
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                movement.x += 1;
                //generate();
            }if (movement.y > 1){
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                movement.y -= 1;
                //generate();
            }if (movement.y < 1){
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                movement.y += 1;
                //generate();
            }
        }

        public void generate(){
            foreach (Transform loc in transform){
                float noise = Mathf.PerlinNoise((loc.position.x + offsetX) / 5, (loc.position.y + offsetY) / 5);
                Debug.Log((loc.position.x + offsetX));
                if (noise > 0.9f){
                    loc.gameObject.GetComponent<SpriteRenderer>().sprite = data.floorTiles[0];
                }else if (noise > 0.75f){
                    loc.gameObject.GetComponent<SpriteRenderer>().sprite = data.floorTiles[1];
                }else{
                    loc.gameObject.GetComponent<SpriteRenderer>().sprite = data.floorTiles[2];
                }
            }
        }
    }
}