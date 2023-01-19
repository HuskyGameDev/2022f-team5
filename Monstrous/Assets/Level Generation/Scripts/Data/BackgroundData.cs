using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.Data{
    [System.Serializable]
    public struct Nodes{
        [SerializeField] public GameObject[] row;
    }

    public class BackgroundData : MonoBehaviour{
        public Nodes[] rows;
        public Nodes[] columns;
    }
}