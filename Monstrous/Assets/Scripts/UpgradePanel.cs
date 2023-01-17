using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private float fixedDeltaTime;
    public static bool Paused = false;

    public Transform frame;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    private Vector2 pos1 = new Vector2(0, 130);
    private Vector2 pos2 = new Vector2(0, 0);
    private Vector2 pos3 = new Vector2(0, -130);


    public UpgradeLoader loader;

    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0f;
        Paused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //when this panel is awakened by the player leveling up, generate an upgrade in each slot
    void OnEnable()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Paused = true;

        placeUpgrades( loader.GetUpgrades() );
        
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void placeUpgrades(List<GameObject> slots)
    {
        slot1 = slots[0];
        slot2 = slots[1];
        slot3 = slots[2];

        slot1.transform.SetParent(frame);
        slot1.GetComponent<RectTransform>().anchoredPosition = pos1;
        slot2.transform.SetParent(frame);
        slot2.GetComponent<RectTransform>().anchoredPosition = pos2;
        slot3.transform.SetParent(frame);
        slot3.GetComponent<RectTransform>().anchoredPosition = pos3;
    }
}
