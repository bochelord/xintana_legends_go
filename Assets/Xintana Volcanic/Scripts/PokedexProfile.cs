using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PokedexProfile : MonoBehaviour {

    public Dictionary<EnemyType, bool> enemiesKnown = new Dictionary<EnemyType, bool>();
    public int lastEnemyWorld;

    public PokedexProfile GeneratePokedex()
    {
        PokedexProfile profile = new PokedexProfile();
        profile.lastEnemyWorld = 1;

        EnemyType[] _count = (EnemyType[])System.Enum.GetValues(typeof(EnemyType));

        for (int i = 0; i < _count.Length; i++)
        {
            profile.enemiesKnown.Add(_count[i], false);
        }
        return profile;
    }
}
