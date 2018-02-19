﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XintanaStructure
{
    public int level;
    public float life, lifeBase, lifeGrowth;
    public float damage, damageBase, damageGrouth;

    public WeaponType weaponType;

    public XintanaStructure GenerateXintanaBasic(WeaponType _weaponType)
    {
        this.weaponType = _weaponType;
        switch (weaponType)
        {
            case WeaponType.blue:
                damageBase = 2;
                damageGrouth = 0.01f;
                break;
            case WeaponType.green:
                damageBase = 2.5f;
                damageGrouth = 0.01f;
                break;
            case WeaponType.red:
                damageBase = 1.5f;
                damageGrouth = 0.005f;
                break;
            case WeaponType.yellow:
                damageBase = 3;
                damageGrouth = 0.025f;
                break;
            case WeaponType.black:
                damageBase = 2f;
                damageGrouth = 0.03f;
                break;
        }

        XintanaStructure xintana = new XintanaStructure();

        xintana.level = 1;
        xintana.lifeBase = 10;
        xintana.lifeGrowth = 0.5f;
        xintana.life = Mathf.RoundToInt(Formulas.ExponentialGrowth(xintana.lifeBase, xintana.lifeGrowth, xintana.level - 1));
        xintana.damageBase = damageBase;
        xintana.damageGrouth = damageGrouth;
        xintana.damage = Formulas.ExponentialGrowth(xintana.damageBase, xintana.damageGrouth, xintana.level - 1);


        return xintana;
    }

    public XintanaStructure GenerateXintanaWithLevel(WeaponType weaponType, int level)
    {
        XintanaStructure xintana = new XintanaStructure().GenerateXintanaBasic(weaponType);
        xintana.level = level;
        xintana.life = Mathf.RoundToInt(Formulas.ExponentialGrowth(xintana.lifeBase, xintana.lifeGrowth, xintana.level - 1));
        xintana.damage = Formulas.ExponentialGrowth(xintana.damageBase, xintana.damageGrouth, xintana.level - 1);

        return xintana;
    }
}
