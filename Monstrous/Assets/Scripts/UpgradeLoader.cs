using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class UpgradeLoader : MonoBehaviour
{
    //a field for the gameobject of every upgrade in the game

    private List<GameObject> upgradeList = new List<GameObject>();
    private bool listSet = false;
    Random rnd = new Random();

    void Start()
    {
        
    }

    private void setList()
    {
        foreach (GameObject upObj in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            upgradeList.Add(upObj);
            Debug.Log(upObj + " : Look Here");
        }
        listSet = true;
    }

    public List<GameObject> GetUpgrades()
    {
        if(!listSet)
        {
            setList();
        }

        shuffle();

        return new List<GameObject> { upgradeList[0], upgradeList[1], upgradeList[2] };
    }

    private void shuffle()
    {
        int n = upgradeList.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            GameObject value = upgradeList[k];
            upgradeList[k] = upgradeList[n];
            upgradeList[n] = value;
        }
    }
}
