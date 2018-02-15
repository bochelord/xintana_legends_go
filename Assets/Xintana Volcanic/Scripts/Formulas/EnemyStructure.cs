using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyType { zazuc, makula, ball, kogi, blackKnight, lavabeast, alchemist, devil, explorer, fireMage, iceBeast, skeletonMage}


public class EnemyStructure {

    public EnemyType type;
    public int level, minLevel, maxLevel;
    public float life, lifeBase, lifeGrowth;
    public float damage, damageBase, damageGrowth;
    public float dna_hue, dna_colorsat, dna_brightness;
    public int score;
    public float xp, xpBase, xpGrowth;

    public EnemyStructure GenerateBasic(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.zazuc:
                this.type = EnemyType.zazuc;
                lifeBase = 3f;
                lifeGrowth = 0.06f;
                damageBase = 2f;
                damageGrowth = 0.08f;
                score = 1500;
                break;
            case EnemyType.makula:
                this.type = EnemyType.makula;
                lifeBase = 7f;
                lifeGrowth = 0.07f;
                damageBase = 10f;
                damageGrowth = 0.08f;
                score = 2750;
                break;
            case EnemyType.kogi:
                this.type = EnemyType.kogi;
                lifeBase = 2f;
                lifeGrowth = 0.07f;
                damageBase = 1f;
                damageGrowth = 0.09f;
                score = 1000;
                break;
            case EnemyType.blackKnight:
                this.type = EnemyType.blackKnight;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.08f;
                score = 1750;
                break;
            case EnemyType.lavabeast:
                this.type = EnemyType.lavabeast;
                lifeBase = 5f;
                lifeGrowth = 0.07f;
                damageBase = 4f;
                damageGrowth = 0.09f;
                score = 2000;
                break;
            case EnemyType.alchemist:
                this.type = EnemyType.alchemist;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.1f;
                score = 2000;
                break;
            case EnemyType.devil:
                this.type = EnemyType.devil;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.09f;
                score = 2100;
                break;
            case EnemyType.explorer:
                this.type = EnemyType.explorer;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3f;
                damageGrowth = 0.09f;
                score = 2200;
                break;
            case EnemyType.fireMage:
                this.type = EnemyType.fireMage;
                lifeBase = 5f;
                lifeGrowth = 0.07f;
                damageBase = 3.5f;
                damageGrowth = 0.1f;
                score = 2300;
                break;
            case EnemyType.iceBeast:
                this.type = EnemyType.iceBeast;
                lifeBase = 4f;
                lifeGrowth = 0.07f;
                damageBase = 3.5f;
                damageGrowth = 0.085f;
                score = 2200;
                break;
            case EnemyType.skeletonMage:
                this.type = EnemyType.skeletonMage;
                lifeBase = 3f;
                lifeGrowth = 0.07f;
                damageBase = 2.75f;
                damageGrowth = 0.1f;
                score = 2150;
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

        if (retEnemy.type == EnemyType.makula)
        {
            retEnemy.xpBase = 10f;
            retEnemy.xpGrowth = 0.15f;
        }
        else
        {
            retEnemy.xpBase = 2f;
            retEnemy.xpGrowth = 0.15f;
        }

        retEnemy.xp = Formulas.ExponentialGrowth(retEnemy.xpBase, retEnemy.xpGrowth, retEnemy.level - 1);

        return retEnemy;
    }

    public EnemyStructure GenerateWithLevel(EnemyType enemyType, int level)
    {
        EnemyStructure retEnemy = new EnemyStructure().GenerateBasic(enemyType);
        retEnemy.level = level;
        retEnemy.life = Mathf.RoundToInt(Formulas.ExponentialGrowth(retEnemy.lifeBase, retEnemy.lifeGrowth, retEnemy.level - 1));
        retEnemy.damage = Formulas.ExponentialGrowth(retEnemy.damageBase, retEnemy.damageGrowth, retEnemy.level - 1);
        retEnemy.xp = Formulas.ExponentialGrowth(retEnemy.xpBase, retEnemy.xpGrowth, retEnemy.level - 1);

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
