using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int level;
    public EnemyType type;

    private Animator animator;
    private _2dxFX_HSV hsv_spriteFX;
    private float[] genotype;// /// - HueShift (0,360)          [0]
                                /// - Color Saturation (-2,2)   [1]
                                /// - Brightness (-2,2)         [2]
                                /// - size (0.275,0.325)        [3]


    
    private float life;

    
    private float damage;

    private EnemyStructure new_enemy;
    private bool critico;
    private Pooler pooler;
    private LevelManager levelManager;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        hsv_spriteFX = this.GetComponent<_2dxFX_HSV>();
        pooler = this.GetComponentInParent<Pooler>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    //void Start () {

    //    //new_enemy = new EnemyStructure().GenerateWithLevel(type,level);
    //    //yield return new WaitForSeconds(0.1f);
    //    animator = this.GetComponent<Animator>();
    //    hsv_spriteFX = this.GetComponent<_2dxFX_HSV>();
    //    pooler = this.GetComponentInParent<Pooler>();
    //}


    void OnEnable() // On Enable we need to replenish its life otherwise would appear as a Zombie
    {
        //yield return new WaitForSeconds(0.1f);
        new_enemy = new EnemyStructure().GenerateWithLevel(type, level);
        life = new_enemy.life;
        damage = new_enemy.damage;
        this.GetComponent<_2dxFX_HSV>()._HueShift = new_enemy.dna_hue;
        this.GetComponent<_2dxFX_HSV>()._Saturation = new_enemy.dna_colorsat;
        this.GetComponent<_2dxFX_HSV>()._ValueBrightness = new_enemy.dna_brightness;
    }
    /// <summary>
    /// Retu rns damage done by enemy based on standard fumble,critic (< 4,> 95)
    /// </summary>
    public float GetDamageDoneByEnemy()
    {
        damage = Formulas.GetDamageCalculated(damage, out critico);
        return damage;
    }

    public float GetDamageDoneByEnemy(int fumbleThreshold, int criticThreshold)
    {
        damage = Formulas.GetDamageCalculated(damage,fumbleThreshold,criticThreshold, out critico);
        return damage;
    }


    public void ApplyDamageToEnemy(float damage_in)
    {
        life = life - damage_in;

        if (life < 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        levelManager.GetNewEnemy();
        pooler.RemoveElement(this.transform);

    }

}
