using UnityEngine;
using System.Collections;

public static class Formulas : object
{
    const int fumbleThreshold = 4;
    const int criticThreshold = 95;
    // Energy base - calculated
    // exploration skill - fix
    // accura

    /// <summary>
    /// The Exponential Growth/Decay Formula.
    /// </summary>
    /// <param name="k1">The Base</param>
    /// <param name="k2">The Growth/Decay</param>
    /// <param name="k3">The Level</param>
    /// <returns></returns>
    public static float ExponentialGrowth(float k1, float k2, float k3)
    {
        float y = k1 * Mathf.Pow((1 + k2), k3);
        //Debug.Log("k1: " + k1);
        //Debug.Log("k2: " + k2);
        //Debug.Log("k3: " + k3);
        //Debug.Log("Resultado: " + y);
        return y;
    }

    /*
    public static int coinsGivenByLevelUp(int coinslevel)
    {
        int coins = (int)ExponentialGrowth(100, 0.15f, coinslevel);
        return coins;
    }

    public static int healthGivenByLevelUp(int healthLevel)
    {
        int health = (int)ExponentialGrowth(100, 0.15f, healthLevel);
        return health;
    }

    public static int accuracyGivenByLevelUp(int accuracyLevel)
    {
        int accuracy = (int)ExponentialGrowth(100, 0.15f, accuracyLevel);
        return accuracy;
    }

    public static int tokenGivenByLevelUp(int tokenLevel)
    {
        int token = (int)ExponentialGrowth(100, 0.15f, tokenLevel);
        return token;
    }



    public static int XpGivenByEnemy(int enemyLevel)
    {
        int xp = (int)ExponentialGrowth(2, 0.15f, enemyLevel);
        return xp;
    }

    public static int XpGivenByBoss(int enemyLevel)
    {
        int xp = (int)ExponentialGrowth(10, 0.15f, enemyLevel);
        return xp;
    }

    public static int XpMonkeyDifferenceByLevel(int monkeyLevel)
    {
        int xp = (int)ExponentialGrowth(25, 0.3f, monkeyLevel);
        return xp;
    }

     */


    /// <summary>
    /// Returns the Weapon Damage by level and rounded up.
    /// </summary>
    /// <param name="damageBase"></param>
    /// <param name="damageGrowth"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static float WeaponDamage(float damageBase, float damageGrowth, int level)
    {
        float damage = Mathf.Ceil(ExponentialGrowth(damageBase, damageGrowth, level));
        //Debug.Log("damage from Formulas.WeaponDamage > " + damage);
        return damage;
    }

    /*
    /// <summary>
    /// Given a Monkey XP it returns the level for that XP
    /// </summary>
    /// <param name="xp"></param>
    /// <returns></returns>
    public static int LevelFormula(int xp)
    {
        int level = 0;
        //level = (int)(Mathf.Log10(xp) / Mathf.Log10(32.5f))+1;
        //level = sum  1 + Mathf.FloorToInt((Mathf.Log(xp / 25) / Mathf.Log(1.3f)));
        int temp_xp;
        //
        for (int n = 1; n <= 50; n++)
        {

            temp_xp = XpMonkeyDifferenceByLevel(n);
            //Debug.Log("Temp XP: " + temp_xp);
            xp -= temp_xp;

            if (xp < 0)
            {
                level = n - 1;
                //Debug.Log("Level from formula: " + level);
                //break;
                n = 51;
            }
            else if (xp == 0)
            {
                level = n;
            }
        }
        if (level == 0) { level = 1; }
        return level;
    }


    public static int GetTotalXpforGivenLevel(int level)
    {
        int xp = 0;

        for (int n = 1; n <= level; n++)
        {
            xp += XpMonkeyDifferenceByLevel(n);
        }

        return xp;
    }

    */

    /// <summary>
    /// Returns a Damage value based on the orginal param and if its a critical.
    /// The damage is calculated based on a D100, based on this result we get
    /// a Fumble (04 or less) , Normal or Critical (85 or more)
    /// A fumble gives damage = 0
    /// A normal gives damage weapondamage +-1
    /// a critic gives damage weapondamage *2
    /// </summary>
    /// <param name="weaponDamage"></param>
    /// <param name="critical"></param>
    /// <returns></returns>
    public static float GetDamageCalculated(float weaponDamage, out bool critical)
    {
        //Debug.Log("Weapon Damage >>>> " + weaponDamage);
        float damage;
        int dice;
        critical = false;
        dice = d100();

        // If dice is less than 4 we get a Fumble.
        if (dice <= 4)
        {
            damage = 0;
        }
        else if (dice > 4 && dice <= 84)
        {
            damage = Random.Range(weaponDamage - 1f, weaponDamage + 1f);
        }
        else
        {
            damage = weaponDamage * 2;
            critical = true;
        }


        // As damage cannot be less than 0.1 we set it to 0.1.
        if (damage < 0.1f)
        {
            damage = 0.1f;
        }

        return damage;
    }


    public static float GetDamageCalculated(float weaponDamage, int fumbleThreshold, int criticThreshold, out bool critical)
    {
        //Debug.Log("Weapon Damage >>>> " + weaponDamage);
        float damage;
        int dice;
        critical = false;
        dice = d100();

        // If dice is less than fumbleThreshold we get a Fumble.
        if (dice <= fumbleThreshold)
        {
            damage = 0;
        }
        else if (dice > fumbleThreshold && dice <= criticThreshold)
        {
            damage = Random.Range(weaponDamage - 1f, weaponDamage + 1f);
        }
        else
        {
            damage = weaponDamage * 2;
            critical = true;
        }


        // As damage cannot be less than 0.1 we set it to 0.1.
        if (damage < 0.1f)
        {
            damage = 0.1f;
        }

        return damage;
    }

    public static int d100()
    {
        int a = UnityEngine.Random.Range(0, 10);
        int b = UnityEngine.Random.Range(0, 10);

        return a * 10 + b;
    }
}