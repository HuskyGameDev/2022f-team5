using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.Data{
    //This struct is to allow us to edit the values of a 2D array in the inspector
    [System.Serializable]
    public struct Nodes{
        [SerializeField] public GameObject[] row;
    }

    public class BackgroundData : MonoBehaviour{
        [Header("Dimensions")]
        public int width = 35;
        public int height = 21;

        [Header("Nodes")]
        public Nodes[] rows;
        public Nodes[] columns;
    }
}