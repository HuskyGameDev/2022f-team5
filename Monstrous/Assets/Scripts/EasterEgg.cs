using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    [SerializeField] private int minDogs = 3;
    [SerializeField] private int maxDogs = 30;
    private ArrayList inputs = new ArrayList();
    private string[] key = {"up", "up", "down", "down", "left", "right", "left", "right", "b", "a"};

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("up")){
            inputs.Add("up");
        }else if(Input.GetKeyDown("down")){
            inputs.Add("down");
        }else if (Input.GetKeyDown("left")){
            inputs.Add("left");
        }else if (Input.GetKeyDown("right")){
            inputs.Add("right");
        }else if (Input.GetKeyDown("a")){
            inputs.Add("a");
        }else if (Input.GetKeyDown("b")){
            inputs.Add("b");
        }
        while (inputs.Count > 10){
            inputs.RemoveAt(0);
        }
        int matches = 0;
        for (int i = 0; i < inputs.Count; i++){
            if (inputs[i] == key[i]) matches++;
        }if (matches == 10){
            inputs.RemoveRange(0, 9);
            for (int i = 0; i < Random.Range(minDogs, maxDogs); i++){
                Instantiate(egg, new Vector3(-10, Random.Range(-4.3f, 4.3f), 0), Quaternion.identity);
            }
        }
    }
}
