using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyType { zazuc, makula, ball, kogi, blackKnight, lavabeast, alchemist, devil, explorer}


public class EnemyStructure {

    public EnemyType type;
    public int level, minLevel, maxLevel;
    public float life, lifeBase, lifeGrowth;
    public float damage, damageBase, damageGrowth;
    public float dna_hue, dna_colorsat, dna_brightness;


    public EnemyStructure GenerateBasic(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.zazuc:
                this.type = EnemyType.zazuc;
                lifeBase = 3f;
                lifeGrowth = 0.06f;
                damageBase = 2f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.makula:
                this.type = EnemyType.makula;
                lifeBase = 7f;
                lifeGrowth = 0.08f;
                damageBase = 5f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.kogi:
                this.type = EnemyType.kogi;
                lifeBase = 2f;
                lifeGrowth = 0.07f;
                damageBase = 1f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.blackKnight:
                this.type = EnemyType.blackKnight;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.lavabeast:
                this.type = EnemyType.lavabeast;
                lifeBase = 5f;
                lifeGrowth = 0.07f;
                damageBase = 4f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.alchemist:
                this.type = EnemyType.alchemist;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.devil:
                this.type = EnemyType.devil;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.1f;
                break;
            case EnemyType.explorer:
                this.type = EnemyType.explorer;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.1f;
                break;
            default:
                Debug.LogError("This Enemy doesn't exists!");
                break;
        }


        EnemyStructure retEnemy = new EnemyStructure();
        retEnemy.type = this.type;
        retEnemy.level = 1;
        retEnemy.minLevel = 1;
        retEnemy.maxLevel = 50;
        retEnemy.lifeBase = lifeBase;
        retEnemy.lifeGrowth = lifeGrowth;
        retEnemy.life = Mathf.RoundToInt(Formulas.ExponentialGrowth(retEnemy.lifeBase, retEnemy.lifeGrowth, retEnemy.level - 1));
        retEnemy.damageBase = damageBase;
        retEnemy.damageGrowth = damageGrowth;
        retEnemy.damage = Formulas.ExponentialGrowth(retEnemy.damageBase, retEnemy.damageGrowth, retEnemy.level - 1);

        ///Genotype DNA for HSV spriteFX
        retEnemy.dna_hue = UnityEngine.Random.Range(0f, 360f);
        retEnemy.dna_colorsat = UnityEngine.Random.Range(-2f, 2f);
        retEnemy.dna_brightness = UnityEngine.Random.Range(0.50f, 2f);

        return retEnemy;
    }

    public EnemyStructure GenerateWithLevel(EnemyType enemyType, int level)
    {
        EnemyStructure retEnemy = new EnemyStructure().GenerateBasic(enemyType);
        retEnemy.level = level;
        retEnemy.life = Mathf.RoundToInt(Formulas.ExponentialGrowth(retEnemy.lifeBase, retEnemy.lifeGrowth, retEnemy.level - 1));
        retEnemy.damage = Formulas.ExponentialGrowth(retEnemy.damageBase, retEnemy.damageGrowth, retEnemy.level - 1);

        return retEnemy;
    }

    public EnemyStructure GenerateRandomEnemyWithLevel(int level)
    {
        Array a = Enum.GetValues(typeof(EnemyType));
        EnemyType randomEnemyType = (EnemyType)a.GetValue(UnityEngine.Random.Range(0, a.Length));

        EnemyStructure retEnemy = new EnemyStructure().GenerateWithLevel(randomEnemyType, level);
        return retEnemy;
    }

}
