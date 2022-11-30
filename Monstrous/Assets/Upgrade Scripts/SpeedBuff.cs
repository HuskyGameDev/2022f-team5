using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    public float speedInc = .2f;

    public void Upgrade(GameObject target)
    {
        Player play = target.GetComponent<Player>();
        play.moveSpeed += speedInc;
    }
}
