using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UpgradePanel : MonoBehaviour
{
    private float fixedDeltaTime;
    public static bool Paused = false;
    private List<UpgradeData> upgradeList = new List<UpgradeData>();
    private bool listSet = false;
    Random rnd = new Random();

    public Button slot1;
    public Button slot2;
    public Button slot3;
    private List<UpgradeData> currentUpgrades = new List<UpgradeData>();

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

        List<UpgradeData> selectionList = new List<UpgradeData>(currentUpgrades);
        List<int> selectionWeights = new List<int>();
        for (int i = 0; i < currentUpgrades.Count; i++) selectionWeights.Add(1);
        //currentUpgrades = GetUpgrades();
        Debug.Log(currentUpgrades);
        // assignUpgrade(slot1, currentUpgrades[0]);
        // assignUpgrade(slot2, currentUpgrades[1]);
        // assignUpgrade(slot3, currentUpgrades[2]);
        assignUpgrade(slot1, getUpgradeByWeight(selectionList, selectionWeights));
        assignUpgrade(slot2, getUpgradeByWeight(selectionList, selectionWeights));
        assignUpgrade(slot3, getUpgradeByWeight(selectionList, selectionWeights));
    }

    void OnDisable()
    {
        //restart time when disabled
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Paused = false;

        //remove listeners
        unassignUpgrade(slot1, currentUpgrades[0]);
        unassignUpgrade(slot2, currentUpgrades[1]);
        unassignUpgrade(slot3, currentUpgrades[2]);
    }

    private void setList()
    {
        foreach (GameObject upObj in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            upgradeList.Add(new UpgradeData(upObj.transform.name, upObj.GetComponent<Image>().sprite) );
            Debug.Log(upObj + " : Look Here");
        }
        listSet = true;
    }

    public List<UpgradeData> GetUpgrades()
    {
        if (!listSet)
        {
            setList();
        }

        Debug.Log(upgradeList.Count);

        Debug.Log("Before shuffle");
        shuffle();
        Debug.Log("After shuffle");

        return new List<UpgradeData> { upgradeList[0], upgradeList[1], upgradeList[2] };
    }

    private void shuffle()
    {
        int n = upgradeList.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            UpgradeData value = upgradeList[k];
            upgradeList[k] = upgradeList[n];
            upgradeList[n] = value;
        }
    }

    //This method will (hopefully) replace shuffle
    private UpgradeData getUpgradeByWeight(List<UpgradeData> upgrades, List<int> weights){
        Debug.Log("Starting getUpgradeByWeight");
        int totalWeight = 0;
        foreach (int weight in weights) totalWeight += weight;
        int weightTarget = rnd.Next(0, totalWeight);
        int currentWeight = 0;
        int i;
        Debug.Log("Beginning of for loop");
        for (i = 0; i < upgrades.Count; i++){
            currentWeight += weights[i];
            if (weightTarget < currentWeight) break;
        }
        Debug.Log("End of for loop");
        Debug.Log("returnable");
        UpgradeData returnable = upgrades[i];
        Debug.Log("upgrades");
        upgrades.RemoveAt(i);
        Debug.Log("weights");
        weights.RemoveAt(i);
        return returnable;
    }

    private void assignUpgrade(Button butt, UpgradeData upgrade)
    {
        switch(upgrade.name)
        {
            case "AttackSpeed":
                butt.onClick.AddListener(UpAttackSpeed);
                break;
            case "BaseAtkDamage":
                butt.onClick.AddListener(UpBaseAtkDamage);
                break;
            case "IncHealth":
                butt.onClick.AddListener(UpIncHealth);
                break;
            case "IncSpeed":
                butt.onClick.AddListener(UpIncSpeed);
                break;
            case "MoreShots":
                butt.onClick.AddListener(UpMoreShots);
                break;
        }
        butt.GetComponent<Image>().sprite = upgrade.image;
    }

    private void unassignUpgrade(Button butt, UpgradeData upgrade)
    {
        switch (upgrade.name)
        {
            case "AttackSpeed":
                butt.onClick.RemoveListener(UpAttackSpeed);
                break;
            case "BaseAtkDamage":
                butt.onClick.RemoveListener(UpBaseAtkDamage);
                break;
            case "IncHealth":
                butt.onClick.RemoveListener(UpIncHealth);
                break;
            case "IncSpeed":
                butt.onClick.RemoveListener(UpIncSpeed);
                break;
            case "MoreShots":
                butt.onClick.RemoveListener(UpMoreShots);
                break;
        }
    }

    //============================================================================
    //-------------------- All Upgrade Methods go after here ---------------------
    //============================================================================
    public GameObject player;

    //name : AttackSpeed
    private float ASInc = 5;
    public void UpAttackSpeed()
    {
        player.GetComponent<Weapons>().baseAttackAS /= 1.1f;
    }

    //name : BaseAtkDamage
    private float damageInc = 5;
    public void UpBaseAtkDamage()
    {
        player.GetComponent<Weapons>().baseAttackBaseDam += damageInc;
    }

    //name : IncHealth
    private float healthInc = 20;
    public void UpIncHealth()
    {
        Player play = player.GetComponent<Player>();
        float proportion = play.pHealth / play.pMaxHealth;
        play.pMaxHealth += healthInc;
        play.pHealth = play.pMaxHealth * proportion;
        play.healthBar.UpdateHealthBar(play.pHealth);
    }

    //name : MoreShots
    public void UpMoreShots()
    {
        player.GetComponent<Weapons>().baseAttackNumShots++;
    }

    //name : IncSpeed
    private float speedInc = .2f;
    public void UpIncSpeed()
    {
        Player play = player.GetComponent<Player>();
        play.moveSpeed += speedInc;
    }
}
