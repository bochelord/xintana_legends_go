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
        public int appearsInWorld;
        public GameObject prefab;
    }

    [Header("Xintana Enemies List")]
    public List<XintanaEnemy> xintanaEnemies = new List<XintanaEnemy>();
}
