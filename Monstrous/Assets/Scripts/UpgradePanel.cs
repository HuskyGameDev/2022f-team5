using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UpgradePanel : MonoBehaviour
{
    private float fixedDeltaTime;
    public static bool Paused = false;
    private List<string> upgradeList = new List<string>();
    private bool listSet = false;
    Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //pause time while this is enabled
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0f;
        Paused = true;
    }

    //when this panel is awakened by the player leveling up, generate an upgrade in each slot
    void OnEnable()
    {
        //pause time while this is enabled
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Paused = true;

        Debug.Log(GetUpgrades());
    }

    void OnDisable()
    {
        //restart time when disabled
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Paused = false;

        //remove listeners
    }

    private void setList()
    {
        foreach (GameObject upObj in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            upgradeList.Add(upObj.transform.name);
            Debug.Log(upObj + " : Look Here");
        }
        listSet = true;
    }

    public List<string> GetUpgrades()
    {
        if (!listSet)
        {
            setList();
        }

        Debug.Log(upgradeList.Count);

        Debug.Log("Before shuffle");
        shuffle();
        Debug.Log("After shuffle");

        return new List<string> { upgradeList[0], upgradeList[1], upgradeList[2] };
    }

    private void shuffle()
    {
        int n = upgradeList.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            string value = upgradeList[k];
            upgradeList[k] = upgradeList[n];
            upgradeList[n] = value;
        }
    }
}
