using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class XintanaEnemiesBestiary : ScriptableObject {

    [System.Serializable]
    public class XintanaEnemy
    {
        public string nameId;
        public EnemyType type;
        public string description;
        public int[] appearsInWorld;
        public GameObject prefab;

        public int level, minLevel, maxLevel;
        public float life, lifeBase, lifeGrowth;
        public float damage, damageBase, damageGrowth;
        public float dna_hue, dna_colorsat, dna_brightness;
        public int score;
        public float xp, xpBase, xpGrowth;
    }

    [Header("Xintana Enemies List")]
    public List<XintanaEnemy> xintanaEnemies = new List<XintanaEnemy>();
}
