using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private float fixedDeltaTime;
    public static bool Paused = false;

    public Transform frame;
    public Transform reserve;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

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
        slot2.transform.SetParent(frame);
        slot3.transform.SetParent(frame);

        slot1.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, -230f, 900f);
        slot1.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 50f, 170f);

        slot2.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, -230f, 900f);
        slot2.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 295f, 170f);

        slot3.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, -230f, 900f);
        slot3.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 50f, 170f);
    }
}
