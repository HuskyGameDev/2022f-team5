using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public SwingAttackScript swing;
    private float fixedDeltaTime;
    public static bool Paused = false;
    private List<UpgradeData> upgradeList = new List<UpgradeData>();
    Random rnd = new Random();

    public Button slot1;
    public Button slot2;
    public Button slot3;
    private List<UpgradeData> currentUpgrades = new List<UpgradeData>();

    public GameObject hp;
    public GameObject exp;
    public GameObject volume;

    public TextMeshPro upgrd1;
    public TextMeshPro upgrd2;
    public TextMeshPro upgrd3;

    public TextMeshProUGUI hoverText;

    public AudioSource levelUpSound;
    public AudioClip[] playerSounds;

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
        if (!levelUpSound.isPlaying)
        {
            levelUpSound.clip = playerSounds[0];
            levelUpSound.Play();
        }

        //pause time while this is enabled
        Time.timeScale = 0f;
        //AudioListener.pause = true;
        Paused = true;

        setList();

        List<UpgradeData> selectionList = new List<UpgradeData>();
        for (int i = 0; i < upgradeList.Count; i++) selectionList.Add(upgradeList[i]);
        currentUpgrades = new List<UpgradeData> {getUpgradeByWeight(selectionList), getUpgradeByWeight(selectionList), getUpgradeByWeight(selectionList)};
        assignUpgrade(slot1, currentUpgrades[0]);
        assignUpgrade(slot2, currentUpgrades[1]);
        assignUpgrade(slot3, currentUpgrades[2]);

        upgrd1.text = currentUpgrades[0].description;
        upgrd2.text = currentUpgrades[1].description;
        upgrd3.text = currentUpgrades[2].description;

        hp.GetComponent<BoxCollider2D>().enabled = true;
        exp.GetComponent<BoxCollider2D>().enabled = true;
        volume.GetComponent<BoxCollider2D>().enabled = true;

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

        upgrd1.text = "";
        upgrd2.text = "";
        upgrd3.text = "";
        hoverText.text = "";

        foreach (GameObject upObj in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            upObj.GetComponent<UpgradeData>().resetWeight();            
        }

        hp.GetComponent<BoxCollider2D>().enabled = false;
        exp.GetComponent<BoxCollider2D>().enabled = false;
        volume.GetComponent<BoxCollider2D>().enabled = false;

    }

    private void setList()
    {
        foreach (GameObject upObj in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            upgradeList.Add(upObj.GetComponent<UpgradeData>());
            //Debug.Log(upObj + " : Look Here");
        }
    }

    //Currently unused. I'm leaving this here in case we need to go back to it in the future
    /*
    public List<UpgradeData> GetUpgrades()
    {
        if (!listSet)
        {
            setList();
        }

        Debug.Log(upgradeList.Count);

        Debug.Log("Before shuffle");
        //shuffle();
        Debug.Log("After shuffle");

        return new List<UpgradeData> { upgradeList[0], upgradeList[1], upgradeList[2] };
    }
    */

    //Currently unused. I'm leaving this here in case we need to go back to it in the future
    /*
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
    */

    //This method will (hopefully) replace shuffle
    private UpgradeData getUpgradeByWeight(List<UpgradeData> upgrades){
        int totalWeight = 0;
        for (int j = 0; j < upgrades.Count; j++) totalWeight += upgrades[j].defaultWeight;
        int weightTarget = rnd.Next(0, totalWeight);
        int currentWeight = 0;
        int i = 0;
        for (; i < upgrades.Count; i++){
            currentWeight += upgrades[i].defaultWeight;
            if (weightTarget < currentWeight) break;
        }
        UpgradeData returnable = upgrades[i];
        upgrades.RemoveAt(i);
        return returnable;
    }

    private void assignUpgrade(Button butt, UpgradeData upgrade)
    {
        switch(upgrade.name)
        {
            case "BaseAtkSpeed":
                butt.onClick.AddListener(Up_BaseAtkSpeed);
                break;
            case "BaseAtkDamage":
                butt.onClick.AddListener(Up_BaseAtkDamage);
                break;
            case "IncHealth":
                butt.onClick.AddListener(Up_IncHealth);
                break;
            case "BaseAtkSpeedSpid":
                butt.onClick.AddListener(Up_BaseAtkSpeedSpid);
                break;
            case "BaseAtkDamageSkele":
                butt.onClick.AddListener(Up_BaseAtkDamageSkele);
                break;
            case "IncHealthZomb":
                butt.onClick.AddListener(Up_IncHealthZomb);
                break;
            case "IncSpeed":
                butt.onClick.AddListener(Up_IncSpeed);
                break;
            case "MoreShots":
                butt.onClick.AddListener(Up_MoreShots);
                break;
            case "SwingAtk":
                butt.onClick.AddListener(Up_SwingAtk);
                break;
            case "SwingAtkSpeed":
                butt.onClick.AddListener(Up_SwingAtkSpeed);
                break;
            case "SwingAtkDamage":
                butt.onClick.AddListener(Up_SwingAtkDamage);
                break;
            case "SwingAtkSize":
                butt.onClick.AddListener(Up_SwingAtkSize);
                break;
            case "Bonemerang":
                butt.onClick.AddListener(Up_Bonemerang);
                break;
            case "BoneAtkDamage":
                butt.onClick.AddListener(Up_BoneAtkDamage);
                break;
            case "BoneAtkAS":
                butt.onClick.AddListener(Up_BoneAttackAS);
                break;
        }
        butt.GetComponent<Image>().sprite = upgrade.image;
    }

    private void unassignUpgrade(Button butt, UpgradeData upgrade)
    {
        butt.onClick.RemoveAllListeners();

        /**
        switch (upgrade.name)
        {
            case "BaseAtkSpeed":
                butt.onClick.RemoveListener(Up_BaseAtkSpeed);
                break;
            case "BaseAtkDamage":
                butt.onClick.RemoveListener(Up_BaseAtkDamage);
                break;
            case "IncHealth":
                butt.onClick.RemoveListener(Up_IncHealth);
                break;
            case "BaseAtkSpeedSpid":
                butt.onClick.RemoveListener(Up_BaseAtkSpeedSpid);
                break;
            case "BaseAtkDamageSkele":
                butt.onClick.RemoveListener(Up_BaseAtkDamageSkele);
                break;
            case "IncHealthZomb":
                butt.onClick.RemoveListener(Up_IncHealthZomb);
                break;
            case "IncSpeed":
                butt.onClick.RemoveListener(Up_IncSpeed);
                break;
            case "MoreShots":
                butt.onClick.RemoveListener(Up_MoreShots);
                break;
            case "SwingAtk":
                butt.onClick.RemoveListener(Up_SwingAtk);
                break;
            case "SwingAtkSpeed":
                butt.onClick.RemoveListener(Up_SwingAtkSpeed);
                break;
            case "SwingAtkDamage":
                butt.onClick.RemoveListener(Up_SwingAtkDamage);
                break;
            case "SwingAtkSize":
                butt.onClick.RemoveListener(Up_SwingAtkSize);
                break;
            case "Bonemerang":
                butt.onClick.RemoveListener(Up_Bonemerang);
                break;
            case "BoneAtkDamage":
                butt.onClick.RemoveListener(Up_BoneAtkDamage);
                break;
            case "BoneAtkAS":
                butt.onClick.RemoveListener(Up_BoneAttackAS);
                break;
        }
        */
    }

    //============================================================================
    //-------------------- All Upgrade Methods go after here ---------------------
    //============================================================================
    public GameObject player;

    //name : BaseAtkSpeed
    public void Up_BaseAtkSpeed()
    {
        player.GetComponent<Weapons>().baseAttackAS /= 1.4f;
    }

    //name : BaseAtkSpeedSpid
    public void Up_BaseAtkSpeedSpid()
    {
        player.GetComponent<Weapons>().baseAttackAS /= 1.65f;
    }

    //name : BaseAtkDamage
    private float damageInc = 1.6f;
    private float damageIncSkele = 1.9f;
    public void Up_BaseAtkDamage()
    {
        player.GetComponent<Weapons>().baseAttackBaseDam *= damageInc;
    }

    //name : BaseAtkDamageSkele
    public void Up_BaseAtkDamageSkele()
    {
        player.GetComponent<Weapons>().baseAttackBaseDam *= damageIncSkele;
    }

    //name : IncHealth
    public void Up_IncHealth()
    {
        Player play = player.GetComponent<Player>();
        float proportion = play.pHealth / play.pMaxHealth;
        play.pMaxHealth += 100f; //<---------------------------value
        play.pHealth = play.pMaxHealth * proportion;
        play.healthBar.UpdateHealthBarMax(play.pMaxHealth, 100);
        play.healthBar.UpdateHealthBar(play.pHealth);
    }

    //name : IncHealthZomb
    public void Up_IncHealthZomb()
    {
        Player play = player.GetComponent<Player>();
        float proportion = play.pHealth / play.pMaxHealth;
        play.pMaxHealth += 200f; //<---------------------------value
        play.pHealth = play.pMaxHealth * proportion;
        play.healthBar.UpdateHealthBarMax(play.pMaxHealth, 200f);
        play.healthBar.UpdateHealthBar(play.pHealth);
    }

    //name : MoreShots
    public void Up_MoreShots()
    {
        player.GetComponent<Weapons>().baseAttackNumShots++;
    }

    //name : IncSpeed
    public void Up_IncSpeed()
    {
        Player play = player.GetComponent<Player>();
        play.moveSpeed += 0.8f;
    }

    public void Up_SwingAtk()
    {
        swing.gameObject.SetActive(true);
        meleeUp1.SetActive(true);
        meleeUp2.SetActive(true);
        meleeUp3.SetActive(true);
        Destroy(meleeUp0);
    }

    public GameObject meleeUp0;
    public GameObject meleeUp1;
    public GameObject meleeUp2;
    public GameObject meleeUp3;

    public void Up_SwingAtkSpeed()
    {
        swing.SwingAtkAS /= 1.8f;
    }

    private float swingDamageInc = 1.7f;
    public void Up_SwingAtkDamage()
    {
        swing.SwingAtkDamage *= swingDamageInc;
    }

    public void Up_SwingAtkSize()
    {
        swing.size.x *= 1.3f;
        swing.size.y *= 1.3f;
        //increase the sprite size
    }

    public GameObject boneUp1;
    public GameObject boneUp2;
    public void Up_Bonemerang()
    {
        player.GetComponent<Weapons>().gainBoomerang();
        Destroy(GameObject.Find("Bonemerang"));
        boneUp1.SetActive(true);
        boneUp2.SetActive(true);
    }

    public void Up_BoneAtkDamage()
    {
        Weapons.boneAttackBaseDam *= damageInc;
    }

    public void Up_BoneAttackAS()
    {
        player.GetComponent<Weapons>().boneAttackAS /= 1.8f;
    }
}
