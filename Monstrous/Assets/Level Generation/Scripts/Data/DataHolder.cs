using UnityEngine;

namespace Monstrous.Data{
    public class DataHolder : MonoBehaviour{
        public Biome[] biomes;
    }

    [System.Serializable]
    public struct Biome{
        public string biomeID;
        public Sprite[] grassTiles;
        public Sprite[] secondaryTiles;
        public Sprite[] pathTiles;
        public string[] enemies;
    }
}