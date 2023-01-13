using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLoader : MonoBehaviour
{
    //a field for the gameobject of every upgrade in the game

    private List<GameObject> upgradeList = new List<GameObject>();
    private bool listSet = false;

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

        //List<GameObject> result = new List<GameObject>();
        return new List<GameObject> { upgradeList[0], upgradeList[1], upgradeList[2] };
    }
}
