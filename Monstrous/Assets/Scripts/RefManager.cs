using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefManager : MonoBehaviour
{
    static RefManager instance;
    void Awake() { instance = this; }

    public static GameObject playerRef;
}
